using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class StudentIndexViewModel
    {
        public string NameFilter { get; set; }
        public string CourseFilter { get; set; }

        public List<Student> Students { get; set; }

        public static async Task<StudentIndexViewModel>
            CreateViewModel(StudentEnrollmentDbContext context) =>
                new StudentIndexViewModel
                {
                    Students = await context.Student.Include(s => s.CurrentCourse)
                                                    .ToListAsync()
                };
    }
}
