using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Configurations
{
    public class PlanConfigurations : IEntityTypeConfiguration<Plan>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Plan> builder)
        {
            builder.Property(X => X.Name)
                 .HasColumnType("varchar")
                 .HasMaxLength(50);
            builder.Property(X => X.Description).HasMaxLength(200);
            builder.Property(X => X.Price).HasPrecision(10, 2);
            builder.Property(X => X.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.ToTable(Tb =>
            {
                Tb.HasCheckConstraint("PlanDurationCheck", "DurationDays Between 1 and 365");
            });
        }

    }
}