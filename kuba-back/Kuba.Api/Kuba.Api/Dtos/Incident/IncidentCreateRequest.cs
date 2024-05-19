using Kuba.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Kuba.Api.Dtos.Incident
{
    public class IncidentCreateRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ReportedDate { get; set; }
        [Required]
        public SeverityType SeverityType { get; set; }
        [Required]
        public IncidentType IncidentType { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
