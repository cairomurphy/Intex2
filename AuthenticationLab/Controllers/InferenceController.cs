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
    //CONTROLLER FOR ALL PREDICTIONS

    public class InferenceController : Controller
    {
        private InferenceSession _session;
        private InferenceSession _session2;
        private InferenceSession _session3;

        public InferenceController()
        {
            _session = new InferenceSession("wwwroot/crash_severity.onnx");
            _session2 = new InferenceSession("wwwroot/age_crash_severity.onnx");
            _session3 = new InferenceSession("wwwroot/distracted_crash_severity.onnx");
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
            var result = _session2.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("boolean_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            ViewBag.PredictedValue = prediction.PredictedValue;
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
            var result = _session3.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("boolean_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            ViewBag.PredictedValue = prediction.PredictedValue;
            result.Dispose();
            return View("Prediction4");
        }

        [HttpGet]
        public IActionResult PredictionForm3()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Prediction3(TimeSeverity data)
        //{
        //    var result = _session.Run(new List<NamedOnnxValue>
        //    {
        //        NamedOnnxValue.CreateFromTensor("int64_input", data.AsTensor())
        //    });
        //    Tensor<float> score = result.First().AsTensor<float>();
        //    var prediction = new Prediction { PredictedValue3 = score.First() };
        //    ViewBag.PredictedValue3 = prediction.PredictedValue3;
        //    result.Dispose();
        //    return View("Prediction3");
        //}
    }

        
}
