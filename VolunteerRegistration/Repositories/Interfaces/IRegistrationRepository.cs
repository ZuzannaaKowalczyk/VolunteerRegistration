using VolunteerRegistration.Models;

namespace VolunteerRegistration.Repositories.Interfaces
{
    public interface IRegistrationRepository : IRepository<Registration>
    {
        Task<List<Registration>> GetAllWithDetailsAsync();
        Task<Registration?> GetByIdWithDetailsAsync(int id);
        Task<List<Registration>> GetByEmailWithDetailsAsync(string email);
    }
}
