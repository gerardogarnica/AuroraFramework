using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Settings.Configurations
{
    public class AttributeSettingConfiguration : IEntityTypeConfiguration<Domain.Settings.Models.AttributeSettingData>
    {
        public void Configure(EntityTypeBuilder<Domain.Settings.Models.AttributeSettingData> builder)
        {
            builder.ToTable("AttributeSetting", "COM");

            builder.HasKey(e => e.AttributeId).HasName("PK_AttributeSetting");

            builder.Property(e => e.AttributeId).HasColumnName("AttributeId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(e => e.ScopeType).HasColumnName("ScopeType").IsRequired().HasColumnType("varchar(20)");
            builder.Property(e => e.DataType).HasColumnName("DataType").IsRequired().HasColumnType("varchar(10)");
            builder.Property(e => e.Configuration).HasColumnName("Configuration").IsRequired().HasColumnType("xml");
            builder.Property(e => e.IsVisible).HasColumnName("IsVisible").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsEditable).HasColumnName("IsEditable").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");

            builder.HasIndex(e => new { e.Code, e.ScopeType }).IsUnique().HasDatabaseName("UK_AttributeSetting");
        }
    }
}