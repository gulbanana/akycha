using Akycha.Model;

namespace Akycha;

public interface IUnitOfWork
{
    FactoryContext DB { get; }
    void NotifyChanged();
    event Action OnChanged;
}
