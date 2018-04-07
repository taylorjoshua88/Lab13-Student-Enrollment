using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Course
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string Iteration { get; set; }

        [Required]
        public int Level { get; set; }

        [Display(Name = "Starting Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Ending Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [EnumDataType(typeof(Technology))]
        [Required]
        public Technology Technology { get; set; }
    }

    public enum Technology
    {
        [Display(Name = "ASP.NET Core")] AspDotNetCore,
        [Display(Name = "JavaScript")] JavaScript,
        [Display(Name = "Mobile Applications")] MobileApp,
        [Display(Name = "Ruby on Rails")] Rails,
        [Display(Name = "General Development")] General,
        Python,
        Java
    }
}
