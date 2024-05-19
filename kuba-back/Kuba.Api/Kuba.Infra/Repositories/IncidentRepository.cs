using Kuba.Domain.Interfaces;
using Kuba.Domain.Models;
using Kuba.Infra.Data;

namespace Kuba.Infra.Repositories
{
    public class IncidentRepository : BaseRepository<Incident>, IIncidentRepository
    {
        public IncidentRepository(ApiDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Incident>> GetIncidentsByUpdatedBy(Guid id)
        {
            return await FindAsync(i => i.UpdatedBy == id);
        }

        public async Task<IEnumerable<Incident>> GetIncidentsByUserId(Guid id)
        {
            return await FindAsync(i => i.UserId == id);
        }
    }
}
