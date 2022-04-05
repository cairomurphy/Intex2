using System;
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

        public IActionResult Data(string countyname, int severity, int pageNum = 1)
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
                //.Where(x => x.CRASH_SEVERITY_ID == severity || severity == null)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes =
                    (countyname == null
                    ? repo.mytable.Count()
                    : repo.mytable.Where(x => x.COUNTY_NAME == countyname).Count()),
                    //&&
                    //TotalNumCrashes =
                    //(severity == null
                    //? repo.mytable.Count()
                    //: repo.mytable.Where(x => x.CRASH_SEVERITY_ID == severity).Count()),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 10
                }
            };
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




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
