﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;
using System.Data;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;
using WorkflowAutomation.Domain.Database;
using WorkflowAutomation.Persistence.EntityTypeConfigurations;

namespace WorkflowAutomation.Persistence
{
    public partial class DocumentsDbContext : DbContext, IDocumentUserDbContext
    {
        public DocumentsDbContext(DbContextOptions<DocumentsDbContext> options)
            : base(options) { }

        public virtual DbSet<AppUser> Users { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<DocumentContent> DocumentContents { get; set; } = null!;
        public virtual DbSet<DocumentStatus> DocumentStatuses { get; set; } = null!;
        public virtual DbSet<DocumentType> DocumentTypes { get; set; } = null!;
        public virtual DbSet<Key> Keys { get; set; } = null!;
        public virtual DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Subdivision> Subdivisions { get; set; } = null!;
        public virtual DbSet<UserPosition> UserPositions { get; set; } = null!;
        public virtual DbSet<UserSubdivision> UserSubdivisions { get; set; } = null!;
        private DbSet<AllowedSubdivision> AllowedSubdivisions { get; set; } = null!;

        public List<AllowedSubdivision> GetAllowedSubdivisions(int a)
        {
            NpgsqlParameter totalParameter = new NpgsqlParameter()
            {
                ParameterName = "@a",
                DbType = DbType.Int32,
                // Direction = ParameterDirection.Input
                Value = a
            };
            var result = AllowedSubdivisions.FromSqlRaw("CALL public.findallowedsubdivisions @a)", totalParameter).ToList();
            //var result = AllowedSubdivisions.FromSqlRaw($"WITH RECURSIVE r AS ( SELECT id_subdivision, id_subordination, name FROM subdivision WHERE id_subdivision = {a} UNION SELECT subdivision.id_subdivision, subdivision.id_subordination, subdivision.name  FROM subdivision   JOIN r  ON subdivision.id_subordination = r.id_subdivision) SELECT * FROM r;").ToList();
            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto");

            modelBuilder.Entity<AllowedSubdivision>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("app_user_pkey");

                entity.ToTable("app_user");

                entity.HasIndex(e => e.IdUser, "fki_app_user_asp_id_fkey");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.LastOnline)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("last_online");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(256)
                    .HasColumnName("patronymic");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("register_date");

                entity.Property(e => e.RemovalDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("removal_date");

                entity.Property(e => e.Surname)
                    .HasMaxLength(256)
                    .HasColumnName("surname");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithOne(p => p.AppUser)
                    .HasForeignKey<AppUser>(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("app_user_asp_id_fkey");
            });

            modelBuilder.Entity<AspNetDeviceCode>(entity =>
            {
                entity.HasKey(e => e.UserCode);

                entity.HasIndex(e => e.DeviceCode, "IX_DeviceCodes_DeviceCode")
                    .IsUnique();

                entity.HasIndex(e => e.Expiration, "IX_DeviceCodes_Expiration");

                entity.Property(e => e.UserCode).HasMaxLength(200);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.Data).HasMaxLength(50000);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.DeviceCode).HasMaxLength(200);

                entity.Property(e => e.SessionId).HasMaxLength(100);

                entity.Property(e => e.SubjectId).HasMaxLength(200);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.IdDocument)
                    .HasName("document_pkey");

                entity.ToTable("document");

                entity.HasIndex(e => e.IdSender, "IX_document_id_sender");

                entity.HasIndex(e => e.IdReceiver, "fki_document_id_receiver_fkey");

                entity.HasIndex(e => e.IdDocumentType, "fki_rtewte");

                entity.Property(e => e.IdDocument)
                    .HasColumnName("id_document")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date");

                entity.Property(e => e.IdDocumentType).HasColumnName("id_document_type");

                entity.Property(e => e.IdReceiver).HasColumnName("id_receiver");

                entity.Property(e => e.IdSender).HasColumnName("id_sender");

                entity.Property(e => e.RemoveDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("remove_date");

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .HasColumnName("title");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("update_date");

                entity.HasOne(d => d.IdDocumentTypeNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.IdDocumentType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_id_document_type_fkey");

                entity.HasOne(d => d.IdReceiverNavigation)
                    .WithMany(p => p.DocumentIdReceiverNavigations)
                    .HasForeignKey(d => d.IdReceiver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_id_receiver_fkey");

                entity.HasOne(d => d.IdSenderNavigation)
                    .WithMany(p => p.DocumentIdSenderNavigations)
                    .HasForeignKey(d => d.IdSender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_id_sender_fkey");
            });

            modelBuilder.Entity<DocumentContent>(entity =>
            {
                entity.HasKey(e => e.IdDocumentContent)
                    .HasName("document_content_pkey");

                entity.ToTable("document_content");

                entity.HasIndex(e => e.IdDocument, "fki_document_content_id_document_fkey");

                entity.Property(e => e.IdDocumentContent)
                    .HasColumnName("id_document_content")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdDocument).HasColumnName("id_document");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.HasOne(d => d.IdDocumentNavigation)
                    .WithMany(p => p.DocumentContents)
                    .HasForeignKey(d => d.IdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_content_id_document_fkey");
            });

            modelBuilder.Entity<DocumentStatus>(entity =>
            {
                entity.HasKey(e => e.IdDocumentStatus)
                    .HasName("document_status_pkey");

                entity.ToTable("document_status");

                entity.HasIndex(e => e.IdDocument, "IX_document_status_id_document");

                entity.HasIndex(e => e.IdStatus, "IX_document_status_id_status");

                entity.HasIndex(e => e.IdUser, "fki_document_status_id_user_fkey");

                entity.Property(e => e.IdDocumentStatus)
                    .HasColumnName("id_document_status")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AppropriationDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("appropriation_date");

                entity.Property(e => e.IdDocument).HasColumnName("id_document");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdDocumentNavigation)
                    .WithMany(p => p.DocumentStatuses)
                    .HasForeignKey(d => d.IdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_status_id_document_fkey");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.DocumentStatuses)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_status_id_status_fkey");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.DocumentStatuses)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_status_id_user_fkey");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.IdDocumentType)
                    .HasName("document_type_pkey");

                entity.ToTable("document_type");

                entity.Property(e => e.IdDocumentType)
                    .HasColumnName("id_document_type")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(256)
                    .HasColumnName("short_name");
            });

