using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace AuthenticationLab.Models
//{
//    public class TimeSeverity
//    {
//        public long night_dark_condition { get; set; }
//        public long drowsy_driving { get; set; }
//        public long month { get; set; }
//        public long day { get; set; }
//        public long hour { get; set; }

//        public Tensor<long> AsTensor()
//        {
//            long[] data = new long[]
//            {
//                night_dark_condition, drowsy_driving, month, day, hour
//            };
//            long[] dimensions = new long[] { 1, 5 };
//            return new DenseTensor<long>(data, dimensions);
//        }
//    }
//}
