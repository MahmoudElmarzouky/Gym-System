using Gym_System.Models;
using Gym_System.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGymRepository<Game> gameRepository;

        public GamesController(IGymRepository<Game> gameRepository)
        {
            this.gameRepository = gameRepository;
        }
        // GET: GamesController
        public ActionResult Index(string SearchWord, int pageSize, int pageNumber, string order)
        {
            if (order == "asc")
            {
                ViewBag.ascending = true;
                var list = gameRepository.ListOfData();
                return View(list.OrderBy(e => e.GameName));
            }
            if (order == "desc")
            {
                ViewBag.descending = true;
                var list = gameRepository.ListOfData();
                return View(list.OrderByDescending(e => e.GameName));
            }
            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pagesize = pageSize;
                ViewBag.pagenumber = pageNumber;
                var list = gameRepository.ListOfData();
                return View(list.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }
            if (string.IsNullOrEmpty(SearchWord))
            {
                var list = gameRepository.ListOfData();
                return View(list);
            }
            else
            {
                var list = gameRepository.Search(SearchWord);
                ViewBag.search = SearchWord;
                return View(list);

            }
        }

        // GET: GamesController/Details/5
        public ActionResult Details(int id)
        {
            var game = gameRepository.Find(id);
            return View(game);
        }

        // GET: GamesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GamesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game model)
        {
                try
                {
                    gameRepository.Add(model);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
           
        }

        // GET: GamesController/Edit/5
        public ActionResult Edit(int id)
        {
            var game = gameRepository.Find(id);
            return View(game);
        }

        // POST: GamesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Game model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    gameRepository.Update(model);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: GamesController/Delete/5
        public ActionResult Delete(int id)
        {
            var game = gameRepository.Find(id);
            return View(game);
        }

        // POST: GamesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Game model)
        {
            try
            {
                gameRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
