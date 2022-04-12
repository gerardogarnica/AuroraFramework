using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Applications.Configurations
{
    public class ConnectionConfiguration : IEntityTypeConfiguration<Domain.Applications.Models.ConnectionData>
    {
        public void Configure(EntityTypeBuilder<Domain.Applications.Models.ConnectionData> builder)
        {
            builder.ToTable("Connection", "APP");

            builder.HasKey(e => e.ConnectionId).HasName("PK_Connection");

            builder.Property(e => e.ConnectionId).HasColumnName("ConnectionId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.ProfileId).HasColumnName("ProfileId").IsRequired().HasColumnType("int");
            builder.Property(e => e.ComponentId).HasColumnName("ComponentId").IsRequired().HasColumnType("int");
            builder.Property(e => e.ConnString).HasColumnName("ConnString").IsRequired().HasColumnType("varchar(1000)");
            builder.Property(e => e.IsEncrypted).HasColumnName("IsEncrypted").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.ProfileId, e.ComponentId }).IsUnique().HasDatabaseName("UK_Connection");

            builder.HasOne(e => e.Profile).WithMany(e => e.Connections).HasForeignKey(e => e.ProfileId).HasConstraintName("FK_Connection_Profile");
        }
    }
}