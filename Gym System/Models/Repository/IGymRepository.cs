using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Models.Repository
{
    public interface IGymRepository<TEntity>
    {
        public List<TEntity> ListOfData();
        public TEntity Find(int id);
        public void Add(TEntity entity);
        public void Delete(int  id);
        public void Update(TEntity entity);
        public List<TEntity> Search(String term);
    }
}
