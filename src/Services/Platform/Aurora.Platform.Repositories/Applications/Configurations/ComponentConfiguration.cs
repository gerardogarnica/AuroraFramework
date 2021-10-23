﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Applications.Configurations
{
    public class ComponentConfiguration
    {
        public ComponentConfiguration(EntityTypeBuilder<Domain.Applications.Models.ComponentData> builder)
        {
            builder.ToTable("Componente", "APP");

            builder.HasKey(e => e.ComponentId).HasName("PK_Componente");

            builder.Property(e => e.ComponentId).HasColumnName("IdComponente").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.ApplicationId).HasColumnName("IdAplicacion").IsRequired().HasColumnType("smallint");
            builder.Property(e => e.Code).HasColumnName("Codigo").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Description).HasColumnName("Descripcion").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.HasIndex(e => new { e.ApplicationId, e.Code }).IsUnique().HasDatabaseName("UK_Componente");

            builder.HasOne(e => e.Application).WithMany(e => e.Components).HasForeignKey(e => e.ApplicationId).HasConstraintName("FK_Componente_Aplicacion");
        }
    }
}