using VolunteerRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace VolunteerRegistration.Repositories
{
    public class OrganizerRepository : IRepository<Organizer>
    {
        private readonly VolunteerRegistrationContext _context;

        public OrganizerRepository(VolunteerRegistrationContext context)
        {
            _context = context;
        }

        public IQueryable<Organizer> GetAll()
        {
            return _context.Organizers;
        }

        public async Task<Organizer> GetByIdAsync(int id)
        {
            return await _context.Organizers.FindAsync(id);
        }

        public async Task CreateAsync(Organizer entity)
        {
            await _context.Organizers.AddAsync(entity);
        }

        public Task UpdateAsync(Organizer model)
        {
            _context.Organizers.Update(model);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Organizers.FindAsync(id);
            if (entity != null)
            {
                _context.Organizers.Remove(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
