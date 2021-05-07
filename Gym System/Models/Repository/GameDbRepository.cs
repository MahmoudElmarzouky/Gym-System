using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Models.Repository
{
    public class GameDbRepository : IGymRepository<Game>
    {
        GymDbContext dbContext;
        public GameDbRepository(GymDbContext dbContext)
        {
              this.dbContext=dbContext;
        }
        public void Add(Game entity)
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Game game = Find(id);
            dbContext.Remove(game);
            dbContext.SaveChanges();
        }

        public Game Find(int id)
        {
            Game game= dbContext.Games.SingleOrDefault(a => a.Id == id);
            return game;
        }

        public List<Game> ListOfData()
        {
            var list = dbContext.Games.ToList();
            return list;
        }

        public List<Game> Search(string term)
        {
            var result = dbContext.Games.Where(b =>
             b.GameName.Contains(term)).ToList();
            return result;

        }

        public void Update(Game entity)
        {
            dbContext.Update(entity);
            dbContext.SaveChanges();
        }
    }
}
