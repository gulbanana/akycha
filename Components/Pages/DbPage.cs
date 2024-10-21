using Akycha.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Akycha.Components.Pages;

public class DbPage : ComponentBase, IDisposable, IUnitOfWork
{
    [Inject] public required IDbContextFactory<FactoryContext> Factory { get; set; }
    [Inject] public required ILogger<DbPage> Logger { get; set; }

    private FactoryContext db = default!;
    public bool HasChanges { get; set; }
    private Timer timer = default!;
    private bool disposed;
    public event Action? OnChanged;

    protected override void OnInitialized()
    {
        db = Factory.CreateDbContext();
        timer = new(OnTimeout);
    }

    public void Dispose()
    {
        disposed = true;
        timer.Dispose();
        db.Dispose();
    }

    public void NotifyChanged()
    {
        InvokeAsync(() =>
        {
            HasChanges = true;
            StateHasChanged();
            timer.Change(TimeSpan.FromMilliseconds(400), Timeout.InfiniteTimeSpan);
        });
    }

    private async void OnTimeout(object? _)
    {
        await InvokeAsync(async () =>
        {
            if (!disposed)
            {
                await dbLock.WaitAsync();
                try
                {
                    await db.SaveChangesAsync();
                    HasChanges = false;
                    StateHasChanged();
                    OnChanged?.Invoke();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Save failed.");
                }
                finally
                {
                    dbLock.Release();
                }
            }
        });
    }

    private readonly SemaphoreSlim dbLock = new(1, 1);
    public async Task<T> Load<T>(Func<FactoryContext, Task<T>> f)
    {
        await dbLock.WaitAsync();
        try
        {
            return await f(db);
        }
        finally
        {
            dbLock.Release();
        }
    }

    public LocalView<T> GetLoaded<T>() where T : class
    {
        return db.Set<T>().Local;
    }
}
