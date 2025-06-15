using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BikeAppApp.Models
{
    public partial class MotoDBContext : DbContext
    {
        public MotoDBContext()
        {
        }

        public MotoDBContext(DbContextOptions<MotoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alicilar> Alicilars { get; set; } = null!;
        public virtual DbSet<BakimGecmisi> BakimGecmisis { get; set; } = null!;
        public virtual DbSet<BayiAlici> BayiAlicis { get; set; } = null!;
        public virtual DbSet<BayiMotosiklet> BayiMotosiklets { get; set; } = null!;
        public virtual DbSet<Bayiler> Bayilers { get; set; } = null!;
        public virtual DbSet<Calisanlar> Calisanlars { get; set; } = null!;
        public virtual DbSet<Motosikletler> Motosikletlers { get; set; } = null!;
        public virtual DbSet<YetkiliServi> YetkiliServis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alicilar>(entity =>
            {
                entity.HasKey(e => e.AliciId)
                    .HasName("PK__Alicilar__E859CA3A62619D00");
            });

            modelBuilder.Entity<BakimGecmisi>(entity =>
            {
                entity.HasKey(e => e.BakimId)
                    .HasName("PK__BakimGec__7227287E3F817718");

                entity.HasOne(d => d.Motosiklet)
                    .WithMany(p => p.BakimGecmisis)
                    .HasForeignKey(d => d.MotosikletId)
                    .HasConstraintName("FK__BakimGecm__Motos__412EB0B6");

                entity.HasOne(d => d.Servis)
                    .WithMany(p => p.BakimGecmisis)
                    .HasForeignKey(d => d.ServisId)
                    .HasConstraintName("FK__BakimGecm__Servi__4222D4EF");
            });

            modelBuilder.Entity<BayiAlici>(entity =>
            {
                entity.HasOne(d => d.Alici)
                    .WithMany(p => p.BayiAlicis)
                    .HasForeignKey(d => d.AliciId)
                    .HasConstraintName("FK__BayiAlici__Alici__49C3F6B7");

                entity.HasOne(d => d.Bayi)
                    .WithMany(p => p.BayiAlicis)
                    .HasForeignKey(d => d.BayiId)
                    .HasConstraintName("FK__BayiAlici__BayiI__48CFD27E");

                entity.HasOne(d => d.Motosiklet)
                    .WithMany(p => p.BayiAlicis)
                    .HasForeignKey(d => d.MotosikletId)
                    .HasConstraintName("FK__BayiAlici__Motos__4AB81AF0");
            });

            modelBuilder.Entity<BayiMotosiklet>(entity =>
            {
                entity.HasOne(d => d.Bayi)
                    .WithMany(p => p.BayiMotosiklets)
                    .HasForeignKey(d => d.BayiId)
                    .HasConstraintName("FK__BayiMotos__BayiI__44FF419A");

                entity.HasOne(d => d.Motosiklet)
                    .WithMany(p => p.BayiMotosiklets)
                    .HasForeignKey(d => d.MotosikletId)
                    .HasConstraintName("FK__BayiMotos__Motos__45F365D3");
            });

            modelBuilder.Entity<Bayiler>(entity =>
            {
                entity.HasKey(e => e.BayiId)
                    .HasName("PK__Bayiler__A3CE97F3A59EF5CB");
            });

            modelBuilder.Entity<Calisanlar>(entity =>
            {
                entity.HasKey(e => e.CalisanId)
                    .HasName("PK__Calisanl__40749CB6E231DAED");

                entity.HasOne(d => d.CalistigiYer)
                    .WithMany(p => p.Calisanlars)
                    .HasForeignKey(d => d.CalistigiYerId)
                    .HasConstraintName("FK__Calisanla__Calis__4BAC3F29");

                entity.HasOne(d => d.CalistigiYerNavigation)
                    .WithMany(p => p.Calisanlars)
                    .HasForeignKey(d => d.CalistigiYerId)
                    .HasConstraintName("FK__Calisanla__Calis__4CA06362");
            });

            modelBuilder.Entity<Motosikletler>(entity =>
            {
                entity.HasKey(e => e.MotosikletId)
                    .HasName("PK__Motosikl__0B2DE72AE1AEB247");
            });

            modelBuilder.Entity<YetkiliServi>(entity =>
            {
                entity.HasKey(e => e.ServisId)
                    .HasName("PK__YetkiliS__8BED6408D577BD41");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}