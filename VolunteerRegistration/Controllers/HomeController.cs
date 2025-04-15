using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerRegistration.Models;
using VolunteerRegistration.Repositories.Interfaces;

namespace VolunteerRegistration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Event> _eventRepository;

        public HomeController(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IActionResult Index(string city)
        {
            var query = _eventRepository
                .GetAll()
                .Include(e => e.EventOrganizers)
                    .ThenInclude(eo => eo.Organizer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(e => e.Location.Contains(city));
            }

            ViewData["CurrentCity"] = city;

            var events = query.ToList();
            return View(events);
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}
