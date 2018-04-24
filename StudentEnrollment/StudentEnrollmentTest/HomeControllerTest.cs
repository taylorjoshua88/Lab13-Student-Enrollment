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
    public class HomeControllerTest
    {
        [Fact]
        public void CanGetIndexView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Assert
            Assert.IsType<ViewResult>(controller.Index());
        }

        [Fact]
        public void CanGetAboutView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Assert
            Assert.IsType<ViewResult>(controller.Index());
        }

        [Fact]
        public void CanGetContactView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Assert
            Assert.IsType<ViewResult>(controller.Index());
        }
    }
}
