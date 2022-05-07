using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Domain.Security.Models.RoleData>
    {
        public void Configure(EntityTypeBuilder<Domain.Security.Models.RoleData> builder)
        {
            builder.ToTable("Role", "SEC");

            builder.HasKey(e => e.RoleId).HasName("PK_Role");

            builder.Property(e => e.RoleId).HasColumnName("RoleId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.IsDefault).HasColumnName("IsDefault").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsGlobal).HasColumnName("IsGlobal").IsRequired().HasColumnType("bit");
            builder.Property(e => e.ProfileId).HasColumnName("ProfileId").IsRequired().HasColumnType("int");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.ProfileId, e.Name }).IsUnique().HasDatabaseName("UK_Role");
        }
    }
}