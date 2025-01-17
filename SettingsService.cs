using Akycha.Model;
using Microsoft.EntityFrameworkCore;

namespace Akycha;

public class SettingsService : IDisposable
{
    private readonly SemaphoreSlim dbLock = new(1, 1);
    private readonly FactoryContext db;
    private readonly ILogger<SettingsService> logger;
    private Timer timer = default!;
    private bool disposed;

    public SettingsService(FactoryContext db, ILogger<SettingsService> logger)
    {
        db.Settings.Load();
        this.db = db;
        this.logger = logger;
        this.timer = new(OnTimeout);
    }

    public void Dispose()
    {
        dbLock.Wait();
        try
        {
            disposed = true;
            timer.Dispose();
            db.Dispose();
        }
        finally
        {
            dbLock.Release();
        }
    }

    public string Get(string key, string fallback)
    {
        if (db.Settings.Local.FindEntry(key)?.Entity is Setting { Value: var result })
        {
            return result;
        }
        else
        {
            return fallback;
        }
    }

    public bool Get(string key, bool fallback)
    {
        if (db.Settings.Local.FindEntry(key)?.Entity is Setting { Value: var result })
        {
            return bool.TryParse(result, out var parsed) ? parsed : fallback;
        }
        else
        {
            return fallback;
        }
    }

    public int Get(string key, int fallback)
    {
        if (db.Settings.Local.FindEntry(key)?.Entity is Setting { Value: var result })
        {
            return int.TryParse(result, out var parsed) ? parsed : fallback;
        }
        else
        {
            return fallback;
        }
    }

    public void Put(string key, string value)
    {
        var setting = db.Settings.Local.FindEntry(key) ?? db.Add(new Setting { Key = key, Value = value });
        setting.Entity.Value = value;
        timer.Change(TimeSpan.FromMilliseconds(500), Timeout.InfiniteTimeSpan);
    }

    public void Put(string key, bool value)
    {
        Put(key, value.ToString());
    }

    public void Put(string key, int value)
    {
        Put(key, value.ToString());
    }

    private async void OnTimeout(object? _)
    {
        await dbLock.WaitAsync();
        try
        {
            if (!disposed)
            {
                await db.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Settings save failed.");
        }
        finally
        {
            dbLock.Release();
        }
    }
}