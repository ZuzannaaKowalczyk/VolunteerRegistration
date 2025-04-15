using VolunteerRegistration.Models;
using Microsoft.EntityFrameworkCore;
using VolunteerRegistration.Repositories.Interfaces;

namespace VolunteerRegistration.Repositories
{
    public class EventRepository : IRepository<Event>
    {
        private readonly VolunteerRegistrationContext _context;

        public EventRepository(VolunteerRegistrationContext context)
        {
            _context = context;
        }

        public IQueryable<Event> GetAll()
        {
            return _context.Events;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task CreateAsync(Event entity)
        {
            await _context.Events.AddAsync(entity);
        }

        public Task UpdateAsync(Event model)
        {
            _context.Events.Update(model);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Events.FindAsync(id);
            if (entity != null)
            {
                _context.Events.Remove(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
