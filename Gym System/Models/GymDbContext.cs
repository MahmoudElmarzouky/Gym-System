using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Models
{
    public class GymDbContext :DbContext
    {
       public GymDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Coache> Coaches { get; set; }
        public DbSet<Trainee>Trainees  { get; set; }
        public DbSet<Game>Games  { get; set; }
    }
   
}
