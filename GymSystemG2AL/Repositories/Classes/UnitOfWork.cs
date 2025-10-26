using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;
using GymSystemG2AL.Repositories.Interfaces;

namespace GymSystemG2AL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        private readonly GymSystemDBContext dbContext;
        public UnitOfWork(GymSystemDBContext dBContext )
        {
            dbContext = dBContext;
        }
        public IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);
            if (repositories.TryGetValue(EntityType, out var Repo))
                return (IGenaricRepository<TEntity>)Repo;

            var NewRepo = new GenaricRepository<TEntity>(dbContext);
            repositories[EntityType] = NewRepo;

            return NewRepo;
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
    }
}