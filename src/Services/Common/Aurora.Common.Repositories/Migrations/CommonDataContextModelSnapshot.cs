﻿// <auto-generated />
using System;
using Aurora.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aurora.Common.Repositories.Migrations
{
    [DbContext(typeof(CommonDataContext))]
    partial class CommonDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("COM")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aurora.Common.Domain.Catalogs.Models.CatalogData", b =>
                {
                    b.Property<int>("CatalogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdCatalogo")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Codigo");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Descripcion");

                    b.Property<bool>("IsEditable")
                        .HasColumnType("bit")
                        .HasColumnName("EsEditable");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit")
                        .HasColumnName("EsVisible");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Nombre");

                    b.HasKey("CatalogId")
                        .HasName("PK_Catalogo");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("UK_Catalogo");

                    b.ToTable("Catalogo", "COM");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Catalogs.Models.CatalogItemData", b =>
                {
                    b.Property<int>("CatalogItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdCatalogoItem")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CatalogId")
                        .HasColumnType("int")
                        .HasColumnName("IdCatalogo");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Codigo");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("UsuarioCreacion");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("FechaCreacion");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Descripcion");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("EsActivo");

                    b.Property<bool>("IsEditable")
                        .HasColumnType("bit")
                        .HasColumnName("EsEditable");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("UsuarioModificacion");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("FechaModificacion");

                    b.HasKey("CatalogItemId")
                        .HasName("PK_CatalogoItem");

                    b.HasIndex("CatalogId", "Code")
                        .IsUnique()
                        .HasDatabaseName("UK_CatalogoItem");

                    b.ToTable("CatalogoItem", "COM");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Locations.Models.CountryData", b =>
                {
                    b.Property<short>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("IdPais")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InternetPrefix")
                        .HasColumnType("char(3)")
                        .HasColumnName("PrefijoInternet");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("EsActivo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Nombre");

                    b.Property<string>("OfficialName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("NombreOficial");

                    b.Property<string>("ThreeDigitsCode")
                        .IsRequired()
                        .HasColumnType("char(3)")
                        .HasColumnName("CodigoTresDigitos");

                    b.Property<string>("ThreeLettersCode")
                        .IsRequired()
                        .HasColumnType("char(3)")
                        .HasColumnName("CodigoTresLetras");

                    b.Property<string>("TwoLettersCode")
                        .IsRequired()
                        .HasColumnType("char(2)")
                        .HasColumnName("CodigoDosLetras");

                    b.HasKey("CountryId")
                        .HasName("PK_Pais");

                    b.HasIndex("ThreeLettersCode")
                        .IsUnique()
                        .HasDatabaseName("UK_Pais");

                    b.ToTable("Pais", "COM");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Locations.Models.CountryDivisionData", b =>
                {
                    b.Property<short>("DivisionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("IdPaisDivision")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("CountryId")
                        .HasColumnType("smallint")
                        .HasColumnName("IdPais");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("UsuarioCreacion");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("FechaCreacion");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("EsActivo");

                    b.Property<bool>("IsCityLevel")
                        .HasColumnType("bit")
                        .HasColumnName("EsNivelCiudad");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("UsuarioModificacion");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("FechaModificacion");

                    b.Property<int>("LevelNumber")
                        .HasColumnType("int")
                        .HasColumnName("Nivel");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Nombre");

                    b.HasKey("DivisionId")
                        .HasName("PK_PaisDivision");

                    b.HasIndex("CountryId");

                    b.ToTable("PaisDivision", "COM");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Locations.Models.LocationData", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdLocalidad")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlternativeCode")
                        .HasColumnType("varchar(10)")
                        .HasColumnName("CodigoAlterno");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(5)")
                        .HasColumnName("Codigo");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("UsuarioCreacion");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("FechaCreacion");

                    b.Property<short>("DivisionId")
                        .HasColumnType("smallint")
                        .HasColumnName("IdPaisDivision");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("EsActivo");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("UsuarioModificacion");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("FechaModificacion");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Nombre");

                    b.Property<int>("ParentLocationId")
                        .HasColumnType("int")
                        .HasColumnName("IdLocalidadPadre");

                    b.HasKey("LocationId")
                        .HasName("PK_Localidad");

                    b.HasIndex("DivisionId");

                    b.ToTable("Localidad", "COM");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Settings.Models.AttributeSettingData", b =>
                {
                    b.Property<int>("AttributeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdAtributo")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Codigo");

                    b.Property<string>("Configuration")
                        .IsRequired()
                        .HasColumnType("xml")
                        .HasColumnName("Configuracion");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("TipoDato");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Descripcion");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("EsActivo");

                    b.Property<bool>("IsEditable")
                        .HasColumnType("bit")
                        .HasColumnName("EsEditable");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit")
                        .HasColumnName("EsVisible");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Nombre");

                    b.Property<string>("ScopeType")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("TipoAmbito");

                    b.HasKey("AttributeId")
                        .HasName("PK_Atributo");

                    b.HasIndex("Code", "ScopeType")
                        .IsUnique()
                        .HasDatabaseName("UK_Atributo");

                    b.ToTable("Atributo", "COM");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Settings.Models.AttributeValueData", b =>
                {
                    b.Property<int>("AttributeId")
                        .HasColumnType("int")
                        .HasColumnName("IdAtributo");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("UsuarioCreacion");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("FechaCreacion");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("UsuarioModificacion");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("FechaModificacion");

                    b.Property<int>("RelationshipId")
                        .HasColumnType("int")
                        .HasColumnName("IdRelacion");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("xml")
                        .HasColumnName("Valor");

                    b.HasKey("AttributeId")
                        .HasName("PK_ValorAtributo");

                    b.HasIndex("AttributeId", "RelationshipId")
                        .IsUnique()
                        .HasDatabaseName("UK_ValorAtributo");

                    b.ToTable("ValorAtributo", "COM");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Catalogs.Models.CatalogItemData", b =>
                {
                    b.HasOne("Aurora.Common.Domain.Catalogs.Models.CatalogData", "Catalog")
                        .WithMany("Items")
                        .HasForeignKey("CatalogId")
                        .HasConstraintName("FK_CatalogoItem_Catalogo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Locations.Models.CountryDivisionData", b =>
                {
                    b.HasOne("Aurora.Common.Domain.Locations.Models.CountryData", "Country")
                        .WithMany("Divisions")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_PaisDivision_Pais")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Locations.Models.LocationData", b =>
                {
                    b.HasOne("Aurora.Common.Domain.Locations.Models.CountryDivisionData", "Division")
                        .WithMany("Locations")
                        .HasForeignKey("DivisionId")
                        .HasConstraintName("FK_Localidad_PaisDivision")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Settings.Models.AttributeValueData", b =>
                {
                    b.HasOne("Aurora.Common.Domain.Settings.Models.AttributeSettingData", "AttributeSetting")
                        .WithMany("Values")
                        .HasForeignKey("AttributeId")
                        .HasConstraintName("FK_ValorAtributo_Atributo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AttributeSetting");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Catalogs.Models.CatalogData", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Locations.Models.CountryData", b =>
                {
                    b.Navigation("Divisions");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Locations.Models.CountryDivisionData", b =>
                {
                    b.Navigation("Locations");
                });

            modelBuilder.Entity("Aurora.Common.Domain.Settings.Models.AttributeSettingData", b =>
                {
                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}
