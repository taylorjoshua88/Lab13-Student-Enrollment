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
            // The Index action has no logic, model, or view model
            // associated with it. Make sure the proper 200 reponse
            // is returned (IActionResult is ViewResult)
            Assert.IsType<ViewResult>(controller.Index());
        }

        [Fact]
        public void CanGetAboutView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Assert
            // The About action has no logic, model, or view model
            // associated with it. Make sure the proper 200 reponse
            // is returned (IActionResult is ViewResult)
            Assert.IsType<ViewResult>(controller.About());
        }

        [Fact]
        public void CanGetContactView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Assert
            // The Contact action has no logic, model, or view model
            // associated with it. Make sure the proper 200 reponse
            // is returned (IActionResult is ViewResult)
            Assert.IsType<ViewResult>(controller.Contact());
        }
    }
}
