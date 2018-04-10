using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<IActionResult> Index(string nameFilter, string courseFilter)
        {
            return View(await StudentIndexViewModel.CreateViewModel(
                nameFilter, courseFilter, _context));
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
        public async Task<IActionResult> Create()
        {
            ViewData["AvailableCourses"] = await _context.Course.Select(c => c)
                                                                .ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,LastName,FirstName,EnrollmentDate,HighestCourseLevel,CurrentCourseId,PassedInterview,Placed")] Student student)
        {
            if (ModelState.IsValid)
            {
                // HACK: I cannot get the SelectList working properly after hours of research and trying literally EVERYTHING!!!
                //       This works for now.
                student.CurrentCourse = await _context.Course.Where(c => c.ID == student.CurrentCourseId).SingleAsync();
                EntityEntry<Student> createStudent = _context.Update(student);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    TempData["NotificationType"] = "alert-danger";
                    TempData["NotificationMessage"] = $"Could not create {student.FirstName} {student.LastName}! Please try again.";
                    return View(student);
                }

                TempData["NotificationType"] = "alert-success";
                TempData["NotificationMessage"] = $"Successfully added {student.FirstName} {student.LastName} to the database!";
                return RedirectToAction("Details", new { student.ID });
            }

            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    // Had to switch away from using view model due to validation problems
                    /*
                    StudentEditViewModel viewModel = await StudentEditViewModel.CreateViewModel(
                        id.Value, _context);
                    */

                    Student student = await _context.Student.Where(s => s.ID == id)
                                                            .Include(s => s.CurrentCourse)
                                                            .SingleAsync();

                    ViewData["AvailableCourses"] = await _context.Course.Select(c => c)
                                                                        .ToListAsync();

                    return View(student);
                }
                catch (Exception)
                {
                    // Could not match id, display an error message and redirect to Index
                    // TODO: Inject logger to perform proper logging
                    TempData["NotificationType"] = "alert-warning";
                    TempData["NotificationMessage"] = "Could not find the specified student to edit.";
                    return RedirectToAction("Index");
                }
            }

            // Attempted to edit a student without providing an id will simply redirect to Index
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind("ID,LastName,FirstName,EnrollmentDate,HighestCourseLevel,CurrentCourseId,PassedInterview,Placed")] Student student)
        {
            if (ModelState.IsValid)
            {
                // HACK: I cannot get the SelectList working properly after hours of research and trying literally EVERYTHING!!!
                //       This works for now.
                student.CurrentCourse = await _context.Course.Where(c => c.ID == student.CurrentCourseId).SingleAsync();
                EntityEntry<Student> editStudent = _context.Update(student);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    TempData["NotificationType"] = "alert-danger";
                    TempData["NotificationMessage"] = $"Could not edit {student.FirstName} {student.LastName}! Please try again.";
                    return View(student);
                }

                TempData["NotificationType"] = "alert-success";
                TempData["NotificationMessage"] = $"Successfully modified {student.FirstName} {student.LastName} in the database!";
                return RedirectToAction("Details", new { student.ID });
            }

            return View(student);
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

        [HttpPost]
        [ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommitRemove(int id)
        {
            Student student;

            try
            {
                student = await _context.Student.Where(s => s.ID == id)
                                                .SingleAsync();
            }
            catch (Exception)
            {
                // The student wasn't found, alert the user and redirect to index
                TempData["NotificationType"] = "alert-danger";
                TempData["NotificationMessage"] = "Could not find the specified student to remove!";
                return RedirectToAction("Index");
            }

            _context.Remove(student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // There was a database error. Allow the user a chance to retry
                // TODO: Inject logger to perform proper logging
                TempData["NotificationType"] = "alert-danger";
                TempData["NotificationMessage"] = "Unable to remove student from database! Please try again.";
                return View(student);
            }

            // Success! Notify the user and redirect to Index
            TempData["NotificationType"] = "alert-success";
            TempData["NotificationMessage"] = $"Sucessfully removed {student.FirstName} {student.LastName} from the database!";
            return RedirectToAction("Index");
        }
    }
}