using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationLab.Models
{
    public class Crash
    {
        //WITH VALIDATION

        [Key]
        [Required(ErrorMessage = "Please enter an ID")]
        public long CRASH_ID { get; set; }
        [Required(ErrorMessage = "Please enter a date")]
        public string CRASH_DATETIME { get; set; }
        [Required(ErrorMessage = "Please enter a road name")]
        public string MAIN_ROAD_NAME { get; set; }
        [Required(ErrorMessage = "Please enter a city")]
        public string CITY { get; set; }
        [Required(ErrorMessage = "Please enter a county")]
        public string COUNTY_NAME { get; set; }

        [Required(ErrorMessage = "Please enter a route")]
        public string ROUTE { get; set; }
        [Required(ErrorMessage = "Please enter a milepoint (3.2)")]
        public double MILEPOINT { get; set; }
        public double LAT_UTM_Y { get; set; }
        public double LONG_UTM_X { get; set; }
        public string WORK_ZONE_RELATED { get; set; }
        public string PEDESTRIAN_INVOLVED { get; set; }
        public string BICYCLIST_INVOLVED { get; set; }
        public string MOTORCYCLE_INVOLVED { get; set; }
        public string IMPROPER_RESTRAINT { get; set; }
        public string UNRESTRAINED { get; set; }
        public string DUI { get; set; }
        public string INTERSECTION_RELATED { get; set; }
        public string WILD_ANIMAL_RELATED { get; set; }
        public string DOMESTIC_ANIMAL_RELATED { get; set; }
        public string OVERTURN_ROLLOVER { get; set; }
        public string COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }
        public string TEENAGE_DRIVER_INVOLVED { get; set; }
        public string OLDER_DRIVER_INVOLVED { get; set; }
        public string NIGHT_DARK_CONDITION { get; set; }
        public string SINGLE_VEHICLE { get; set; }
        public string DISTRACTED_DRIVING { get; set; }
        public string DROWSY_DRIVING { get; set; }
        public string ROADWAY_DEPARTURE { get; set; }

        //foreign key
        public int CRASH_SEVERITY_ID { get; set; }
        //public Severity Severity { get; set; }
    }
}
