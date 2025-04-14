using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerRegistration.Models;

namespace VolunteerRegistration.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly VolunteerRegistrationContext _context;

        public RegistrationController(VolunteerRegistrationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var registrations = _context.Registrations
                .Include(r => r.Volunteer)
                .Include(r => r.Event)
                .ToList();

            return View(registrations);
        }

        // GET: Create Registration for Event
        public IActionResult Create(int eventId)
        {
            ViewBag.EventId = eventId;
            return View();
        }

        // POST: Create Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Volunteer volunteer, int eventId)
        {
            if (ModelState.IsValid)
            {
                var existingVolunteer = await _context.Volunteers
                    .FirstOrDefaultAsync(v => v.Email == volunteer.Email);

                if (existingVolunteer == null)
                {
                    await _context.Volunteers.AddAsync(volunteer);
                    await _context.SaveChangesAsync();
                    existingVolunteer = volunteer;
                }

                var registration = new Registration
                {
                    VolunteerId = existingVolunteer.Id,
                    EventId = eventId,
                    RegistrationDate = DateTime.Now
                };

                await _context.Registrations.AddAsync(registration);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.EventId = eventId;
            return View(volunteer);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var registration = await _context.Registrations
                .Include(r => r.Volunteer)
                .Include(r => r.Event)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null) return NotFound();

            return View(registration);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Registration registration)
        {
            if (id != registration.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Registrations.Update(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(registration);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var registration = await _context.Registrations
                .Include(r => r.Volunteer)
                .Include(r => r.Event)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null) return NotFound();

            return View(registration);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View(new List<Registration>());
        }

        [HttpPost]
        public IActionResult Search(string email)
        {
            var registrations = _context.Registrations
                .Include(r => r.Volunteer)
                .Include(r => r.Event)
                .Where(r => r.Volunteer.Email == email)
                .ToList();

            ViewBag.SearchedEmail = email;
            return View("Search", registrations);
        }
    }
}
