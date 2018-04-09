using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<IActionResult> Index(Technology? technology, int? level,
            string nameFilter, string instructor)
        {
            // Construct queries for selection boxes
            IQueryable<Technology> technologyQuery =
                _context.Course.OrderBy(c => c.Technology)
                               .Select(c => c.Technology);

            IQueryable<int> levelQuery =
                _context.Course.OrderBy(c => c.Level)
                               .Select(c => c.Level);

            IQueryable<string> instructorQuery =
                _context.Course.OrderBy(c => c.Instructor)
                               .Select(c => c.Instructor);

            // Start query for filtering courses based on user input
            IQueryable<Course> coursesQuery = _context.Course.OrderBy(c => c.StartDate)
                                                             .Select(c => c);

            // Cumulatively apply filters to coursesQuery based on user input
            if (technology.HasValue)
            {
                coursesQuery = coursesQuery.Where(c => c.Technology == technology);
            }
            if (level.HasValue)
            {
                coursesQuery = coursesQuery.Where(c => c.Level == level);
            }
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                coursesQuery = coursesQuery.Where(c => c.Name.Contains(nameFilter));
            }
            if (!string.IsNullOrWhiteSpace(instructor))
            {
                coursesQuery = coursesQuery.Where(c => c.Instructor.Contains(instructor));
            }

            // Construct the view model
            CourseIndexViewModel coursesVM = new CourseIndexViewModel()
            {
                Instructors = new SelectList(await instructorQuery.Distinct().ToListAsync()),
                Levels = new SelectList(await levelQuery.Distinct().ToListAsync()),
                Courses = await coursesQuery.ToListAsync()
            };

            return View(coursesVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            CourseDetailViewModel courseDetailVM = new CourseDetailViewModel();

            // If an ID was provided, match the correct Course and select all
            // of the students enrolled in that course
            if (id.HasValue)
            {
                courseDetailVM.Course = 
                    await _context.Course.Where(c => c.ID == id).SingleAsync();

                courseDetailVM.Students = 
                    await _context.Student.Where(s => s.CurrentCourse == courseDetailVM.Course)
                                          .Select(s => s)
                                          .ToListAsync();
            }
            else
            {
                // If this action was selected without an ID, just redirect to Index
                return RedirectToAction("Index");
            }

            return View(courseDetailVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [Bind("ID,Name,Instructor,Technology,Iteration,Level,StartDate,EndDate")]
            Course course)
        {
            if (ModelState.IsValid)
            {
                EntityEntry<Course> newCourse = _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", newCourse.Entity.ID);
            }

            return View(course);
        }
    }
}