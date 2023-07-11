namespace PizzaApi.Interfaces
{
    public interface IPizza<K,T>
    {
        T Add(T item);      
        ICollection<T> GetAll();
      
    }
}
