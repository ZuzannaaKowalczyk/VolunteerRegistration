using Microsoft.AspNetCore.Mvc;
using VolunteerRegistration.Models;
using VolunteerRegistration.Repositories;

namespace VolunteerRegistration.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly IRepository<Organizer> _repository;

        public OrganizerController(IRepository<Organizer> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var organizers = _repository.GetAll().ToList();
            return View(organizers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Organizer organizer)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(organizer);
                await _repository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var organizer = await _repository.GetByIdAsync(id.Value);
            if (organizer == null) return NotFound();

            return View(organizer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Organizer organizer)
        {
            if (id != organizer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(organizer);
                await _repository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var organizer = await _repository.GetByIdAsync(id.Value);
            if (organizer == null) return NotFound();

            return View(organizer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
