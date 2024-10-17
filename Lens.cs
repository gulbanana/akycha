namespace Akycha;

public class Lens<T>(Func<T> get, Action<T> set)
{
    public T Value
    {
        get => get();
        set => set(value);
    }
}