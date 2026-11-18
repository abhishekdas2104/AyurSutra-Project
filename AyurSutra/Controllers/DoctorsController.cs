
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AyurSutra.Data;
using AyurSutra.Models;

namespace AyurSutra.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            var doctors = _context.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(d =>
                    d.FullName.Contains(searchString) ||
                    d.Specialization.Contains(searchString));
            }

            ViewBag.SearchString = searchString;

            return View(doctors.OrderByDescending(d => d.DoctorId).ToList());
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            var doctor = _context.Doctors.Find(id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Doctor doctor)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Doctors.Update(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            var doctor = _context.Doctors.Find(id);

            if (doctor == null)
                return NotFound();

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
