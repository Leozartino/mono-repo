using Kuba.Domain.Interfaces;
using Kuba.Domain.Interfaces.Services;
using Kuba.Domain.Models;

namespace Kuba.Application.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<List<Incident>> GetAllIncidentsAsync()
        {
            var incidents = await _incidentRepository.GetAllAsync();

            return incidents;
        }

        public async Task<Incident> GetIncidentByIdAsync(Guid id)
        {
            var incident = await _incidentRepository.GetByIdAsync(id);

            return incident;

        }
    }
}
