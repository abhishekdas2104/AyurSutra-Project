
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AyurSutra.Data;
using AyurSutra.Models;

namespace AyurSutra.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            var patients = _context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p =>
                    p.FullName.Contains(searchString) ||
                    p.Phone.Contains(searchString));
            }

            ViewBag.SearchString = searchString;

            return View(patients.OrderByDescending(p => p.PatientId).ToList());
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            var patient = _context.Patients.Find(id);

            if (patient == null)
                return NotFound();

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient patient)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Patients.Update(patient);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            var patient = _context.Patients.Find(id);

            if (patient == null)
                return NotFound();

            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
