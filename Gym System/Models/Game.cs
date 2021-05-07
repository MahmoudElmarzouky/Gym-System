using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Models
{
    public class Game
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Please Enter Game Name ")]
        [DisplayName("Game")]
        public string GameName { get; set; }
        [Required(ErrorMessage = "Please Enter Cost Pre Month ")]
        [DisplayName("Cost Pre Month ")]
        public double CostPreMonth { get; set; }
    }
}
