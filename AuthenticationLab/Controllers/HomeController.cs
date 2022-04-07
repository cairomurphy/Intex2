﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthenticationLab.Models;
using AuthenticationLab.Models.ViewModels;
using System.Text;

namespace AuthenticationLab.Controllers
{
    public class HomeController : Controller
    {
        private ICrashRepository repo { get; set; }

        public HomeController(ICrashRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Statistics()
        {
            return View();
        }

        public IActionResult Data(string countyname, int pageNum = 1)
        {
            StringBuilder QParam = new StringBuilder();
            if (pageNum != 0)
            {
                QParam.Append($"Page=-");
            }


            int pageSize = 50;

            var x = new CrashesViewModel
            {
                mytable = repo.mytable
                .Where(x => x.COUNTY_NAME == countyname || countyname == null)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes =
                    (countyname == null
                    ? repo.mytable.Count()
                    : repo.mytable.Where(x => x.COUNTY_NAME == countyname).Count()),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 10
                }
            };

            //var movies = from m in _context.Movie
            //             select m;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    movies = movies.Where(s => s.Title!.Contains(searchString));
            //}

            //return View(await movies.ToListAsync());
            return View(x);
        }

        public IActionResult PredictionForm()
        {
            return View();
        }

        public IActionResult Details(long crashid)
        {
            var x = repo.mytable
                .Single(x => x.CRASH_ID == crashid);
            return View(x);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Crash());
        }

        [HttpPost]
        public IActionResult Add(Crash c)
        {
            if (ModelState.IsValid)
            {
                repo.CreateCrash(c);
                repo.SaveCrash(c);
                return RedirectToAction("Data");
            }
            else
            {
                return View(c);
            }
        }

        [HttpGet]
        public IActionResult Edit(int crashid)
        {
            var x = repo.mytable.Single(i => i.CRASH_ID == crashid);

            return View("Add", x);
        }

        [HttpPost]
        public IActionResult Edit(Crash c)
        {
            repo.UpdateCrash(c);
            repo.SaveCrash(c);

            return RedirectToAction("Data");
        }


        [HttpGet]
        public IActionResult Delete(int crashid)
        {
            var x = repo.mytable.Single(i => i.CRASH_ID == crashid);

            return View(x);
        }

        [HttpPost]
        public IActionResult Delete(Crash c)
        {
            repo.DeleteCrash(c);
            return RedirectToAction("Data");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            //rootkit
        }
    }
}
