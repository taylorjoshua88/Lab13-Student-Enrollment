using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
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

        public static async Task<CourseDetailViewModel> FromIDAsync(int id, StudentEnrollmentDbContext context)
        {
            CourseDetailViewModel courseDetailVM = new CourseDetailViewModel();

            courseDetailVM.Course = 
                await context.Course.Where(c => c.ID == id).SingleAsync();

            courseDetailVM.Students = 
                await context.Student.Where(s => s.CurrentCourse == courseDetailVM.Course)
                                     .Select(s => s)
                                     .ToListAsync();

            return courseDetailVM;
        }
    }
}
