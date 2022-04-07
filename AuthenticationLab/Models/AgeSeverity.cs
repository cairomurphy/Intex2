using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationLab.Models
{
    public class AgeSeverity
    {
        public bool improper_restraint { get; set; }
        public bool unrestrained { get; set; }
        public bool dui { get; set; }
        public bool teenage_driver_involved { get; set; }
        public bool older_driver_involved { get; set; }

        public Tensor<bool> AsTensor()
        {
            bool[] data = new bool[]
            {
                improper_restraint, unrestrained, dui, teenage_driver_involved, older_driver_involved
            };
            int[] dimensions = new int[] { 1, 5 };
            return new DenseTensor<bool>(data, dimensions);
        }
    }
}
