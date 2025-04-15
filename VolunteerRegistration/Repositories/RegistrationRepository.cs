using VolunteerRegistration.Models;
using Microsoft.EntityFrameworkCore;
using VolunteerRegistration.Repositories.Interfaces;

namespace VolunteerRegistration.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly VolunteerRegistrationContext _context;

        public RegistrationRepository(VolunteerRegistrationContext context)
        {
            _context = context;
        }

        public IQueryable<Registration> GetAll()
        {
            return _context.Registrations;
        }

        public async Task<Registration> GetByIdAsync(int id)
        {
            return await _context.Registrations.FindAsync(id);
        }

        public async Task CreateAsync(Registration entity)
        {
            await _context.Registrations.AddAsync(entity);
        }

        public Task UpdateAsync(Registration model)
        {
            _context.Registrations.Update(model);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Registrations.FindAsync(id);
            if (entity != null)
            {
                _context.Registrations.Remove(entity);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Rozszerzone metody:

        public async Task<List<Registration>> GetAllWithDetailsAsync()
        {
            return await _context.Registrations
                .Include(r => r.Volunteer)
                .Include(r => r.Event)
                .ToListAsync();
        }

        public async Task<Registration?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Registrations
                .Include(r => r.Volunteer)
                .Include(r => r.Event)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Registration>> GetByEmailWithDetailsAsync(string email)
        {
            return await _context.Registrations
                .Include(r => r.Volunteer)
                .Include(r => r.Event)
                .Where(r => r.Volunteer.Email == email)
                .ToListAsync();
        }
    }
}
