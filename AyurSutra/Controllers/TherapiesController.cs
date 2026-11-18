
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AyurSutra.Data;
using AyurSutra.Models;

namespace AyurSutra.Controllers
{
    public class TherapiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TherapiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            var therapies = _context.Therapies.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                therapies = therapies.Where(t =>
                    t.TherapyName.Contains(searchString));
            }

            ViewBag.SearchString = searchString;

            return View(therapies.OrderByDescending(t => t.TherapyId).ToList());
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Therapy therapy)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Therapies.Add(therapy);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(therapy);
        }

        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            var therapy = _context.Therapies.Find(id);

            if (therapy == null)
                return NotFound();

            return View(therapy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Therapy therapy)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Therapies.Update(therapy);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(therapy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
                return RedirectToAction("Login", "Account");

            var therapy = _context.Therapies.Find(id);

            if (therapy == null)
                return NotFound();

            _context.Therapies.Remove(therapy);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
