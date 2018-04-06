using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Data;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class CoursesController : Controller
    {
        private readonly StudentEnrollmentDbContext _context;

        public CoursesController(StudentEnrollmentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Course);
        }
    }
}