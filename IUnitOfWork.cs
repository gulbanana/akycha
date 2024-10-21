using Akycha.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Akycha;

public interface IUnitOfWork
{
    Task<T> Load<T>(Func<FactoryContext, Task<T>> f);
    LocalView<T> GetLoaded<T>() where T : class;
    void NotifyChanged();
    event Action OnChanged;
}
