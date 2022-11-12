using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;
using WorkflowAutomation.Persistence.EntityTypeConfigurations;

namespace WorkflowAutomation.Persistence
{
    public class DocumentsDbContext : DbContext, IDocumentUserDbContext
    {
        public DocumentsDbContext(DbContextOptions<DocumentsDbContext> options)
            : base(options) { }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<DocumentContent> DocumentContents { get; set; } = null!;
        public virtual DbSet<DocumentType> DocumentTypes { get; set; } = null!;
        public virtual DbSet<DocumentUser> DocumentUsers { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Subdivision> Subdivisions { get; set; } = null!;
        public virtual DbSet<UserPosition> UserPositions { get; set; } = null!;
        public virtual DbSet<UserSubdivision> UserSubdivisions { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=workflow_automation;Username=postgres;Password=101001Zeus");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("user_pkey");

                entity.ToTable("app_user");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasDefaultValueSql("gen_random_uuid()");

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
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.IdDocument)
                    .HasName("document_pkey");

                entity.ToTable("document");

                entity.HasIndex(e => e.IdRoute, "fki_document_id_route_fkey");

                entity.HasIndex(e => e.IdStatus, "fki_document_id_status_fkey");

                entity.HasIndex(e => e.IdDocumentType, "fki_rtewte");

                entity.Property(e => e.IdDocument)
                    .HasColumnName("id_document")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date");

                entity.Property(e => e.IdDocumentType).HasColumnName("id_document_type");

                entity.Property(e => e.IdRoute).HasColumnName("id_route");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

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

                entity.HasOne(d => d.IdRouteNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.IdRoute)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_id_route_fkey");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_id_status_fkey");
            });

            modelBuilder.Entity<DocumentContent>(entity =>
            {
                entity.HasKey(e => e.IdDocumentContent)
                    .HasName("document_content_pkey");

                entity.ToTable("document_content");

                entity.HasIndex(e => e.IdDocument, "fki_document_content_id_document_fkey");

                entity.Property(e => e.IdDocumentContent)
                    .HasColumnName("id_document_content")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

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

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.IdDocumentType)
                    .HasName("document_type_pkey");

                entity.ToTable("document_type");

                entity.Property(e => e.IdDocumentType)
                    .HasColumnName("id_document_type")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DocumentUser>(entity =>
            {
                entity.HasKey(e => e.IdDocumentUser)
                    .HasName("document_user_pkey");

                entity.ToTable("document_user");

                entity.HasIndex(e => e.IdReceiver, "fki_document_user_id_receiver_fkey");

                entity.HasIndex(e => e.IdSender, "fki_document_user_id_sender_fkey");

                entity.Property(e => e.IdDocumentUser)
                    .HasColumnName("id_document_user")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.IdDocument).HasColumnName("id_document");

                entity.Property(e => e.IdReceiver).HasColumnName("id_receiver");

                entity.Property(e => e.IdSender).HasColumnName("id_sender");

                entity.Property(e => e.PreviousDocumentUser).HasColumnName("previous_document_user");

                entity.HasOne(d => d.IdDocumentNavigation)
                    .WithMany(p => p.DocumentUsers)
                    .HasForeignKey(d => d.IdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_user_id_document_fkey");

                entity.HasOne(d => d.IdReceiverNavigation)
                    .WithMany(p => p.DocumentUserIdReceiverNavigations)
                    .HasForeignKey(d => d.IdReceiver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_user_id_receiver_fkey");

                entity.HasOne(d => d.IdSenderNavigation)
                    .WithMany(p => p.DocumentUserIdSenderNavigations)
                    .HasForeignKey(d => d.IdSender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_user_id_sender_fkey");

                entity.HasOne(d => d.PreviousDocumentUserNavigation)
                    .WithMany(p => p.InversePreviousDocumentUserNavigation)
                    .HasForeignKey(d => d.PreviousDocumentUser)
                    .HasConstraintName("document_user_previous_document_user_fkey");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPosition)
                    .HasName("position_pkey");

                entity.ToTable("position");

                entity.HasIndex(e => e.IdSubordination, "fki_position_id_subordination_fkey");

                entity.Property(e => e.IdPosition)
                    .HasColumnName("id_position")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.IdSubordination).HasColumnName("id_subordination");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdSubordinationNavigation)
                    .WithMany(p => p.InverseIdSubordinationNavigation)
                    .HasForeignKey(d => d.IdSubordination)
                    .HasConstraintName("position_id_subordination_fkey");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(e => e.IdRoute)
                    .HasName("route_pkey");

                entity.ToTable("route");

                entity.Property(e => e.IdRoute)
                    .HasColumnName("id_route")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.CompleteDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("complete_date");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("status_pkey");

                entity.ToTable("status");

                entity.Property(e => e.IdStatus)
                    .HasColumnName("id_status")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

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
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.IdSubordination).HasColumnName("id_subordination");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

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
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

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
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

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
    }
}
