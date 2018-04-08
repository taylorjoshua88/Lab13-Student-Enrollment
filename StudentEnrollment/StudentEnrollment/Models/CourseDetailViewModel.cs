using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class CourseDetailViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public Course Course { get; set; }
    }
}
