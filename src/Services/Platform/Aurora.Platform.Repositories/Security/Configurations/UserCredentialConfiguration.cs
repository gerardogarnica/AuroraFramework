using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class UserCredentialConfiguration : IEntityTypeConfiguration<Domain.Security.Models.UserCredentialData>
    {
        public void Configure(EntityTypeBuilder<Domain.Security.Models.UserCredentialData> builder)
        {
            builder.ToTable("UserCredential", "SEC");

            builder.HasKey(e => e.UserId).HasName("PK_UserCredential");

            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType("int");
            builder.Property(e => e.Password).HasColumnName("Password").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.PasswordControl).HasColumnName("PasswordControl").IsRequired().HasColumnType("varchar(500)");
            builder.Property(e => e.MustChange).HasColumnName("MustChange").IsRequired().HasColumnType("bit");
            builder.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate").HasColumnType("datetime");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasOne(e => e.User).WithOne(e => e.Credential).HasConstraintName("FK_UserCredential_User");
        }
    }
}