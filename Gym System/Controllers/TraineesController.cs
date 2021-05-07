using Gym_System.Models;
using Gym_System.Models.Repository;
using Gym_System.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Controllers
{
    public class TraineesController : Controller
    {
        private readonly IGymRepository<Trainee> traineeRepository;
        private readonly IGymRepository<Coache> coacheRepository;
        private readonly IGymRepository<Game> gameRepository;

        public TraineesController(IGymRepository<Trainee>traineeRepository, IGymRepository<Coache> coacheRepository, IGymRepository<Game> gameRepository)
        {
            this.traineeRepository = traineeRepository;
            this.coacheRepository = coacheRepository;
            this.gameRepository = gameRepository;
        }
        // GET: TraineesController
        public ActionResult Index(string SearchWord, int pageSize, int pageNumber, string order)
        {
            if (order == "asc")
            {
                ViewBag.ascending = true;
                var list = traineeRepository.ListOfData();
                return View(list.OrderBy(e => e.Full_Name));
            }
            if (order == "desc")
            {
                ViewBag.descending = true;
                var list = traineeRepository.ListOfData();
                return View(list.OrderByDescending(e => e.Full_Name));
            }
            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pagesize = pageSize;
                ViewBag.pagenumber = pageNumber;
                var list = traineeRepository.ListOfData();
                return View(list.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }
            if (string.IsNullOrEmpty(SearchWord))
            {
                var list = traineeRepository.ListOfData();
                return View(list);
            }
            else
            {
                var list = traineeRepository.Search(SearchWord);
                ViewBag.search = SearchWord;
                return View(list);
            }
            
        }

        // GET: TraineesController/Details/5
        public ActionResult Details(int id)
        {
            var trainne = traineeRepository.Find(id);
            return View(trainne);
        }

        // GET: TraineesController/Create
        public ActionResult Create()
        {
            var model = new CocheTraineeViewModel
            {
                Coaches = fillSelectedCoaches()
                ,
                Games=fillSelectedGames()
        };
            return View(model);
        }

        // POST: TraineesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CocheTraineeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.CoacheID == -1)
                    {
                        ViewBag.Name = "Please Select Date !!";
                        return View(getAllCoachesAndGames());
                    }
                    Coache coache_ = coacheRepository.Find(model.CoacheID);
                    Game  game_ = gameRepository.Find(model.GameID);
                    Trainee trainee = new Trainee
                    {
                        Id = model.TraineeID,
                        Full_Name = model.TraineeName,
                        Email = model.Email,
                        PhoneNo = model.PhoneNo,
                        age = model.age,
                        StartDate = model.StartDate,
                        Months = model.Months,
                        game = game_,
                        coache = coache_,
                        Cost = game_.CostPreMonth * model.Months
                    };
                    traineeRepository.Add(trainee);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "You must fill required felled");
            return View(getAllCoachesAndGames());
        }

        // GET: TraineesController/Edit/5
        public ActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                Trainee trainee = traineeRepository.Find(id);
                var coacheId = (trainee.coache == null) ? 0 : trainee.coache.Id;
                var gameId = (trainee.game == null) ? 0 : trainee.game.Id;
                var viewModel = new CocheTraineeViewModel
                {
                    TraineeID = trainee.Id,
                    TraineeName = trainee.Full_Name,
                    Email = trainee.Email,
                    PhoneNo = trainee.PhoneNo,
                    age = trainee.age,
                    StartDate = trainee.StartDate,
                    GameID = gameId,
                    Games = gameRepository.ListOfData().ToList()
                ,
                    CoacheID = coacheId,
                    Coaches = coacheRepository.ListOfData().ToList()
                ,
                    Cost = trainee.Cost
                };
                return View(viewModel);
            }
            return View();
        }

        // POST: TraineesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CocheTraineeViewModel model)
        {
            try
            {
                Coache coache_ = coacheRepository.Find(model.CoacheID);
                Game game_ = gameRepository.Find(model.GameID);
                Trainee trainee = new Trainee
                {
                    Id = model.TraineeID,
                    Full_Name = model.TraineeName,
                    Email = model.Email,
                    PhoneNo = model.PhoneNo,
                    age = model.age,
                    StartDate = model.StartDate,
                    Months = model.Months,
                    game = game_,
                    coache = coache_,
                    Cost = game_.CostPreMonth*model.Months
                }; traineeRepository.Update(trainee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
     

        // GET: TraineesController/Delete/5
        public ActionResult Delete(int id)
        {
            var trainee = traineeRepository.Find(id);
            return View(trainee);
        }

        // POST: TraineesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Trainee model)
        {
            try
            {
                traineeRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Coache> fillSelectedCoaches()
        {
            var coache = coacheRepository.ListOfData().ToList();
            coache.Insert(0, new Coache { Id = -1, Full_Name = "---Please Select an Coache---" });
            return coache;
        }
        List<Game> fillSelectedGames()
        {
            var game = gameRepository.ListOfData().ToList();
            game.Insert(0, new Game { Id = -1, GameName = "---Please Select an game---" });
            return game;
        }
        CocheTraineeViewModel getAllCoachesAndGames()
        {
            var vmodel = new CocheTraineeViewModel
            {
                Coaches = fillSelectedCoaches(),
                Games=fillSelectedGames()
            };
            return vmodel;
        }
    }
}
