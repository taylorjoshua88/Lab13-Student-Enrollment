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
            CreateViewModel(string nameFilter, string courseFilter, 
                            StudentEnrollmentDbContext context)
        {
            StudentIndexViewModel viewModel = new StudentIndexViewModel();

            IQueryable<Student> studentsQuery = context.Student.Select(s => s);

            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.FirstName.Contains(nameFilter) ||
                                                         s.LastName.Contains(nameFilter));
            }
            if (!string.IsNullOrWhiteSpace(courseFilter))
            {
                studentsQuery = studentsQuery.Where(s => s.CurrentCourse.Name.Contains(courseFilter));
            }

            viewModel.Students = await studentsQuery.Include(s => s.CurrentCourse)
                                                    .OrderBy(s => s.LastName)
                                                    .ToListAsync();

            return viewModel;
        }
    }
}
