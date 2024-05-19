using Kuba.Domain.Models;

namespace Kuba.Domain.Interfaces
{
    public interface IIncidentRepository : IRepository<Incident>
    {
        Task<IEnumerable<Incident>> GetIncidentsByUpdatedBy(Guid id);

        Task<IEnumerable<Incident>> GetIncidentsByUserId(Guid id);

    }
}
