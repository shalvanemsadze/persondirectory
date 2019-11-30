namespace PersonDirectory.Data.Models
{
    public abstract class Base<T>
    {
        public T Id { get; set; }
    }
}