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
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace AuthenticationLab.Controllers
{
    public class HomeController : Controller
    {
        private InferenceSession _session;
        private ICrashRepository repo { get; set; }

        public HomeController(ICrashRepository temp, InferenceSession session)
        {
            repo = temp;
            _session = session;
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

        public IActionResult Safety()
        {
            return View();
        }

        public IActionResult Prediction()
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

        //onnx

        //[ApiController]
        //[Route("/score")]
       
        
        [HttpGet]
        public IActionResult PredictionForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Prediction(CrashSeverity data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("boolean_input", data.AsTensor())
            });
            Tensor<int> score = result.First().AsTensor<int>();
            var prediction = new Prediction { PredictedValue = score.First() * 100000 };
            result.Dispose();
            return Ok(prediction);
        }
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            //rootkit
        }
    }
}
