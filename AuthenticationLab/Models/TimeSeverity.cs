using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationLab.Models
{
    public class TimeSeverity
    {
        public float night_dark_condition { get; set; }
        public float drowsy_driving { get; set; }
        public float month { get; set; }
        public float day { get; set; }
        public float hour { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                night_dark_condition, drowsy_driving, month, day, hour
            };
            //float[] dimensions = new float[] { 1, 5 };
            //return new DenseTensor<float>(data, dimensions);
            return null;
        }
    }
}
