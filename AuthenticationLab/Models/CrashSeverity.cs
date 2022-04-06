using Microsoft.ML.OnnxRuntime.Tensors;

namespace AuthenticationLab.Models
{
    public class CrashSeverity
    {
        public bool pedestrian_involved { get; set; }
        public bool bicyclist_involved { get; set; }
        public bool motorcycle_involved { get; set; }
        public bool improper_restraint { get; set; }
        public bool unrestrained { get; set; }
        public bool dui { get; set; }
        public bool intersection_related { get; set; }
        public bool overturn_rollover { get; set; }
        public bool single_vehicle { get; set; }
        public bool roadway_departure { get; set; }

        public Tensor<bool> AsTensor()
        {
            bool[] data = new bool[]
            {
                pedestrian_involved, bicyclist_involved, motorcycle_involved, improper_restraint, unrestrained, dui, intersection_related, overturn_rollover, single_vehicle, roadway_departure
            };
            int[] dimensions = new int[] { 1, 10 };
            return new DenseTensor<bool>(data, dimensions);
        }
    }
}