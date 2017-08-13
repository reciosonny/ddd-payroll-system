namespace PayrollAppSample.DDD.Domain.Repository.Implementation {
    public interface IGenericRepository<T> where T : class {
        void Add(T entity);
        void Delete(T model);
        T GetFirstItem();
        void Update(T entity);
        T FindItem(int id);
    }
}