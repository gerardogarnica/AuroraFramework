using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Settings.Configurations
{
    public class AttributeValueConfiguration
    {
        public AttributeValueConfiguration(EntityTypeBuilder<Domain.Settings.Models.AttributeValueData> builder)
        {
            builder.ToTable("ValorAtributo", "COM");

            builder.HasKey(e => e.AttributeId).HasName("PK_ValorAtributo");

            builder.Property(e => e.AttributeId).HasColumnName("IdAtributo").IsRequired().HasColumnType("int");
            builder.Property(e => e.RelationshipId).HasColumnName("IdRelacion").IsRequired().HasColumnType("int");
            builder.Property(e => e.Value).HasColumnName("Valor").IsRequired().HasColumnType("xml");
            builder.Property(e => e.CreatedBy).HasColumnName("UsuarioCreacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("UsuarioModificacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("FechaModificacion").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.AttributeId, e.RelationshipId }).IsUnique().HasDatabaseName("UK_ValorAtributo");

            builder.HasOne(e => e.AttributeSetting).WithMany(e => e.Values).HasForeignKey(e => e.AttributeId).HasConstraintName("FK_ValorAtributo_Atributo");
        }
    }
}