using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Applications.Configurations
{
    public class ComponentConfiguration : IEntityTypeConfiguration<Domain.Applications.Models.ComponentData>
    {
        public void Configure(EntityTypeBuilder<Domain.Applications.Models.ComponentData> builder)
        {
            builder.ToTable("Component", "APP");

            builder.HasKey(e => e.ComponentId).HasName("PK_Component");

            builder.Property(e => e.ComponentId).HasColumnName("ComponentId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.ApplicationId).HasColumnName("ApplicationId").IsRequired().HasColumnType("smallint");
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.HasIndex(e => new { e.ApplicationId, e.Code }).IsUnique().HasDatabaseName("UK_Component");

            builder.HasOne(e => e.Application).WithMany(e => e.Components).HasForeignKey(e => e.ApplicationId).HasConstraintName("FK_Component_Application");
        }
    }
}