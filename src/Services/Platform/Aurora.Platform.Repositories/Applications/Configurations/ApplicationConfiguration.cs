using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Applications.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Applications.Models.ApplicationData>
    {
        public void Configure(EntityTypeBuilder<Domain.Applications.Models.ApplicationData> builder)
        {
            builder.ToTable("Application", "APP");

            builder.HasKey(e => e.ApplicationId).HasName("PK_Application");

            builder.Property(e => e.ApplicationId).HasColumnName("ApplicationId").IsRequired().HasColumnType("smallint").UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType("varchar(36)");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.HasCustomConfig).HasColumnName("HasCustomConfig").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.HasIndex(e => e.Code).IsUnique().HasDatabaseName("UK_Application");
        }
    }
}