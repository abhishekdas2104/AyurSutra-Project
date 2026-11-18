
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AyurSutra.Data;
using AyurSutra.Models;

namespace AyurSutra.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("AdminUsername") != null;
        }

        private void LoadDropdowns()
        {
            ViewBag.Patients = new SelectList(
                _context.Patients.ToList(), "PatientId", "FullName");

            ViewBag.Doctors = new SelectList(
                _context.Doctors.ToList(), "DoctorId", "FullName");

            ViewBag.Therapies = new SelectList(
                _context.Therapies.ToList(), "TherapyId", "TherapyName");
        }

        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Therapy)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

            return View(appointments);
        }

        public IActionResult Create()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (!_context.Patients.Any(p => p.PatientId == appointment.PatientId) ||
                !_context.Doctors.Any(d => d.DoctorId == appointment.DoctorId) ||
                !_context.Therapies.Any(t => t.TherapyId == appointment.TherapyId))
            {
                ModelState.AddModelError("", "Please select a valid patient, doctor and therapy.");
            }

            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadDropdowns();
            return View(appointment);
        }

        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
                return NotFound();

            LoadDropdowns();
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Appointment appointment)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (!_context.Patients.Any(p => p.PatientId == appointment.PatientId) ||
                !_context.Doctors.Any(d => d.DoctorId == appointment.DoctorId) ||
                !_context.Therapies.Any(t => t.TherapyId == appointment.TherapyId))
            {
                ModelState.AddModelError("", "Please select a valid patient, doctor and therapy.");
            }

            if (ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadDropdowns();
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
