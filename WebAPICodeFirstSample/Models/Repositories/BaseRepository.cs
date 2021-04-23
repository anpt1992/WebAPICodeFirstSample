using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Models.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetIQueryable();
        List<T> Query(Expression<Func<T, bool>> e);
        T GetById(object id);
        void Delete(object id);
        void DeleteRange(List<object> ids);
        T Insert(T entity);
        List<T> InsertRange(List<T> entites);
        void DeleteCondition(Expression<Func<T, bool>> e);
        void Update(T entity, object id);
        IEnumerable<T> SQLRaw(string sql);

        // Use in case of using nav properties of 
        // obj that just inserted. It's imposible to
        // use nav properties of object right after inserted
        void LoadNavigationProperty<TRef>(T entity, Expression<Func<T, TRef>> exp) where TRef : class;
        void LoadNavigationCollection<TRef>(T entity, Expression<Func<T, IEnumerable<TRef>>> exp) where TRef : class;
        void UpdateRange(List<T> entities);
        public delegate void Change(T entity);
        void UpdateIf(Expression<Func<T, bool>> condition, Change change);
        delegate void ChangeUpdate(EntityEntry<T> changeUpdate);
        // changeUpdate is used to change update.
        // For example: ignore update for some fields like created_at, ...
        void Update(T entity, object id, ChangeUpdate changeUpdate);
        T GetByIds(object id, object id2);
        void Remove(object id, object id2);
        Task<int> DeleteAsync(object id);
    }
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext  _dbContext;
        protected DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext  dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual void Delete(object id)
        {
            T entity = GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                Save();
            }
            else throw new Exception("Not found");
        }

        public virtual async Task<int> DeleteAsync(object id)
        {
            long _id = Convert.ToInt64(id);
            var entity = await _dbSet.FindAsync(_id);
            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }


        public virtual T GetById(object id)
        {
            long _id = Convert.ToInt64(id);
            return _dbSet.Find(_id);
        }

        public IQueryable<T> GetIQueryable()
        {
            return _dbSet;
        }

        public virtual T Insert(T entity)
        {
            entity = _dbSet.Add(entity).Entity;
            Save();
            return entity;
        }
        protected void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                handleDbUpdateEx(dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void handleDbUpdateEx(DbUpdateException dbEx)
        {
            if (dbEx.InnerException != null)
            {
                // dbEx.InnerException.InnerException is SqlException
                if (dbEx.InnerException.InnerException != null)
                    throw new Exception(dbEx.InnerException.InnerException.Message,
                                        dbEx.InnerException.InnerException);
                else throw new Exception(dbEx.InnerException.Message, dbEx.InnerException);
            }
            else throw new Exception(GetDbUpdateErrMsgs(dbEx), dbEx);
        }

        // Get common error messages from DbUpdateException
        private string GetDbUpdateErrMsgs(DbUpdateException dbEx)
        {
            return dbEx.Message;
        }

        public virtual void Update(T entityToUpdate, object id)
        {
            var originEntity = GetById(id);
            _dbContext.Entry(originEntity).State = EntityState.Detached;
            _dbContext.Update(entityToUpdate);
            Save();
        }

        public List<T> Query(Expression<Func<T, bool>> e)
        {
            return _dbSet.Where(e).ToList();
        }

        public IEnumerable<T> SQLRaw(string sql)
        {
            return _dbSet.FromSqlRaw(sql).ToList();
        }

        public void DeleteRange(List<object> ids)
        {
            List<T> deletedEntities = new List<T>();
            foreach (int id in ids)
            {
                var entity = GetById(id);
                if (entity != null) deletedEntities.Add(entity);
            }
            _dbSet.RemoveRange(deletedEntities);
            Save();
        }

        public List<T> InsertRange(List<T> entites)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                _dbSet.AddRange(entites);
                Save();
                dbContextTransaction.Commit();
            }
            return entites;

        }

        public void DeleteCondition(Expression<Func<T, bool>> e)
        {
            List<T> deletedEntities = _dbSet.Where(e).ToList();
            _dbSet.RemoveRange(deletedEntities);
            Save();
        }

        public void LoadNavigationProperty<TRef>(T entity, Expression<Func<T, TRef>> exp) where TRef : class
        {
            _dbContext.Entry(entity).Reference(exp).Load();
        }

        public void UpdateRange(List<T> entities)
        {
            _dbSet.UpdateRange(entities);
            Save();
        }

        public void UpdateIf(Expression<Func<T, bool>> condition, IBaseRepository<T>.Change change)
        {
            var entities = _dbSet.Where(condition).ToList();
            foreach (var entity in entities)
            {
                change.Invoke(entity);
            }
            UpdateRange(entities);
        }

        // changeUpdate is used to change update.
        // For example: ignore update for some fields like created_at, ...
        public void Update(T entity, object id, IBaseRepository<T>.ChangeUpdate changeUpdate)
        {
            var originEntity = GetById(id);

            _dbContext.Entry(originEntity).State = EntityState.Detached;

            var updatedEntry = _dbContext.Update(entity);

            changeUpdate.Invoke(updatedEntry);

            Save();
        }

        public void LoadNavigationCollection<TRef>(
            T entity, Expression<Func<T, IEnumerable<TRef>>> exp) where TRef : class
        {
            _dbContext.Entry(entity).Collection(exp).Load();
        }

        public virtual T GetByIds(object id, object id2)
        {
            return _dbSet.Find(id, id2);
        }
        public virtual void Remove(object id, object id2)
        {
            T entity = GetByIds(id, id2);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                Save();
            }
            else throw new Exception("Not found");
        }
    }
}
