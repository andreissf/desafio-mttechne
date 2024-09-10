namespace Core.Data;

public interface IRepository<T> where T : Entity
{
    IUnitOfWork UnitOfWork { get; }
    IEnumerable<T> Get();
    T GetById(Guid id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}