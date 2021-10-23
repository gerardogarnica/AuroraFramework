using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Locations.Configurations
{
    public class CountryDivisionConfiguration
    {
        public CountryDivisionConfiguration(EntityTypeBuilder<Domain.Locations.Models.CountryDivisionData> builder)
        {
            builder.ToTable("PaisDivision", "COM");

            builder.HasKey(e => e.DivisionId).HasName("PK_PaisDivision");

            builder.Property(e => e.DivisionId).HasColumnName("IdPaisDivision").IsRequired().HasColumnType("smallint").UseIdentityColumn();
            builder.Property(e => e.CountryId).HasColumnName("IdPais").IsRequired().HasColumnType("smallint");
            builder.Property(e => e.Name).HasColumnName("Nombre").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.LevelNumber).HasColumnName("Nivel").IsRequired().HasColumnType("int");
            builder.Property(e => e.IsCityLevel).HasColumnName("EsNivelCiudad").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("EsActivo").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("UsuarioCreacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("UsuarioModificacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("FechaModificacion").IsRequired().HasColumnType("datetime");

            builder.HasOne(e => e.Country).WithMany(e => e.Divisions).HasForeignKey(e => e.CountryId).HasConstraintName("FK_PaisDivision_Pais");
        }
    }
}