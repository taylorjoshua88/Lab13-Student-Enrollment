using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class StudentEditViewModel
    {
        public IEnumerable<Course> AvailableCourses { get; set; }

        public Student Student { get; set; }

        public static async Task<StudentEditViewModel>
            CreateViewModel(int studentId, StudentEnrollmentDbContext context) =>
                new StudentEditViewModel()
                {
                    AvailableCourses = await context.Course.Select(c => c)
                                                           .ToListAsync(),

                    Student = await context.Student.Where(s => s.ID == studentId)
                                                   .Include(s => s.CurrentCourse)
                                                   .SingleAsync()
                };
    }
}
