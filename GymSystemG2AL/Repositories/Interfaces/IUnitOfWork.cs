using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;



namespace GymSystemG2AL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {

        IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();

        int SaveChanges();

        public ISessionRepository SessionRepository{ get;}
    }
}
