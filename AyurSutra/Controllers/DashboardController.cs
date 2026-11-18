
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AyurSutra.Data;

namespace AyurSutra.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminUsername") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TotalPatients = _context.Patients.Count();
            ViewBag.TotalDoctors = _context.Doctors.Count();
            ViewBag.TotalTherapies = _context.Therapies.Count();
            ViewBag.TotalAppointments = _context.Appointments.Count();

            ViewBag.PendingAppointments = _context.Appointments
                .Count(a => a.Status == "Pending");

            ViewBag.CompletedTreatments = _context.TreatmentRecords
                .Count(r => r.Status == "Completed");

            return View();
        }
    }
}
