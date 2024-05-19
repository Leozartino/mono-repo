 using Kuba.Domain.Models;

namespace Kuba.Domain.Interfaces.Services
{
    public interface IIncidentService
    {
        Task<Incident> GetIncidentByIdAsync(Guid id);

        Task<List<Incident>> GetAllIncidentsAsync();

    }
}
