using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Models
{
    public class Coache
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter A Coach Name ")]
        [DisplayName("Coach Name")]
        [MinLength(6,ErrorMessage = "Coach name must contain at least 6 characters")]
        public string Full_Name { get; set; }

        [Required(ErrorMessage = "Please Enter A Phone Number")]
        [DisplayName("Phone Number")]
        [MinLength(11, ErrorMessage = "Phone Number must contain at least 11 digit")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Please Enter A Salary")]
        public double Salary { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }

    }
}
