using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaxFormGeneratorApi.Domain;

namespace TaxFormGeneratorApi.Dal
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly TaxFormGeneratorContext DbContext;

        public GenericRepository(TaxFormGeneratorContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public TEntity GetOne(int entityId)
        {
            return this.Query().AsNoTracking().Single(entity => entity.Id == entityId);
        }

        public TEntity GetOneOrNone(int entityId)
        {
            return this.Query().AsNoTracking().SingleOrDefault(entity => entity.Id == entityId);
        }

        public IEnumerable<TEntity> GetBy(Func<TEntity, bool> predicate)
        {
            return this.Query().AsNoTracking().Where(predicate).ToList();
        }
        
        public TEntity GetOneOrNoneBy(Func<TEntity, bool> predicate)
        {
            return this.Query().AsNoTracking().SingleOrDefault(predicate);
        }

        public void Insert(TEntity entity)
        {
            this.DbContext.Set<TEntity>().Add(entity);
            this.Commit();
        }
        
        public void Update(TEntity entity)
        {
            this.Commit(); // TODO: rethink
        }

        public void Remove(params TEntity[] entities)
        {
            this.DbContext.Set<TEntity>().RemoveRange(entities);
            this.Commit();
        }
        
        protected IQueryable<TEntity> Query()
        {
            return this.DbContext.Set<TEntity>();
        }

        protected TEntity Create()
        {
            return this.DbContext.Set<TEntity>().CreateProxy();
        }

        protected void Commit()
        {
            this.DbContext.SaveChanges();
        }
    }
}