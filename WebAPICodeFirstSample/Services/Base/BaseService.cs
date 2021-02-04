using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICodeFirstSample.Models.Repositories;

namespace WebAPICodeFirstSample.Services.Base
{
    public interface IBaseService<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Delete(object id);
        T Insert(T entity);
        void Update(T entity, object id);
        bool Exist(object id);
        List<T> InsertRange(List<T> entities);
    }
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected IBaseRepository<T> _repo;

        public BaseService(IBaseRepository<T> repo)
        {
            _repo = repo;
        }

        public virtual void Delete(object id)
        {
            _repo.Delete(id);
        }

        public bool Exist(object id)
        {
            return _repo.GetById(id) != null;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _repo.GetIQueryable();
        }

        public T GetById(object id)
        {
            return _repo.GetById(id);
        }

        public virtual T Insert(T entity)
        {
            return _repo.Insert(entity);
        }

        public void Update(T entity, object id)
        {
            _repo.Update(entity, id);
        }
        public virtual List<T> InsertRange(List<T> entities)
        {
            return _repo.InsertRange(entities);
        }       
    }
}
