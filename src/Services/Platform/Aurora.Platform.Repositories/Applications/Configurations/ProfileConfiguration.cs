using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Applications.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Domain.Applications.Models.ProfileData>
    {
        public void Configure(EntityTypeBuilder<Domain.Applications.Models.ProfileData> builder)
        {
            builder.ToTable("Profile", "APP");

            builder.HasKey(e => e.ProfileId).HasName("PK_Profile");

            builder.Property(e => e.ProfileId).HasColumnName("ProfileId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.ApplicationId).HasColumnName("ApplicationId").IsRequired().HasColumnType("smallint");
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType("varchar(36)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.HasIndex(e => new { e.ApplicationId, e.Code }).IsUnique().HasDatabaseName("UK_Profile");

            builder.HasOne(e => e.Application).WithMany(e => e.Profiles).HasForeignKey(e => e.ApplicationId).HasConstraintName("FK_Profile_Application");
        }
    }
}