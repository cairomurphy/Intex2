using AuthenticationLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore.Controllers
{
    public class InferenceController : Controller
    {
        private InferenceSession _session;

        public InferenceController(InferenceSession session)
        {
            _session = session;
        }

        [HttpGet]
        public IActionResult PredictionForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prediction(CrashSeverity data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("boolean_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            ViewBag.PredictedValue = prediction.PredictedValue;
            result.Dispose();
            return View("Prediction");
        }

        [HttpGet]
        public IActionResult PredictionForm2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prediction2(LocationSeverity data)
        {
            return View("Prediction2");
        }
    }

        
}
