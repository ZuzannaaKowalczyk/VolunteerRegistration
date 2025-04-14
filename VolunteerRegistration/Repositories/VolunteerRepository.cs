using VolunteerRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace VolunteerRegistration.Repositories
{
    public class VolunteerRepository : IRepository<Volunteer>
    {
        private readonly VolunteerRegistrationContext _context;

        public VolunteerRepository(VolunteerRegistrationContext context)
        {
            _context = context;
        }

        public IQueryable<Volunteer> GetAll()
        {
            return _context.Volunteers;
        }

        public async Task<Volunteer> GetByIdAsync(int id)
        {
            return await _context.Volunteers.FindAsync(id);
        }

        public async Task CreateAsync(Volunteer entity)
        {
            await _context.Volunteers.AddAsync(entity);
        }

        public Task UpdateAsync(Volunteer model)
        {
            _context.Volunteers.Update(model);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Volunteers.FindAsync(id);
            if (entity != null)
            {
                _context.Volunteers.Remove(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
