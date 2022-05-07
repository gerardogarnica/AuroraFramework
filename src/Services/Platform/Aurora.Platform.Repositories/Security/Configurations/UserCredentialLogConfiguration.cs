using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class UserCredentialLogConfiguration : IEntityTypeConfiguration<Domain.Security.Models.UserCredentialLogData>
    {
        public void Configure(EntityTypeBuilder<Domain.Security.Models.UserCredentialLogData> builder)
        {
            builder.ToTable("UserCredentialLog", "SEC");

            builder.HasKey(e => e.LogId).HasName("PK_UserCredentialLog");

            builder.Property(e => e.LogId).HasColumnName("LogId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType("int");
            builder.Property(e => e.ChangeNumber).HasColumnName("ChangeNumber").IsRequired().HasColumnType("int");
            builder.Property(e => e.Password).HasColumnName("Password").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.PasswordControl).HasColumnName("PasswordControl").IsRequired().HasColumnType("varchar(500)");
            builder.Property(e => e.MustChange).HasColumnName("MustChange").IsRequired().HasColumnType("bit");
            builder.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate").HasColumnType("datetime");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.EndDate).HasColumnName("EndDate").HasColumnType("datetime");

            builder.HasOne(e => e.Credential).WithMany(e => e.CredentialLogs).HasForeignKey(e => e.UserId).HasConstraintName("FK_UserCredentialLog_UserCredential");
        }
    }
}