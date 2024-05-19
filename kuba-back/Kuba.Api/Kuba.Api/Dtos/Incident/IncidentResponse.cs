using Kuba.Domain.Enums;

namespace Kuba.Api.Dtos.Incident
{
    public class IncidentResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReportedDate { get; set; }
        public SeverityType SeverityType { get; set; }
        public IncidentType IncidentType { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
