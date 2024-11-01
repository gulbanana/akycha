namespace Akycha.Model;

public interface IListable<T>
{
    int Id { get; set; }
    string Name { get; }
    string Category { get; }
    static abstract IOrderedEnumerable<T> Sort(IEnumerable<T> ts);
}