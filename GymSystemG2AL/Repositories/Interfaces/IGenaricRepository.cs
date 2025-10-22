using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;

namespace GymSystemG2AL.Repositories.Interfaces
{
    public interface IGenaricRepository<TEntity> where TEntity : BaseEntity , new()
    {
        // Get By Id
        TEntity? GetBYId(int Id);

        // GetAll
        IEnumerable<TEntity> GetAll(Func<TEntity , bool>? condition = null);

        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);
    }
}