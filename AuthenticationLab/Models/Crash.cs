using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationLab.Models
{
    public class Crash
    {

        [Key]
        [Required]
        public long CRASH_ID { get; set; }
        public string CRASH_DATETIME { get; set; }
        public string MAIN_ROAD_NAME { get; set; }
        public string CITY { get; set; }
        public string COUNTY_NAME { get; set; }

        //foreign key
        public int CRASH_SEVERITY_ID { get; set; }
        public Severity Severity { get; set; }
    }
}
