using Kuba.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kuba.Infra.Configurations
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {

            builder.Property(x => x.Title).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();

            builder.Property(x => x.ReportedDate).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();

            // Relationship with the user who created the incident
            builder.HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with the user who last updated the incident
            builder.HasOne(i => i.AdmOrSupervisor)
                .WithMany()
                .HasForeignKey(i => i.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Incidents");
        }
    }
}
