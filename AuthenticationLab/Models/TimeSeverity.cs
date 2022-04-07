using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationLab.Models
{
    public class TimeSeverity
    {
        public int night_dark_condition { get; set; }
        public int drowsy_driving { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }

        public Tensor<int> AsTensor()
        {
            int[] data = new int[]
            {
                night_dark_condition, drowsy_driving, month, day, hour
            };
            int[] dimensions = new int[] { 1, 5 };
            return new DenseTensor<int>(data, dimensions);
        }
    }
}
