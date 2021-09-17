using Fecovita.Core.Entities.Base;
using Fecovita.Core.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Fecovita.Persistence.Repository.Base
{
    public class EntityFrameworkGenericRepository<TEntity> : DbContextFactory, IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        public void Create(TEntity entity)
        {
            using (DbContextTransaction transaction = Instance.Database.BeginTransaction())
            {
                try
                {
                    Instance.Set<TEntity>().Add(entity);
                    SaveChanges();
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public void Delete(TEntity entity)
        {
            using (DbContextTransaction transaction = Instance.Database.BeginTransaction())
            {
                try
                {
                    Instance.Set<TEntity>().Remove(entity);
                    SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public void Delete(int id)
        {
            using (DbContextTransaction transaction = Instance.Database.BeginTransaction())
            {
                try
                {
                    var entityToDelete = Instance.Set<TEntity>().FirstOrDefault(e => e.Id == id);
                    if (entityToDelete != null)
                    {
                        Instance.Set<TEntity>().Remove(entityToDelete);
                    }
                    SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public void Edit(TEntity entity)
        {
            using (DbContextTransaction transaction = Instance.Database.BeginTransaction())
            {
                try
                {
                    var editedEntity = Instance.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);
                    editedEntity = entity;
                    SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public TEntity GetById(int id)
        {
            return Instance.Set<TEntity>().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<TEntity> Filter()
        {
            return Instance.Set<TEntity>();
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            return Instance.Set<TEntity>().Where(predicate);
        }

        public void SaveChanges() => Instance.SaveChanges();       
    }
}
