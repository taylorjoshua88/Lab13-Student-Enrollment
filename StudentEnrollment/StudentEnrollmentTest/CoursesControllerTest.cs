using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Controllers;
using StudentEnrollment.Data;
using StudentEnrollment.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace StudentEnrollmentTest
{
    public class CoursesControllerTest
    {
        [Fact]
        public async void CanGetIndexWithViewModel()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                context.Course.Add(new Course()
                {
                    Name = "Underwater Basket Weaving.NET Core",
                    Technology = Technology.AspDotNetCore,
                    Instructor = "Tom Hanks",
                    Iteration = "underwater-seattle-901d1",
                    Level = 901,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today + TimeSpan.FromDays(70)
                });

                await context.SaveChangesAsync();

                CoursesController controller = new CoursesController(context);

                // Act
                ViewResult vr = await controller.Index(Technology.AspDotNetCore, 901, "Basket", "Hanks") as ViewResult;

                // Assert
                Assert.Single((vr.Model as CourseIndexViewModel).Courses);
            }
        }

        [Fact]
        public async void CanGetDetailsWithViewModel()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course newCourse = new Course()
                {
                    Name = "Underwater Basket Weaving.NET Core",
                    Technology = Technology.AspDotNetCore,
                    Instructor = "Tom Hanks",
                    Iteration = "underwater-seattle-901d1",
                    Level = 901,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today + TimeSpan.FromDays(70)
                };

                context.Course.Add(newCourse);
                await context.SaveChangesAsync();

                CoursesController controller = new CoursesController(context);

                // Act
                ViewResult vr = await controller.Details(newCourse.ID) as ViewResult;

                // Assert
                Assert.Equal("Underwater Basket Weaving.NET Core", (vr.Model as CourseDetailViewModel).Course.Name);
            }
        }

        [Fact]
        public void CanGetCreateView()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                CoursesController controller = new CoursesController(context);

                // Assert (would be null or another type if a ViewResult was not generated)
                Assert.IsType<ViewResult>(controller.Create());
            }
        }

        [Fact]
        public async void CanPostCreateEntity()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                CoursesController controller = new CoursesController(context);

                // Act
                await controller.Create(new Course()
                {
                    Name = "Underwater Basket Weaving.NET Core",
                    Technology = Technology.AspDotNetCore,
                    Instructor = "Tom Hanks",
                    Iteration = "underwater-seattle-901d1",
                    Level = 901,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today + TimeSpan.FromDays(70)
                });

                // Assert
                Assert.Single(context.Course);
            }
        }

        [Fact]
        public async void CanGetEditWithViewModel()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course()
                {
                    Name = "Underwater Basket Weaving.NET Core",
                    Technology = Technology.AspDotNetCore,
                    Instructor = "Tom Hanks",
                    Iteration = "underwater-seattle-901d1",
                    Level = 901,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today + TimeSpan.FromDays(70)
                };

                context.Course.Add(course);
                await context.SaveChangesAsync();

                CoursesController controller = new CoursesController(context);

                // Act
                ViewResult vr = await controller.Edit(course.ID) as ViewResult;

                // Assert
                Assert.Equal(course.Iteration, (vr.Model as Course).Iteration);
            }
        }

        [Fact]
        public async void CanPostEdit()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course()
                {
                    Name = "Underwater Basket Weaving.NET Core",
                    Technology = Technology.AspDotNetCore,
                    Instructor = "Tom Hanks",
                    Iteration = "underwater-seattle-901d1",
                    Level = 901,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today + TimeSpan.FromDays(70)
                };

                context.Course.Add(course);
                await context.SaveChangesAsync();

                CoursesController controller = new CoursesController(context);

                Course editedCourse = await context.Course.FirstAsync(c => c.ID == course.ID);
                editedCourse.Iteration = "underwater-seattle-901d2";

                // Act
                await controller.Edit(editedCourse);

                // Assert
                Assert.Equal("underwater-seattle-901d2",
                    (await context.Course.FirstAsync(c => c.ID == course.ID)).Iteration);
            }
        }

        [Fact]
        public async void CanGetDeleteWithViewModel()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course()
                {
                    Name = "Underwater Basket Weaving.NET Core",
                    Technology = Technology.AspDotNetCore,
                    Instructor = "Tom Hanks",
                    Iteration = "underwater-seattle-901d1",
                    Level = 901,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today + TimeSpan.FromDays(70)
                };

                context.Course.Add(course);
                await context.SaveChangesAsync();

                CoursesController controller = new CoursesController(context);

                // Act
                ViewResult vr = await controller.Delete(course.ID) as ViewResult;

                // Assert
                Assert.Equal("Tom Hanks", (vr.Model as Course).Instructor);
            }
        }

        [Fact]
        public async void CanPostDelete()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course()
                {
                    Name = "Underwater Basket Weaving.NET Core",
                    Technology = Technology.AspDotNetCore,
                    Instructor = "Tom Hanks",
                    Iteration = "underwater-seattle-901d1",
                    Level = 901,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today + TimeSpan.FromDays(70)
                };

                context.Course.Add(course);
                await context.SaveChangesAsync();

                int preDeleteCount = await context.Course.CountAsync();

                CoursesController controller = new CoursesController(context);

                // Act
                await controller.CommitDelete(course.ID);

                // Assert
                Assert.True(preDeleteCount > await context.Course.CountAsync());
            }
        }
    }
}
