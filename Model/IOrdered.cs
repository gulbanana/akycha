namespace Akycha.Model;

public interface IOrdered<T>
{
    int Id { get; set; }
    static abstract string GetKey(T t);
}