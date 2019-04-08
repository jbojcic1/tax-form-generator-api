using System;
using System.Collections.Generic;
using System.Linq;
using TaxFormGeneratorApi.Domain;

namespace TaxFormGeneratorApi.Dal
{
    public interface IRepository<TEntity> 
        where TEntity : class, IEntity
    {
        TEntity Find(params object[] keyValues);

        TEntity GetOne(int entityId);
        
        TEntity GetOneOrNone(int entityId);
        
        IEnumerable<TEntity> GetBy(Func<TEntity, bool> predicate);
        
        TEntity GetOneOrNoneBy(Func<TEntity, bool> predicate);
        
        void Insert(TEntity entity);

        void Update(TEntity entity);
        
        void Remove(params TEntity[] entities);
    }
}