namespace Akycha.Model;

public interface IOrdered<T, U>
{
    static abstract U GetKey(T t);
}