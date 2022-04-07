using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationLab.Models
{
    public class LocationSeverity
    {
        //public int id { get; set; }
        public bool intersection_related { get; set; }
        public bool distracted_driving { get; set; }
        public bool county_name_CACHE { get; set; }
        public bool county_name_CARBON { get; set; }
        public bool county_name_DAVIS { get; set; }
        public bool county_name_DUCHESNE { get; set; }
        public bool county_name_IRON { get; set; }
        public bool county_name_JUAB { get; set; }
        public bool county_name_MILLARD { get; set; }
        public bool county_name_OTHER { get; set; }
        public bool county_name_SALT_LAKE { get; set; }
        public bool county_name_SAN_JUAN { get; set; }
        public bool county_name_SANPETE { get; set; }
        public bool county_name_SEVIER { get; set; }
        public bool county_name_SUMMIT { get; set; }
        public bool county_name_TOOELE { get; set; }
        public bool county_name_UINTAH { get; set; }
        public bool county_name_UTAH { get; set; }
        public bool county_name_WASATCH { get; set; }
        public bool county_name_WASHINGTON { get; set; }
        public bool county_name_WEBER { get; set; }

        public Tensor<bool> AsTensor()
        {
            bool[] data = new bool[]
            {
                intersection_related, distracted_driving, county_name_CACHE, county_name_CARBON, county_name_DAVIS, county_name_DUCHESNE, county_name_IRON, county_name_JUAB, county_name_MILLARD, county_name_OTHER, county_name_SALT_LAKE, county_name_SANPETE, county_name_SAN_JUAN, county_name_SEVIER, county_name_SUMMIT, county_name_TOOELE, county_name_UINTAH, county_name_UTAH, county_name_WASATCH, county_name_WASHINGTON, county_name_WEBER
            };
            int[] dimensions = new int[] { 1, 21 };
            return new DenseTensor<bool>(data, dimensions);
        }
    }
}
