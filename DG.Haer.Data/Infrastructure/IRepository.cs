using DG.Haer.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DG.Haer.Data
{
    public interface IRepository<TEntity> where TEntity : Entity, new()
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        bool Exists(int id);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        bool NotExists(int id);
        bool NotExists(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Remove(TEntity entity);
        void Remove(int id);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}
