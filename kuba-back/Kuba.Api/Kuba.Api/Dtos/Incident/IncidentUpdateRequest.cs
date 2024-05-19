using Kuba.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Kuba.Api.Dtos.Incident
{
    public class IncidentUpdateRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReportedDate { get; set; }
        public SeverityType? SeverityType { get; set; }
        public IncidentType? IncidentType { get; set; }
        [Required]
        public Guid UpdatedBy { get; set; }
    }
}
