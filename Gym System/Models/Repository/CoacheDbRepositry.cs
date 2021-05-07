using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Models.Repository
{
    public class CoacheDbRepositry : IGymRepository<Coache>
    {
        GymDbContext dbContext;
        public CoacheDbRepositry(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Coache entity)
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Coache coache = Find(id);
            dbContext.Remove(coache);
            dbContext.SaveChanges();
        }

        public Coache Find(int id)
        {
            Coache coache = dbContext.Coaches.SingleOrDefault(a => a.Id == id);
            return coache;
        }

        public List<Coache> ListOfData()
        {
            var list= dbContext.Coaches.ToList();
            return list;
        }

        public List<Coache> Search(string term)
        {
            var result = dbContext.Coaches.Where(b =>
            b.Full_Name.Contains(term) || b.PhoneNo.Contains(term)).ToList();
            return result;
        }

        public void Update(Coache coache)
        {
            dbContext.Update(coache);
            dbContext.SaveChanges();
        }
    }
}
