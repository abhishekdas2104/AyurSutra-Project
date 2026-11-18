
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AyurSutra.Data;
using AyurSutra.Models;

namespace AyurSutra.Controllers
{
    public class TreatmentRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TreatmentRecordsController(ApplicationDbContext context)
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

            ViewBag.Therapies = new SelectList(
                _context.Therapies.ToList(), "TherapyId", "TherapyName");
        }

        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var records = _context.TreatmentRecords
                .Include(r => r.Patient)
                .Include(r => r.Therapy)
                .OrderByDescending(r => r.TreatmentDate)
                .ToList();

            return View(records);
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
        public IActionResult Create(TreatmentRecord record)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (!_context.Patients.Any(p => p.PatientId == record.PatientId) ||
                !_context.Therapies.Any(t => t.TherapyId == record.TherapyId))
            {
                ModelState.AddModelError("", "Please select a valid patient and therapy.");
            }

            if (ModelState.IsValid)
            {
                _context.TreatmentRecords.Add(record);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadDropdowns();
            return View(record);
        }

        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var record = _context.TreatmentRecords.Find(id);

            if (record == null)
                return NotFound();

            LoadDropdowns();
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TreatmentRecord record)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (!_context.Patients.Any(p => p.PatientId == record.PatientId) ||
                !_context.Therapies.Any(t => t.TherapyId == record.TherapyId))
            {
                ModelState.AddModelError("", "Please select a valid patient and therapy.");
            }

            if (ModelState.IsValid)
            {
                _context.TreatmentRecords.Update(record);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadDropdowns();
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var record = _context.TreatmentRecords.Find(id);

            if (record == null)
                return NotFound();

            _context.TreatmentRecords.Remove(record);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
