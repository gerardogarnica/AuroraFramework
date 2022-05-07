﻿// <auto-generated />
using System;
using Aurora.Platform.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aurora.Platform.Repositories.Migrations
{
    [DbContext(typeof(PlatformDataContext))]
    partial class PlatformDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("APP")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aurora.Platform.Domain.Applications.Models.ApplicationData", b =>
                {
                    b.Property<short>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("ApplicationId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasColumnName("Code");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.Property<bool>("HasCustomConfig")
                        .HasColumnType("bit")
                        .HasColumnName("HasCustomConfig");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("ApplicationId")
                        .HasName("PK_Application");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("UK_Application");

                    b.ToTable("Application", "APP");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Applications.Models.ComponentData", b =>
                {
                    b.Property<int>("ComponentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ComponentId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("ApplicationId")
                        .HasColumnType("smallint")
                        .HasColumnName("ApplicationId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Code");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.HasKey("ComponentId")
                        .HasName("PK_Component");

                    b.HasIndex("ApplicationId", "Code")
                        .IsUnique()
                        .HasDatabaseName("UK_Component");

                    b.ToTable("Component", "APP");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Applications.Models.ConnectionData", b =>
                {
                    b.Property<int>("ConnectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ConnectionId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComponentId")
                        .HasColumnType("int")
                        .HasColumnName("ComponentId");

                    b.Property<string>("ConnString")
                        .IsRequired()
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("ConnString");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<bool>("IsEncrypted")
                        .HasColumnType("bit")
                        .HasColumnName("IsEncrypted");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int")
                        .HasColumnName("ProfileId");

                    b.HasKey("ConnectionId")
                        .HasName("PK_Connection");

                    b.HasIndex("ProfileId", "ComponentId")
                        .IsUnique()
                        .HasDatabaseName("UK_Connection");

                    b.ToTable("Connection", "APP");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Applications.Models.ProfileData", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProfileId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("ApplicationId")
                        .HasColumnType("smallint")
                        .HasColumnName("ApplicationId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasColumnName("Code");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.HasKey("ProfileId")
                        .HasName("PK_Profile");

                    b.HasIndex("ApplicationId", "Code")
                        .IsUnique()
                        .HasDatabaseName("UK_Profile");

                    b.ToTable("Profile", "APP");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.RoleData", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit")
                        .HasColumnName("IsDefault");

                    b.Property<bool>("IsGlobal")
                        .HasColumnType("bit")
                        .HasColumnName("IsGlobal");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int")
                        .HasColumnName("ProfileId");

                    b.HasKey("RoleId")
                        .HasName("PK_Role");

                    b.HasIndex("ProfileId", "Name")
                        .IsUnique()
                        .HasDatabaseName("UK_Role");

                    b.ToTable("Role", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.UserCredentialData", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("ExpirationDate");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<bool>("MustChange")
                        .HasColumnType("bit")
                        .HasColumnName("MustChange");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Password");

                    b.Property<string>("PasswordControl")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnName("PasswordControl");

                    b.HasKey("UserId")
                        .HasName("PK_UserCredential");

                    b.ToTable("UserCredential", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.UserCredentialLogData", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LogId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChangeNumber")
                        .HasColumnType("int")
                        .HasColumnName("ChangeNumber");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime")
                        .HasColumnName("EndDate");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("ExpirationDate");

                    b.Property<bool>("MustChange")
                        .HasColumnType("bit")
                        .HasColumnName("MustChange");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Password");

                    b.Property<string>("PasswordControl")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnName("PasswordControl");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("LogId")
                        .HasName("PK_UserCredentialLog");

                    b.HasIndex("UserId");

                    b.ToTable("UserCredentialLog", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.UserData", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("FirstName");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit")
                        .HasColumnName("IsDefault");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("LastName");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("LoginName");

                    b.HasKey("UserId")
                        .HasName("PK_User");

                    b.HasIndex("LoginName")
                        .IsUnique()
                        .HasDatabaseName("UK_User");

                    b.ToTable("User", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.UserMembershipData", b =>
                {
                    b.Property<int>("MembershipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MembershipId")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit")
                        .HasColumnName("IsDefault");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("MembershipId")
                        .HasName("PK_UserMembership");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique()
                        .HasDatabaseName("UK_UserMembership");

                    b.ToTable("UserMembership", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Applications.Models.ComponentData", b =>
                {
                    b.HasOne("Aurora.Platform.Domain.Applications.Models.ApplicationData", "Application")
                        .WithMany("Components")
                        .HasForeignKey("ApplicationId")
                        .HasConstraintName("FK_Component_Application")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Applications.Models.ConnectionData", b =>
                {
                    b.HasOne("Aurora.Platform.Domain.Applications.Models.ProfileData", "Profile")
                        .WithMany("Connections")
                        .HasForeignKey("ProfileId")
                        .HasConstraintName("FK_Connection_Profile")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Applications.Models.ProfileData", b =>
                {
                    b.HasOne("Aurora.Platform.Domain.Applications.Models.ApplicationData", "Application")
                        .WithMany("Profiles")
                        .HasForeignKey("ApplicationId")
                        .HasConstraintName("FK_Profile_Application")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.UserCredentialData", b =>
                {
                    b.HasOne("Aurora.Platform.Domain.Security.Models.UserData", "User")
                        .WithOne("Credential")
                        .HasForeignKey("Aurora.Platform.Domain.Security.Models.UserCredentialData", "UserId")
                        .HasConstraintName("FK_UserCredential_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.UserCredentialLogData", b =>
                {
                    b.HasOne("Aurora.Platform.Domain.Security.Models.UserCredentialData", "Credential")
                        .WithMany("CredentialLogs")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserCredentialLog_UserCredential")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Credential");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.UserMembershipData", b =>
                {
                    b.HasOne("Aurora.Platform.Domain.Security.Models.RoleData", "Role")
                        .WithMany("Memberships")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserMembership_Role")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurora.Platform.Domain.Security.Models.UserData", "User")
                        .WithMany("Memberships")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserMembership_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Applications.Models.ApplicationData", b =>
                {
                    b.Navigation("Components");

                    b.Navigation("Profiles");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Applications.Models.ProfileData", b =>
                {
                    b.Navigation("Connections");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.RoleData", b =>
                {
                    b.Navigation("Memberships");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.UserCredentialData", b =>
                {
                    b.Navigation("CredentialLogs");
                });

            modelBuilder.Entity("Aurora.Platform.Domain.Security.Models.UserData", b =>
                {
                    b.Navigation("Credential");

                    b.Navigation("Memberships");
                });
#pragma warning restore 612, 618
        }
    }
}
