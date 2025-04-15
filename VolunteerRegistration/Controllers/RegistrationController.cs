using Microsoft.AspNetCore.Mvc;
using VolunteerRegistration.Models;
using VolunteerRegistration.Repositories.Interfaces;

namespace VolunteerRegistration.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationRepository _registrationRepo;
        private readonly IRepository<Volunteer> _volunteerRepo;

        public RegistrationController(
            IRegistrationRepository registrationRepo,
            IRepository<Volunteer> volunteerRepo)
        {
            _registrationRepo = registrationRepo;
            _volunteerRepo = volunteerRepo;
        }

        public async Task<IActionResult> Index()
        {
            var registrations = await _registrationRepo.GetAllWithDetailsAsync();
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
                var existingVolunteer = _volunteerRepo
                    .GetAll()
                    .FirstOrDefault(v => v.Email == volunteer.Email);

                if (existingVolunteer == null)
                {
                    await _volunteerRepo.CreateAsync(volunteer);
                    await _volunteerRepo.SaveAsync();
                    existingVolunteer = volunteer;
                }

                var registration = new Registration
                {
                    VolunteerId = existingVolunteer.Id,
                    EventId = eventId,
                    RegistrationDate = DateTime.Now
                };

                await _registrationRepo.CreateAsync(registration);
                await _registrationRepo.SaveAsync();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.EventId = eventId;
            return View(volunteer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var registration = await _registrationRepo.GetByIdWithDetailsAsync(id.Value);
            if (registration == null) return NotFound();

            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Registration registration)
        {
            if (id != registration.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _registrationRepo.UpdateAsync(registration);
                await _registrationRepo.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(registration);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var registration = await _registrationRepo.GetByIdWithDetailsAsync(id.Value);
            if (registration == null) return NotFound();

            return View(registration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _registrationRepo.DeleteAsync(id);
            await _registrationRepo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View(new List<Registration>());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string email)
        {
            var registrations = await _registrationRepo.GetByEmailWithDetailsAsync(email);
            ViewBag.SearchedEmail = email;
            return View("Search", registrations);
        }
    }
}
