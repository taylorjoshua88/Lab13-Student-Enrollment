using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            string nameFilter)
        {
            IQueryable<Technology> technologyQuery =
                _context.Course.OrderBy(c => c.Technology)
                               .Select(c => c.Technology);

            IQueryable<int> levelQuery =
                _context.Course.OrderBy(c => c.Level)
                               .Select(c => c.Level);

            IQueryable<Course> coursesQuery = _context.Course.OrderBy(c => c.StartDate)
                                                             .Select(c => c);

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

            CoursesViewModel coursesVM = new CoursesViewModel()
            {
                Technologies = new SelectList(await technologyQuery.Distinct().ToListAsync()),
                Levels = new SelectList(await levelQuery.Distinct().ToListAsync()),
                Courses = await coursesQuery.ToListAsync()
            };

            return View(coursesVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            CourseDetailViewModel courseDetailVM = new CourseDetailViewModel();

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
                return RedirectToAction("Index");
            }

            return View(courseDetailVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}