using Gym_System.Models;
using Gym_System.Models.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gym_System.Controllers
{
    public class CoachesController : Controller
    {
        private readonly IGymRepository<Coache> coacheRepository;

        private readonly IHostingEnvironment hosting;

        public CoachesController(IGymRepository<Coache> coacheRepository, IHostingEnvironment hosting)
        {
            this.coacheRepository = coacheRepository;
            this.hosting = hosting;
        }
        // GET: CoachesController
        public ActionResult Index(string SearchWord,int pageSize,int pageNumber ,string order)
        {
            if (order=="asc")
            {
                ViewBag.ascending = true;
                var list = coacheRepository.ListOfData();
                return View(list.OrderBy(e=>e.Full_Name));
            }
            if (order=="desc")
            {
                ViewBag.descending = true;
                var list = coacheRepository.ListOfData();
                return View(list.OrderByDescending(e => e.Full_Name));
            }
            if (pageSize > 0&&pageNumber>0) {
                ViewBag.pagesize = pageSize;
                ViewBag.pagenumber = pageNumber;
                var list = coacheRepository.ListOfData();
                return View(list.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }
            if (string.IsNullOrEmpty(SearchWord))
            {
                var list = coacheRepository.ListOfData();
                return View(list);
            }
            else {
                var list = coacheRepository.Search(SearchWord);
                ViewBag.search = SearchWord;
                return View(list);
            }

        }

        // GET: CoachesController/Details/5
        public ActionResult Details(int id)
        {
            var coache = coacheRepository.Find(id);
            return View(coache);
        }

        // GET: CoachesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoachesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Coache coache)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string path = hosting.WebRootPath;
                    Guid imageGuid = Guid.NewGuid();
                    string extension = Path.GetExtension(coache.File.FileName);
                    string imageFullName = imageGuid + extension;
                    coache.ImageUrl = imageFullName;
                    string imagePath = hosting.WebRootPath + "/Images/" + imageFullName;
                    using (FileStream fileStream=new FileStream(imagePath,FileMode.Create)) {
                        coache.File.CopyTo(fileStream);
                    }
                    coacheRepository.Add(coache);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "You must fill required felled");
            return View();
        }

        // GET: CoachesController/Edit/5
        public ActionResult Edit(int id)
        {
            var coache = coacheRepository.Find(id);
            return View(coache);
        }

        // POST: CoachesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Coache model)
        {
            try
            {
                string imageFullName = string.Empty;
                if (model.File != null)
                {
                    Guid imageGuid = Guid.NewGuid();
                    string extension = Path.GetExtension(model.File.FileName);
                     imageFullName = imageGuid + extension;
                    string newPath = hosting.WebRootPath + "/Images/" + imageFullName;
                    string oldPath = hosting.WebRootPath + "/Images/" + model.ImageUrl;
                    using (FileStream fileStream = new FileStream(newPath, FileMode.Create))
                    {
                        model.File.CopyTo(fileStream);
                        System.IO.File.Delete(oldPath);
                    }
                }else 
                {
                    imageFullName = model.ImageUrl;
                }
                    Coache coache = new Coache
                    {
                        Id = model.Id,
                        Full_Name = model.Full_Name,
                        PhoneNo = model.PhoneNo,
                        Salary = model.Salary,
                        ImageUrl = imageFullName
                    };
                    coacheRepository.Update(coache);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoachesController/Delete/5
        public ActionResult Delete(int id)
        {
            var coache = coacheRepository.Find(id);
            return View(coache);
        }

        // POST: CoachesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Coache coache)
        {
            try
            {
                var x = coacheRepository.Find(id);
                coacheRepository.Delete(id);
                string imagePath = hosting.WebRootPath + "/Images/" + x.ImageUrl;
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
