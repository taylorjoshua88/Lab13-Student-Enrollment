using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    return View(await _context.Student.Where(s => s.ID == id.Value)
                                                      .Include(s => s.CurrentCourse)
                                                      .SingleAsync());
                }
                catch (Exception)
                {
                    TempData["NotificationType"] = "alert-warning";
                    TempData["NotificationMessage"] = "Could not find the specified student!";
                    return RedirectToAction("Index");
                }
            }

            // If no id was provided just redirect to Index
            return RedirectToAction("Index");
        }
    }
}