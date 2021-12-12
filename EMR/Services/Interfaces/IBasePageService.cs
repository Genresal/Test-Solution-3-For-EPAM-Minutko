namespace EMR.Services
{
    public interface IBasePageService<T>
    {
        public T GetById(int id);
        public void Create(T item);
        public void Update(T item);
        public void Delete(int id);
    }
}
