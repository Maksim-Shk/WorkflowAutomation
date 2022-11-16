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

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Document> Documents { get; set; }

        public virtual DbSet<DocumentContent> DocumentContents { get; set; }

        public virtual DbSet<DocumentStatus> DocumentStatuses { get; set; }

        public virtual DbSet<DocumentType> DocumentTypes { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Subdivision> Subdivisions { get; set; }

        public virtual DbSet<UserPosition> UserPositions { get; set; }

        public virtual DbSet<UserSubdivision> UserSubdivisions { get; set; }

        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=workflow_automation;Username=postgres;Password=101001Zeus");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser).HasName("user_pkey");

                entity.ToTable("app_user");

                entity.Property(e => e.IdUser)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id_user");
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
                entity.HasKey(e => e.IdDocument).HasName("document_pkey");

                entity.ToTable("document");

                entity.HasIndex(e => e.IdReceiver, "fki_document_id_receiver_fkey");

                entity.HasIndex(e => e.IdSender, "fki_document_id_sender_fkey");

                entity.HasIndex(e => e.IdDocumentType, "fki_rtewte");

                entity.Property(e => e.IdDocument)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, null, null, null)
                    .HasColumnName("id_document");
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

                entity.HasOne(d => d.IdDocumentTypeNavigation).WithMany(p => p.Documents)
                    .HasForeignKey(d => d.IdDocumentType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_id_document_type_fkey");

                entity.HasOne(d => d.IdReceiverNavigation).WithMany(p => p.DocumentIdReceiverNavigations)
                    .HasForeignKey(d => d.IdReceiver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_id_receiver_fkey");

                entity.HasOne(d => d.IdSenderNavigation).WithMany(p => p.DocumentIdSenderNavigations)
                    .HasForeignKey(d => d.IdSender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_id_sender_fkey");
            });

            modelBuilder.Entity<DocumentContent>(entity =>
            {
                entity.HasKey(e => e.IdDocumentContent).HasName("document_content_pkey");

                entity.ToTable("document_content");

                entity.HasIndex(e => e.IdDocument, "fki_document_content_id_document_fkey");

                entity.Property(e => e.IdDocumentContent)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, null, null, null)
                    .HasColumnName("id_document_content");
                entity.Property(e => e.IdDocument).HasColumnName("id_document");
                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");
                entity.Property(e => e.Path).HasColumnName("path");

                entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.DocumentContents)
                    .HasForeignKey(d => d.IdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_content_id_document_fkey");
            });

            modelBuilder.Entity<DocumentStatus>(entity =>
            {
                entity.HasKey(e => e.IdDocumentStatus).HasName("document_status_pkey");

                entity.ToTable("document_status");

                entity.HasIndex(e => e.IdUser, "fki_document_status_id_user_fkey");

                entity.Property(e => e.IdDocumentStatus)
                    .ValueGeneratedNever()
                    .HasColumnName("id_document_status");
                entity.Property(e => e.AppropriationDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("appropriation_date");
                entity.Property(e => e.IdDocument).HasColumnName("id_document");
                entity.Property(e => e.IdStatus).HasColumnName("id_status");
                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdDocumentNavigation).WithMany(p => p.DocumentStatuses)
                    .HasForeignKey(d => d.IdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_status_id_document_fkey");

                entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.DocumentStatuses)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_status_id_status_fkey");

                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.DocumentStatuses)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("document_status_id_user_fkey");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.IdDocumentType).HasName("document_type_pkey");

                entity.ToTable("document_type");

                entity.HasIndex(e => e.IdSubordination, "fki_document_type_id_subordination");

                entity.Property(e => e.IdDocumentType)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, null, null, null)
                    .HasColumnName("id_document_type");
                entity.Property(e => e.IdSubordination).HasColumnName("id_subordination");
                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdSubordinationNavigation).WithMany(p => p.InverseIdSubordinationNavigation)
                    .HasForeignKey(d => d.IdSubordination)
                    .HasConstraintName("document_type_id_subordination");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPosition).HasName("position_pkey");

                entity.ToTable("position");

                entity.HasIndex(e => e.IdSubordination, "fki_position_id_subordination_fkey");

                entity.Property(e => e.IdPosition)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, null, null, null)
                    .HasColumnName("id_position");
                entity.Property(e => e.IdSubordination).HasColumnName("id_subordination");
                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdSubordinationNavigation).WithMany(p => p.InverseIdSubordinationNavigation)
                    .HasForeignKey(d => d.IdSubordination)
                    .HasConstraintName("position_id_subordination_fkey");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus).HasName("status_pkey");

                entity.ToTable("status");

                entity.Property(e => e.IdStatus)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, null, null, null)
                    .HasColumnName("id_status");
                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Subdivision>(entity =>
            {
                entity.HasKey(e => e.IdSubdivision).HasName("subdivision_pkey");

                entity.ToTable("subdivision");

                entity.HasIndex(e => e.IdSubordination, "fki_subdivision_id_subordination_fkey");

                entity.Property(e => e.IdSubdivision)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, null, null, null)
                    .HasColumnName("id_subdivision");
                entity.Property(e => e.IdSubordination).HasColumnName("id_subordination");
                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdSubordinationNavigation).WithMany(p => p.InverseIdSubordinationNavigation)
                    .HasForeignKey(d => d.IdSubordination)
                    .HasConstraintName("subdivision_id_subordination_fkey");
            });

            modelBuilder.Entity<UserPosition>(entity =>
            {
                entity.HasKey(e => e.IdUserPosition).HasName("user_position_pkey");

                entity.ToTable("user_position");

                entity.HasIndex(e => e.IdPosition, "fki_user_position_id_position_fkey");

                entity.HasIndex(e => e.IdUser, "fki_user_position_id_user_fkey");

                entity.Property(e => e.IdUserPosition)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, null, null, null)
                    .HasColumnName("id_user_position");
                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("appointment_date");
                entity.Property(e => e.IdPosition).HasColumnName("id_position");
                entity.Property(e => e.IdUser).HasColumnName("id_user");
                entity.Property(e => e.RemovalDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("removal_date");

                entity.HasOne(d => d.IdPositionNavigation).WithMany(p => p.UserPositions)
                    .HasForeignKey(d => d.IdPosition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_position_id_position_fkey");

                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserPositions)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_position_id_user_fkey");
            });

            modelBuilder.Entity<UserSubdivision>(entity =>
            {
                entity.HasKey(e => e.IdUserSubdivision).HasName("user_subdivision_pkey");

                entity.ToTable("user_subdivision");

                entity.HasIndex(e => e.IdSubdivision, "fki_user_subdivision_id_subdivision_fkey");

                entity.HasIndex(e => e.IdUser, "fki_user_subdivision_id_user_fkey");

                entity.Property(e => e.IdUserSubdivision)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, null, null, null)
                    .HasColumnName("id_user_subdivision");
                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("appointment_date");
                entity.Property(e => e.IdSubdivision).HasColumnName("id_subdivision");
                entity.Property(e => e.IdUser).HasColumnName("id_user");
                entity.Property(e => e.RemovalDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("removal_date");

                entity.HasOne(d => d.IdSubdivisionNavigation).WithMany(p => p.UserSubdivisions)
                    .HasForeignKey(d => d.IdSubdivision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_subdivision_id_subdivision_fkey");

                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserSubdivisions)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_subdivision_id_user_fkey");
            });

        }

    }
}
