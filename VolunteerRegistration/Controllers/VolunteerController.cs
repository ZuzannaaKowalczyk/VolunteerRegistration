using Microsoft.AspNetCore.Mvc;
using VolunteerRegistration.Models;
using VolunteerRegistration.Repositories;


namespace VolunteerRegistration.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IRepository<Volunteer> _repository;

        public VolunteerController(IRepository<Volunteer> repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var volunteers = _repository.GetAll().ToList();
            return View(volunteers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(volunteer);
                await _repository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volunteer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var volunteer = await _repository.GetByIdAsync(id.Value);
            if (volunteer == null) return NotFound();

            return View(volunteer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Volunteer volunteer)
        {
            if (id != volunteer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(volunteer);
                await _repository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(volunteer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var volunteer = await _repository.GetByIdAsync(id.Value);
            if (volunteer == null) return NotFound();

            return View(volunteer);
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