            modelBuilder.Entity<Key>(entity =>
            {
                entity.HasIndex(e => e.Use, "IX_Keys_Use");

                entity.Property(e => e.Algorithm).HasMaxLength(100);

                entity.Property(e => e.IsX509certificate).HasColumnName("IsX509Certificate");
            });

            modelBuilder.Entity<PersistedGrant>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.ConsumedTime, "IX_PersistedGrants_ConsumedTime");

                entity.HasIndex(e => e.Expiration, "IX_PersistedGrants_Expiration");

                entity.HasIndex(e => new { e.SubjectId, e.ClientId, e.Type }, "IX_PersistedGrants_SubjectId_ClientId_Type");

                entity.HasIndex(e => new { e.SubjectId, e.SessionId, e.Type }, "IX_PersistedGrants_SubjectId_SessionId_Type");

                entity.Property(e => e.Key).HasMaxLength(200);

                entity.Property(e => e.ClientId).HasMaxLength(200);

                entity.Property(e => e.Data).HasMaxLength(50000);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.SessionId).HasMaxLength(100);

                entity.Property(e => e.SubjectId).HasMaxLength(200);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPosition)
                    .HasName("position_pkey");

                entity.ToTable("position");

                entity.HasIndex(e => e.IdSubordination, "fki_position_id_subordination_fkey");

                entity.Property(e => e.IdPosition)
                    .HasColumnName("id_position")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IdSubordination).HasColumnName("id_subordination");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(256)
                    .HasColumnName("short_name");

                entity.HasOne(d => d.IdSubordinationNavigation)
                    .WithMany(p => p.InverseIdSubordinationNavigation)
                    .HasForeignKey(d => d.IdSubordination)
                    .HasConstraintName("position_id_subordination_fkey");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("status_pkey");

                entity.ToTable("status");

                entity.Property(e => e.IdStatus)
                    .HasColumnName("id_status")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Subdivision>(entity =>
            {
                entity.HasKey(e => e.IdSubdivision)
                    .HasName("subdivision_pkey");

                entity.ToTable("subdivision");

                entity.HasIndex(e => e.IdSubordination, "fki_subdivision_id_subordination_fkey");

                entity.Property(e => e.IdSubdivision)
                    .HasColumnName("id_subdivision")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreationDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("creation_date");

                entity.Property(e => e.IdSubordination).HasColumnName("id_subordination");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(256)
                    .HasColumnName("short_name");

                entity.HasOne(d => d.IdSubordinationNavigation)
                    .WithMany(p => p.InverseIdSubordinationNavigation)
                    .HasForeignKey(d => d.IdSubordination)
                    .HasConstraintName("subdivision_id_subordination_fkey");
            });

            modelBuilder.Entity<UserPosition>(entity =>
            {
                entity.HasKey(e => e.IdUserPosition)
                    .HasName("user_position_pkey");

                entity.ToTable("user_position");

                entity.HasIndex(e => e.IdPosition, "fki_user_position_id_position_fkey");

                entity.HasIndex(e => e.IdUser, "fki_user_position_id_user_fkey");

                entity.Property(e => e.IdUserPosition)
                    .HasColumnName("id_user_position")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("appointment_date");

                entity.Property(e => e.IdPosition).HasColumnName("id_position");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.RemovalDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("removal_date");

                entity.HasOne(d => d.IdPositionNavigation)
                    .WithMany(p => p.UserPositions)
                    .HasForeignKey(d => d.IdPosition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_position_id_position_fkey");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserPositions)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_position_id_user_fkey");
            });

            modelBuilder.Entity<UserSubdivision>(entity =>
            {
                entity.HasKey(e => e.IdUserSubdivision)
                    .HasName("user_subdivision_pkey");

                entity.ToTable("user_subdivision");

                entity.HasIndex(e => e.IdSubdivision, "fki_user_subdivision_id_subdivision_fkey");

                entity.HasIndex(e => e.IdUser, "fki_user_subdivision_id_user_fkey");

                entity.Property(e => e.IdUserSubdivision)
                    .HasColumnName("id_user_subdivision")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("appointment_date");

                entity.Property(e => e.IdSubdivision).HasColumnName("id_subdivision");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.RemovalDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("removal_date");

                entity.HasOne(d => d.IdSubdivisionNavigation)
                    .WithMany(p => p.UserSubdivisions)
                    .HasForeignKey(d => d.IdSubdivision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_subdivision_id_subdivision_fkey");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserSubdivisions)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_subdivision_id_user_fkey");
            });
        }
        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await this.SaveChangesAsync(cancellationToken);
        }

        DatabaseFacade Database { get { return this.Database; } }
    }
}
