using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class CoursesViewModel
    {
        public SelectList Technologies { get; set; }
        public Technology? Technology { get; set; }

        public SelectList Levels { get; set; }
        public int? Level { get; set; }

        public string NameFilter { get; set; }

        /*
        [DataType(DataType.Date)]
        [Display(Prompt = "Starting Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Prompt = "Ending Date")]
        public DateTime EndDate { get; set; }
        */

        public List<Course> Courses { get; set; }
    }
}
