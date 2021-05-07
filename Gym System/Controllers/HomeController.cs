using Gym_System.Models;
using Gym_System.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGymRepository<Coache> coachRepository;

        public HomeController(ILogger<HomeController> logger , IGymRepository<Coache>coachRepository)
        {
            _logger = logger;
            this.coachRepository = coachRepository;
        }

        public IActionResult Index()
        {
            var coachesList = coachRepository.ListOfData();
            return View(coachesList);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
