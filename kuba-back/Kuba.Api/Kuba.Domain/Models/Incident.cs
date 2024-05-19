using Kuba.Domain.Enums;

namespace Kuba.Domain.Models
{
    public class Incident : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReportedDate { get; set; }
        public SeverityType SeverityType { get; set; }
        public IncidentType IncidentType { get; set; }

        // Navigation property
        public ApplicationUser User { get; set; }
        public ApplicationUser AdmOrSupervisor { get; set; }

        // Foreign key
        public Guid UserId { get; set; }
        public Guid? UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
