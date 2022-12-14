using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ServerAnime.Model;

namespace ServerAnime.Data.DataContext
{
    public partial class serveranimedbContext : DbContext
    {
        public serveranimedbContext(DbContextOptions<serveranimedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Capitulo> Capitulos { get; set; } = null!;
        public virtual DbSet<Catalogo> Catalogos { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Genero> Generos { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {         
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Capitulo>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CatalogoId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("capitulo");

                entity.HasIndex(e => e.CatalogoId, "fk_capitulo_catalogo1_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.CatalogoId).HasColumnName("catalogo_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.NombreCap)
                    .HasMaxLength(45)
                    .HasColumnName("nombre_cap");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("update_at");

                entity.Property(e => e.UrlCapituloCap)
                    .HasMaxLength(45)
                    .HasColumnName("url_capitulo_cap");
            });

            modelBuilder.Entity<Catalogo>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CategoriaId, e.EstadoId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("catalogo");

                entity.HasIndex(e => e.CategoriaId, "fk_Catalogo_Categoria1_idx");

                entity.HasIndex(e => e.EstadoId, "fk_Catalogo_Estado1_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.CategoriaId).HasColumnName("Categoria_id");

                entity.Property(e => e.EstadoId).HasColumnName("Estado_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DescipcionCat)
                    .HasMaxLength(45)
                    .HasColumnName("descipcion_cat");

                entity.Property(e => e.NombreCat)
                    .HasMaxLength(45)
                    .HasColumnName("nombre_cat");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("update_at");

                entity.Property(e => e.UrlPortadaCat)
                    .HasMaxLength(45)
                    .HasColumnName("url_portada_cat");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Catalogos)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Catalogo_Categoria1");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Catalogos)
                    .HasForeignKey(d => d.EstadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Catalogo_Estado1");
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.ToTable("categoria");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("update_at");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("estado");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.NombreEst)
                    .HasMaxLength(45)
                    .HasColumnName("nombre_est");

                entity.Property(e => e.StatusEst).HasColumnName("status_est");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CatalogoId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("genero");

                entity.HasIndex(e => e.CatalogoId, "fk_genero_catalogo_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.CatalogoId).HasColumnName("catalogo_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.NombreGen)
                    .HasMaxLength(45)
                    .HasColumnName("nombre_gen");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("update_at");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Rol)
                    .HasMaxLength(45)
                    .HasColumnName("rol");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("update_at");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.RoleId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("usuario");

                entity.HasIndex(e => e.RoleId, "fk_Usuario_Role1_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.RoleId).HasColumnName("Role_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("password");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("update_at");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Usuario_Role1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
