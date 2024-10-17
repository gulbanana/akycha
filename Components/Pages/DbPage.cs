using Akycha.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Akycha.Components.Pages;

public class DbPage : ComponentBase, IDisposable, IUnitOfWork
{
    [Inject] public required IDbContextFactory<FactoryContext> Factory { get; set; }
    public FactoryContext DB { get; private set; } = default!;
    public bool HasChanges { get; set; }
    private Timer timer = default!;
    private bool disposed;

    protected override void OnInitialized()
    {
        DB = Factory.CreateDbContext();
        timer = new(OnTimeout);
    }

    public void Dispose()
    {
        disposed = true;
        timer.Dispose();
        DB.Dispose();
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
        await InvokeAsync(() =>
        {
            if (!disposed)
            {
                DB.SaveChanges();
                HasChanges = false;
                OnChanged();
            }
        });
    }

    protected virtual void OnChanged()
    {
        StateHasChanged();
    }

    protected List<T> All<T, U>() where T : class, IOrdered<T, U>
    {
        var set = DB.Set<T>();
        set.Load();
        return DB.Set<T>().Local.OrderBy(T.GetKey).ToList();
    }
}
