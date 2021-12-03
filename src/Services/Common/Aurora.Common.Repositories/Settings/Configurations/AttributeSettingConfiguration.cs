using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Settings.Configurations
{
    public class AttributeSettingConfiguration : IEntityTypeConfiguration<Domain.Settings.Models.AttributeSettingData>
    {
        public void Configure(EntityTypeBuilder<Domain.Settings.Models.AttributeSettingData> builder)
        {
            builder.ToTable("Atributo", "COM");

            builder.HasKey(e => e.AttributeId).HasName("PK_Atributo");

            builder.Property(e => e.AttributeId).HasColumnName("IdAtributo").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Codigo").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Name).HasColumnName("Nombre").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Descripcion").IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(e => e.ScopeType).HasColumnName("TipoAmbito").IsRequired().HasColumnType("varchar(20)");
            builder.Property(e => e.DataType).HasColumnName("TipoDato").IsRequired().HasColumnType("varchar(10)");
            builder.Property(e => e.Configuration).HasColumnName("Configuracion").IsRequired().HasColumnType("xml");
            builder.Property(e => e.IsVisible).HasColumnName("EsVisible").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsEditable).HasColumnName("EsEditable").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("EsActivo").IsRequired().HasColumnType("bit");

            builder.HasIndex(e => new { e.Code, e.ScopeType }).IsUnique().HasDatabaseName("UK_Atributo");
        }
    }
}