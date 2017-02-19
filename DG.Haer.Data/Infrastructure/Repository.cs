using DG.Haer.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace DG.Haer.Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        private readonly IDbFactory _dbFactory;
        
        private DbSet<TEntity> _entities;
        protected DbSet<TEntity> Entities => _entities ?? (_entities = DbContext.Set<TEntity>());

        private AppDbContext _dbContext;
        protected AppDbContext DbContext => _dbContext ?? (_dbContext = _dbFactory.Init());

        protected Repository(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public virtual TEntity GetById(int id)
        {
            return Entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).ToList();
        }

        public virtual void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            Entities.AddOrUpdate(entity);
        }

        public virtual void Remove(int id)
        {
            var entity = Entities.Find(id);
            Remove(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);

            Entities.Remove(entity);
        }

        public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Get(predicate);
            foreach (var entity in entities)
                Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        public bool Exists(int id)
        {
            return Entities.Find(id) != null;
        }

        public bool NotExists(int id)
        {
            return !Exists(id);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Get(predicate);
            return entities.Any();
        }

        public bool NotExists(Expression<Func<TEntity, bool>> predicate)
        {
            return !Exists(predicate);
        }
    }
}
