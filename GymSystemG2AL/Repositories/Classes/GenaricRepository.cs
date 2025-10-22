using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;
using GymSystemG2AL.Repositories.Interfaces;

namespace GymSystemG2AL.Repositories.Classes
{
    public class GenaricRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymSystemDBContext _dbContext;
        public GenaricRepository(GymSystemDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition == null)
                return _dbContext.Set<TEntity>().ToList();
            else
                return _dbContext.Set<TEntity>().Where(condition).ToList();
        }

        public TEntity? GetBYId(int Id)
        {
            return _dbContext.Set<TEntity>().Find(Id);
        }

        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }


}