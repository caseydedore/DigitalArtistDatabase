using DigitalArtistDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DigitalArtistDatabase.DAL
{
    public class FakeRepository<TEntity> : IGenericRepository<TEntity> where TEntity : IEntityModel
    {
        private List<TEntity> entities = new List<TEntity>();

        public FakeRepository(List<TEntity> e)
        {
            entities = e;
        }
        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            var query = entities.AsQueryable<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity GetByID(object id)
        {
            return (from e in entities
                    where e.ID == (int)id
                    select e).FirstOrDefault();
        }

        public void Insert(TEntity entity)
        {
            entities.Add(entity);
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = GetByID(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            entities.Remove(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            TEntity target = (from e in entities
                              where e.ID == entityToUpdate.ID
                              select e).FirstOrDefault();
            
            target = entityToUpdate;
        }
    }
}