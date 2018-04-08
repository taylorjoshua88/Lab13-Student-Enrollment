using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentEnrollment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Data
{
    public class SeedCourseData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StudentEnrollmentDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<StudentEnrollmentDbContext>>()))
            {
                if (context.Course.Any())
                {
                    return;
                }

                context.Course.AddRange(
                    new Course
                    {
                        Name = "Advanced ASP.NET Core",
                        Instructor = "Iverson",
                        Level = 401,
                        StartDate = DateTime.Parse("2018-3-19"),
                        EndDate = DateTime.Parse("2018-5-25"),
                        Iteration = "seattle-dotnet-401d3",
                        Technology = Technology.AspDotNetCore
                    },

                    new Course
                    {
                        Name = "Advanced Software Development in Python",
                        Instructor = "Jacobson",
                        Level = 401,
                        StartDate = DateTime.Parse("2018-3-19"),
                        EndDate = DateTime.Parse("2018-5-25"),
                        Iteration = "seattle-python-401d8",
                        Technology = Technology.Python
                    },

                    new Course
                    {
                        Name = "Intro to Software Development & Careers in Tech",
                        Instructor = "Rodman",
                        Level = 101,
                        StartDate = DateTime.Parse("2018-4-7"),
                        EndDate = DateTime.Parse("2018-4-7"),
                        Iteration = "seattle-101d32",
                        Technology = Technology.General
                    },

                    new Course
                    {
                        Name = "Intermediate Software Development",
                        Instructor = "Neumann",
                        Level = 301,
                        StartDate = DateTime.Parse("2018-4-9"),
                        EndDate = DateTime.Parse("2018-5-4"),
                        Iteration = "seattle-301d33",
                        Technology = Technology.General
                    },

                    new Course
                    {
                        Name = "Master Data Structures & Algorithms",
                        Instructor = "Van Truck",
                        Level = 501,
                        StartDate = DateTime.Parse("2018-4-14"),
                        EndDate = DateTime.Parse("2018-5-19"),
                        Iteration = "bellevue-dsa-501n1",
                        Technology = Technology.General
                    },

                    new Course
                    {
                        Name = "Advanced Software Development in Full-Stack JavaScript",
                        Instructor = "Thoreau",
                        Level = 401,
                        StartDate = DateTime.Parse("2018-4-16"),
                        EndDate = DateTime.Parse("2018-6-22"),
                        Iteration = "seattle-javascript-d23",
                        Technology = Technology.JavaScript
                    },

                    new Course
                    {
                        Name = "Foundations of Software Development",
                        Instructor = "Pieterson",
                        Level = 201,
                        StartDate = DateTime.Parse("2018-4-30"),
                        EndDate = DateTime.Parse("2018-5-25"),
                        Iteration = "seattle-201d35",
                        Technology = Technology.General
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
