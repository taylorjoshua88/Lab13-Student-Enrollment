using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Data;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentEnrollmentDbContext _context;

        public StudentsController(StudentEnrollmentDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await StudentIndexViewModel.CreateViewModel(_context));
        }
    }
}