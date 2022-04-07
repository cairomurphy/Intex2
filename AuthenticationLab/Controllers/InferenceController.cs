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


        //Prediction for Severity
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

        //Prediction for Location
        [HttpGet]
        public IActionResult PredictionForm2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prediction2(LocationSeverity data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("boolean_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue2 = score.First() };
            ViewBag.PredictedValue2 = prediction.PredictedValue2;
            result.Dispose();
            return View("Prediction2");
        }

        //Prediction for Age
        [HttpGet]
        public IActionResult PredictionForm4()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prediction4(AgeSeverity data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("int_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue4 = score.First() };
            ViewBag.PredictedValue4 = prediction.PredictedValue4;
            result.Dispose();
            return View("Prediction4");
        }

        [HttpGet]
        public IActionResult PredictionForm3()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prediction3(TimeSeverity data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("boolean_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue3 = score.First() };
            ViewBag.PredictedValue3 = prediction.PredictedValue3;
            result.Dispose();
            return View("Prediction3");
        }
    }

        
}
