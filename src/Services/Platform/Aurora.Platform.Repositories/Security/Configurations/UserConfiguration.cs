using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Security.Models.UserData>
    {
        public void Configure(EntityTypeBuilder<Domain.Security.Models.UserData> builder)
        {
            builder.ToTable("User", "SEC");

            builder.HasKey(e => e.UserId).HasName("PK_User");

            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.LoginName).HasColumnName("LoginName").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.FirstName).HasColumnName("FirstName").IsRequired().HasColumnType("nvarchar(40)");
            builder.Property(e => e.LastName).HasColumnName("LastName").IsRequired().HasColumnType("nvarchar(40)");
            builder.Property(e => e.Email).HasColumnName("Email").IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(e => e.IsDefault).HasColumnName("IsDefault").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => e.LoginName).IsUnique().HasDatabaseName("UK_User");
        }
    }
}