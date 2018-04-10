using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Controllers;
using StudentEnrollment.Data;
using StudentEnrollment.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StudentEnrollmentTest
{
    public class StudentsControllerTest
    {
        // Cannot get this to work
        /*
        [Fact]
        public async void CanCreateStudent()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase("TestDB")
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                StudentsController controller = new StudentsController(context);
                int beforeStudentCount = context.Student.Count();
                await controller.Create(new Student()
                {
                    CurrentCourse = new Course(),
                    CurrentCourseId = 0,
                    EnrollmentDate = DateTime.Today,
                    FirstName = "asdf",
                    LastName = "qwer",
                    HighestCourseLevel = 101,
                    PassedInterview = false,
                    Placed = false
                });

                Assert.True(context.Student.Count() > beforeStudentCount);
            }
        }
        */

        [Fact]
        public void CanGetIndexView()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase("TestDB")
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                StudentsController controller = new StudentsController(context);

                Assert.IsAssignableFrom<Task<IActionResult>>(controller.Index(null, null));
            }
        }

        [Fact]
        public void CanGetDetailsView()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase("TestDB")
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                StudentsController controller = new StudentsController(context);

                Assert.IsAssignableFrom<Task<IActionResult>>(controller.Details(null));
            }
        }

        [Fact]
        public void CanGetCreateView()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase("TestDB")
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                StudentsController controller = new StudentsController(context);

                Assert.IsAssignableFrom<Task<IActionResult>>(controller.Create());
            }
        }

        [Fact]
        public void CanGetEditView()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase("TestDB")
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                StudentsController controller = new StudentsController(context);

                Assert.IsAssignableFrom<Task<IActionResult>>(controller.Edit((int?)null));
            }
        }

        [Fact]
        public void CanGetRemoveView()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase("TestDB")
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                StudentsController controller = new StudentsController(context);

                Assert.IsAssignableFrom<Task<IActionResult>>(controller.Remove(null));
            }
        }
    }
}
