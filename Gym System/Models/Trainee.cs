using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Models
{
    public class Trainee
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Please Enter A Coach Name ")]
        [DisplayName("Trainee Name")]
        [MinLength(6, ErrorMessage = "Trainee name must contain at least 6 characters")]
        public string Full_Name { get; set; }
        

        
        [Required(ErrorMessage = "Please Enter A Email")]
        [DisplayName("Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "Please Enter A Phone Number")]
        [DisplayName("Phone Number")]
        [RegularExpression("0\\d{10}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNo { get; set; }


        [Required(ErrorMessage = "Please Enter Age")]
        [DisplayName("Age")]
        [Range(15,70, ErrorMessage = "Age must be From 15 Year to 60 Year")]
        public int age { get; set; }
        
        
        [Required(ErrorMessage = "Please Enter Start Date")]
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please Enter Months")]
        [DisplayName("Months")]
        public int Months { get; set; }
        public Game game { get; set; }
        
        public Coache coache { get; set; }

        [DisplayName("Cost")]
        public double Cost { get; set; }
        
    }
}
