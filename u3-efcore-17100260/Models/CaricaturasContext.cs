using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace u3_efcore_17100260.Models
{
    public partial class CaricaturasContext : DbContext
    {
        public CaricaturasContext()
        {
        }

        public CaricaturasContext(DbContextOptions<CaricaturasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anime> Animes { get; set; }
        public virtual DbSet<Personaje> Personajes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-60R71IJ\\L17100260;Database=Caricaturas;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Anime>(entity =>
            {
                entity.HasKey(e => e.IdAnime)
                    .HasName("PK__Anime__F69BDB2A70596AE3");

                entity.ToTable("Anime");

                entity.Property(e => e.IdAnime).HasColumnName("id_anime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Personaje>(entity =>
            {
                entity.HasKey(e => e.IdPersonaje)
                    .HasName("PK__Personaj__81949F40EA9A0755");

                entity.Property(e => e.IdPersonaje).HasColumnName("id_personaje");

                entity.Property(e => e.IdAnime).HasColumnName("id_anime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAnimeNavigation)
                    .WithMany(p => p.Personajes)
                    .HasForeignKey(d => d.IdAnime)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flores_Colores");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
