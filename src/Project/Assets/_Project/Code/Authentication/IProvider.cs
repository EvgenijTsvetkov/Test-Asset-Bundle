namespace Project.Services
{
    public interface IProvider<T> where T : class
    {
        T Value { get; set; }
    }
}