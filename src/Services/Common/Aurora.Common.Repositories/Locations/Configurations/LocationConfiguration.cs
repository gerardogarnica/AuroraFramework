using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Locations.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Domain.Locations.Models.LocationData>
    {
        public void Configure(EntityTypeBuilder<Domain.Locations.Models.LocationData> builder)
        {
            builder.ToTable("Localidad", "COM");

            builder.HasKey(e => e.LocationId).HasName("PK_Localidad");

            builder.Property(e => e.LocationId).HasColumnName("IdLocalidad").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.DivisionId).HasColumnName("IdPaisDivision").IsRequired().HasColumnType("smallint");
            builder.Property(e => e.ParentLocationId).HasColumnName("IdLocalidadPadre").HasColumnType("int");
            builder.Property(e => e.Name).HasColumnName("Nombre").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Code).HasColumnName("Codigo").HasColumnType("varchar(5)");
            builder.Property(e => e.AlternativeCode).HasColumnName("CodigoAlterno").HasColumnType("varchar(10)");
            builder.Property(e => e.IsActive).HasColumnName("EsActivo").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("UsuarioCreacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("UsuarioModificacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("FechaModificacion").IsRequired().HasColumnType("datetime");

            builder.HasOne(e => e.Division).WithMany(e => e.Locations).HasForeignKey(e => e.DivisionId).HasConstraintName("FK_Localidad_PaisDivision");
        }
    }
}