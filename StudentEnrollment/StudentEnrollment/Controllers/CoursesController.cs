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

            // If an ID was provided then display the Details view, otherwise return to Index
            if (id.HasValue)
            {
                return View(await CourseDetailViewModel.FromIDAsync(id.Value, _context));
            }
            else
            {
                return RedirectToAction("Index");
            }
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

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // TODO: Inject logger into CoursesController for proper logging
                    TempData["NotificationType"] = "alert-danger";
                    TempData["NotificationMessage"] = 
                        "An error occurred while trying to create the course! Please try again.";

                    return View(course);
                }

                // Redirect to the Details view for the newly added course
                TempData["NotificationType"] = "alert-success";
                TempData["NotificationMessage"] = "Successfully added new course!";
                return RedirectToAction("Details", new { newCourse.Entity.ID });
            }

            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    return View(await _context.Course.Where(c => c.ID == id).SingleAsync());
                }
                catch (Exception)
                {
                    // Could not match id, display an error message and redirect to Index
                    TempData["NotificationType"] = "alert-danger";
                    TempData["NotificationMessage"] = "Could not find the specified course to edit!";
                    return RedirectToAction("Index");
                }
            }

            // Attempting to edit without an id will simply redirect to Index
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            [Bind("ID,Name,Instructor,Technology,Iteration,Level,StartDate,EndDate")]
            Course course)
        {
            if (ModelState.IsValid)
            {
                EntityEntry<Course> editCourse = _context.Update(course);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    TempData["NotificationType"] = "alert-danger";
                    TempData["NotificationMessage"] = $"Could not edit {course.Name}! Please try again.";
                    return View(course);
                }

                // Success! Notify the user and redirect to Details so the user can verify the changes
                TempData["NotificationType"] = "alert-success";
                TempData["NotificationMessage"] = $"Sucessfully modified {editCourse.Entity.Name}!";
                return RedirectToAction("Details", new { editCourse.Entity.ID });
            }

            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                Course course;
                int studentCount;

                try
                {
                    course = await _context.Course.Where(c => c.ID == id)
                                                  .SingleAsync();

                    // Get the number of students enrolled in this course
                    studentCount = await _context.Student.Where(s => s.CurrentCourse.ID == id)
                                                         .CountAsync();
                }
                catch (Exception)
                {
                    TempData["NotificationType"] = "alert-danger";
                    TempData["NotificationMessage"] = "Could not find the specified course to delete.";
                    return RedirectToAction("Index");
                }

                // Do not allow the removal of courses which still have students enrolled in them
                if (studentCount > 0)
                {
                    TempData["NotificationType"] = "alert-warning";
                    TempData["NotificationMessage"] = 
                        $"{course.Name} cannot be removed while students are still enrolled in the course.";
                    return RedirectToAction("Details", new { course.ID });
                }

                // Present a confirmation page to the user
                return View(course);
            }

            // If no id is provided then just redirect to Index
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> CommitDelete(int id)
        {
            Course course;

            try
            {
                course = _context.Course.Where(c => c.ID == id)
                                        .Single();
            }
            catch (Exception)
            {
                TempData["NotificationType"] = "alert-danger";
                TempData["NotificationMessage"] = "Could not find the specified course to delete.";
                return RedirectToAction("Index");
            }

            _context.Remove(course);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                TempData["NotificationType"] = "alert-danger";
                TempData["NotificationMessage"] = $"Could not delete {course.Name}! Please try again.";
                return View(course);
            }

            TempData["NotificationType"] = "alert-success";
            TempData["NotificationMessage"] = $"Sucessfully deleted the {course.Name} course!";
            return RedirectToAction("Index");
        }
    }
}