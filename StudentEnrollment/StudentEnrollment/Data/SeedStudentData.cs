using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentEnrollment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Data
{
    public class SeedStudentData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StudentEnrollmentDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<StudentEnrollmentDbContext>>()))
            {
                if (context.Student.Any())
                {
                    return;
                }

                context.Student.AddRange(
                    new Student
                    {
                        FirstName = "Bob",
                        LastName = "Bobberson",
                        EnrollmentDate = DateTime.Parse("2018-3-14"),
                        HighestCourseLevel = 0,
                        PassedInterview = false,
                        Placed = false,
                        CurrentCourse = context.Course.FirstOrDefault()
                    },

                    new Student
                    {
                        FirstName = "Wise",
                        LastName = "Guy",
                        EnrollmentDate = DateTime.Parse("2018-3-23"),
                        HighestCourseLevel = 101,
                        PassedInterview = false,
                        Placed = false,
                        CurrentCourse = context.Course.LastOrDefault()
                    },

                    new Student
                    {
                        FirstName = "Smooth",
                        LastName = "Criminal",
                        EnrollmentDate = DateTime.Parse("2018-2-11"),
                        HighestCourseLevel = 201,
                        PassedInterview = false,
                        Placed = false,
                        CurrentCourse = context.Course.FirstOrDefault()
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
