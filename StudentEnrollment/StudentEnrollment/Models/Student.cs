using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Current Course")]
        public Course CurrentCourse { get; set; }

        [Required]
        public int CurrentCourseId { get; set; }

        [Display(Name = "Highest Level Completed")]
        [Required]
        public int HighestCourseLevel { get; set; } = 0;

        [Display(Name = "Passed Qualifying Interview")]
        [Required]
        public bool PassedInterview { get; set; } = false;

        [Display(Name = "Placed Professionally")]
        [Required]
        public bool Placed { get; set; } = false;
    }
}
