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
            HomeController controller = new HomeController();

            Assert.IsAssignableFrom<IActionResult>(controller.Index());
        }

        [Fact]
        public void CanGetAboutView()
        {
            HomeController controller = new HomeController();

            Assert.IsAssignableFrom<IActionResult>(controller.About());
        }

        [Fact]
        public void CanGetContactView()
        {
            HomeController controller = new HomeController();

            Assert.IsAssignableFrom<IActionResult>(controller.Contact());
        }
    }
}
