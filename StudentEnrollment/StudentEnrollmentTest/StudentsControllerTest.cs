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
                StudentsController controller = new StudentsController(context);

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

                Student student = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Bob",
                    LastName = "Bobbert",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                context.Student.Add(student);
                await context.SaveChangesAsync();

                // Act
                ViewResult vr = await controller.Index(null, null) as ViewResult;

                // Assert
                Assert.Single((vr.Model as StudentIndexViewModel).Students);
            }
        }

        [Fact]
        public async void CanGetFilteredIndexWithViewModel()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                StudentsController controller = new StudentsController(context);

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

                Student student = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Bob",
                    LastName = "Bobbert",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                Student student2 = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Dave",
                    LastName = "Davidson",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                context.Student.Add(student);
                context.Student.Add(student2);
                await context.SaveChangesAsync();

                // Act
                ViewResult vr = await controller.Index("Dave", "Underwater") as ViewResult;

                // Assert
                Assert.Single((vr.Model as StudentIndexViewModel).Students);
            }
        }

        [Fact]
        public async void CanGetDetails()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                StudentsController controller = new StudentsController(context);

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

                Student student = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Bob",
                    LastName = "Bobbert",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                context.Student.Add(student);
                await context.SaveChangesAsync();

                // Act
                ViewResult vr = await controller.Details(student.ID) as ViewResult;

                // Assert
                Assert.Equal("Tom Hanks", (vr.Model as Student).CurrentCourse.Instructor);
            }
        }

        [Fact]
        public async void CanGetCreate()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                StudentsController controller = new StudentsController(context);

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

                Student student = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Bob",
                    LastName = "Bobbert",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                context.Student.Add(student);
                await context.SaveChangesAsync();

                // Assert
                Assert.IsType<ViewResult>(await controller.Create());
            }
        }

        [Fact]
        public async void CanPostCreate()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                StudentsController controller = new StudentsController(context);

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

                Student student = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Bob",
                    LastName = "Bobbert",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                // Act
                await controller.Create(student);

                // Assert
                Assert.Single(context.Student);
            }
        }

        [Fact]
        public async void CanGetEdit()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                StudentsController controller = new StudentsController(context);

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

                Student student = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Bob",
                    LastName = "Bobbert",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                context.Student.Add(student);
                await context.SaveChangesAsync();

                // Act
                ViewResult vr = await controller.Edit(student.ID) as ViewResult;

                // Assert
                Assert.Equal("Bob", (vr.Model as Student).FirstName);
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
                StudentsController controller = new StudentsController(context);

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

                Student student = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Bob",
                    LastName = "Bobbert",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                context.Student.Add(student);
                await context.SaveChangesAsync();

                Student modifiedStudent = await context.Student.FirstAsync(s => s.ID == student.ID);
                modifiedStudent.LastName = "Robertson";

                // Act
                await controller.Edit(modifiedStudent);

                // Assert
                Assert.Equal("Robertson", (await context.Student.FirstAsync(s => s.ID == student.ID)).LastName);
            }
        }

        [Fact]
        public async void CanGetRemoveWithViewModel()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                StudentsController controller = new StudentsController(context);

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

                Student student = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Bob",
                    LastName = "Bobbert",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                context.Student.Add(student);
                await context.SaveChangesAsync();

                // Act
                ViewResult vr = await controller.Remove(student.ID) as ViewResult;

                // Assert
                Assert.Equal(401, (vr.Model as Student).HighestCourseLevel);
            }
        }

        [Fact]
        public async void CanPostRemove()
        {
            DbContextOptions<StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<StudentEnrollmentDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            using (StudentEnrollmentDbContext context = new StudentEnrollmentDbContext(options))
            {
                // Arrange
                StudentsController controller = new StudentsController(context);

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

                Student student = new Student()
                {
                    CurrentCourseId = newCourse.ID,
                    FirstName = "Bob",
                    LastName = "Bobbert",
                    EnrollmentDate = DateTime.Today,
                    HighestCourseLevel = 401,
                    PassedInterview = false,
                    Placed = false
                };

                context.Student.Add(student);
                await context.SaveChangesAsync();

                // Act
                await controller.CommitRemove(student.ID);

                // Assert
                Assert.Empty(context.Student);
            }
        }
    }
}
