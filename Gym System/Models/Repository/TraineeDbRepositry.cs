using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Models.Repository
{
    public class TraineeDbRepositry : IGymRepository<Trainee>
    {
        GymDbContext dbContext;
        public TraineeDbRepositry(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Trainee entity)
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Trainee trainee = Find(id);
            dbContext.Remove(trainee);
            dbContext.SaveChanges();
        }

        public Trainee Find(int id)
        {
            Trainee trainee = dbContext.Trainees.Include(e => e.coache).SingleOrDefault(a => a.Id == id);
            return trainee;
        }

        public List<Trainee> ListOfData()
        {
            var list = dbContext.Trainees.Include(e=>e.coache).Include(e=>e.game).ToList();
            return list;
        }

        public List<Trainee> Search(string term)
        {

            var result = dbContext.Trainees.Where(b =>
            b.Full_Name.Contains(term) ||b.Email.Contains(term)|| b.PhoneNo.Contains(term)).ToList();
            return result;

        }

        public void Update(Trainee trainee)
        {
            dbContext.Update(trainee);
            dbContext.SaveChanges();
        }

    }
}
