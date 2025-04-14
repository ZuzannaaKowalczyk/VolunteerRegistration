using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerRegistration.Models;
using VolunteerRegistration.Models.ViewModels;

namespace VolunteerRegistration.Controllers
{
    public class EventController : Controller
    {
        private readonly VolunteerRegistrationContext _context;

        public EventController(VolunteerRegistrationContext context)
        {
            _context = context;
        }

        public IActionResult Index(string organizer)
        {
            var events = _context.Events
                .Include(e => e.EventOrganizers)
                    .ThenInclude(eo => eo.Organizer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(organizer))
            {
                events = events.Where(e =>
                    e.EventOrganizers.Any(eo =>
                        eo.Organizer.Name.Contains(organizer)));
            }

            ViewData["CurrentFilter"] = organizer;

            return View(events.ToList());
        }


        // GET: /Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventWithOrganizerViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Szukamy organizatora po e-mailu
                var organizer = _context.Organizers
                    .FirstOrDefault(o => o.Email == model.OrganizerEmail);

                if (organizer == null)
                {
                    organizer = new Organizer
                    {
                        Name = model.OrganizerName,
                        Email = model.OrganizerEmail,
                        Phone = model.OrganizerPhone
                    };

                    _context.Organizers.Add(organizer);
                    await _context.SaveChangesAsync();
                }

                // Zapisanie wydarzenia
                _context.Events.Add(model.Event);
                await _context.SaveChangesAsync();

                // Powiązanie w tabeli EventOrganizer
                var link = new EventOrganizer
                {
                    EventId = model.Event.Id,
                    OrganizerId = organizer.Id
                };

                _context.EventOrganizers.Add(link);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: /Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events.FindAsync(id.Value);
            if (ev == null) return NotFound();

            return View(ev);
        }

        // POST: /Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event ev)
        {
            if (id != ev.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Events.Update(ev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ev);
        }

        // GET: /Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events.FindAsync(id.Value);
            if (ev == null) return NotFound();

            return View(ev);
        }

        // POST: /Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev != null)
            {
                _context.Events.Remove(ev);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Volunteers(int id)
        {
            var registrations = _context.Registrations
                .Include(r => r.Volunteer)
                .Where(r => r.EventId == id)
                .ToList();

            ViewBag.EventName = _context.Events.FirstOrDefault(e => e.Id == id)?.EventName;
            return View(registrations);
        }
    }
}
