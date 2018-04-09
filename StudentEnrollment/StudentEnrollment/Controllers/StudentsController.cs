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

        [HttpGet]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id.HasValue)
            {
                Student student;

                try
                {
                    student = await _context.Student.Where(s => s.ID == id)
                                                    .Include(s => s.CurrentCourse)
                                                    .SingleAsync();
                }
                catch (Exception)
                {
                    // The student was not found, notify the user
                    // TODO: Inject logger to provide proper logging of this
                    TempData["NotificationType"] = "alert-danger";
                    TempData["NotificationMessage"] = "Could not find the specified student to remove!";
                    return RedirectToAction("Index");
                }

                // Present a confirmation page to the user
                return View(student);
            }

            // If no id is provided then just redirect to Index
            return RedirectToAction("Index");
        }
    }
}