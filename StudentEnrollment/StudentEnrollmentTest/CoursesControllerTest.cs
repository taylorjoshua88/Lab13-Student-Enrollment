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
    public class CoursesControllerTest
    {
        [Fact]
        public void CanGetIndexView()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase("TestDB")
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                CoursesController controller = new CoursesController(context);

                Assert.IsAssignableFrom<Task<IActionResult>>(controller.Index(null, null, null, null));
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
                CoursesController controller = new CoursesController(context);

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
                CoursesController controller = new CoursesController(context);

                Assert.IsAssignableFrom<IActionResult>(controller.Create());
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
                CoursesController controller = new CoursesController(context);

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
                CoursesController controller = new CoursesController(context);

                Assert.IsAssignableFrom<Task<IActionResult>>(controller.Delete(null));
            }
        }
    }
}
