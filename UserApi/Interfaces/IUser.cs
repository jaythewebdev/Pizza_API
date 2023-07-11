namespace UserApi.Interfaces
{
    public interface IUser<K, T>
    {
        T Add(T item);
        T Get(K key);
    }
}
