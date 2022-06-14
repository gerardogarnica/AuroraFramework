using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Settings.Configurations
{
    public class AttributeValueConfiguration : IEntityTypeConfiguration<Domain.Settings.Models.AttributeValueData>
    {
        public void Configure(EntityTypeBuilder<Domain.Settings.Models.AttributeValueData> builder)
        {
            builder.ToTable("AttributeValue", "COM");

            builder.HasKey(e => e.AttributeId).HasName("PK_AttributeValue");

            builder.Property(e => e.AttributeId).HasColumnName("AttributeId").IsRequired().HasColumnType("int");
            builder.Property(e => e.RelationshipId).HasColumnName("RelationshipId").IsRequired().HasColumnType("int");
            builder.Property(e => e.Value).HasColumnName("Value").IsRequired().HasColumnType("xml");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.AttributeId, e.RelationshipId }).IsUnique().HasDatabaseName("UK_AttributeValue");

            builder.HasOne(e => e.AttributeSetting).WithMany(e => e.Values).HasForeignKey(e => e.AttributeId).HasConstraintName("FK_AttributeValue_AttributeSetting");
        }
    }
}