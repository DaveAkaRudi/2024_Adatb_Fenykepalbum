﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using PhotoApp.Context;

#nullable disable

namespace PhotoApp.Migrations
{
    [DbContext(typeof(EFContext))]
    partial class EFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PhotoApp.Models.Album", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("cim")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("felhasz_id")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("id");

                    b.HasIndex("felhasz_id");

                    b.ToTable("albumok");
                });

            modelBuilder.Entity("PhotoApp.Models.Felhasznalo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("jelszo")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("NVARCHAR2(64)");

                    b.Property<string>("nev")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("NVARCHAR2(32)");

                    b.Property<int>("role")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("szuletes_datuma")
                        .HasColumnType("TIMESTAMP(7)");

                    b.HasKey("id");

                    b.ToTable("felhasznalok");
                });

            modelBuilder.Entity("PhotoApp.Models.Kategoria", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nev")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id");

                    b.ToTable("kategoriak");
                });

            modelBuilder.Entity("PhotoApp.Models.Kep", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("album_id")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("cim")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<double>("ertekeles")
                        .HasColumnType("BINARY_DOUBLE");

                    b.Property<int>("felhasz_id")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("fenykeputvonal")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("orszag_id")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("id");

                    b.HasIndex("album_id");

                    b.HasIndex("felhasz_id");

                    b.HasIndex("orszag_id");

                    b.ToTable("kepek");
                });

            modelBuilder.Entity("PhotoApp.Models.KepKategoria", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("kategoria_id")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("kep_id")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("id");

                    b.HasIndex("kategoria_id");

                    b.HasIndex("kep_id");

                    b.ToTable("KepKategoria");
                });

            modelBuilder.Entity("PhotoApp.Models.Komment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("felhasz_id")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("kep_id")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("megjegyzes")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id");

                    b.HasIndex("felhasz_id");

                    b.HasIndex("kep_id");

                    b.ToTable("kommentek");
                });

            modelBuilder.Entity("PhotoApp.Models.Orszag", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("nev")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("rovidites")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("NVARCHAR2(3)");

                    b.HasKey("id");

                    b.ToTable("orszagok");
                });

            modelBuilder.Entity("PhotoApp.Models.Palyazat", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<DateTime>("hatarido")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("kategoria_id")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("leiras")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("nev")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("nyertes")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id");

                    b.HasIndex("kategoria_id");

                    b.ToTable("palyazatok");
                });

            modelBuilder.Entity("PhotoApp.Models.Album", b =>
                {
                    b.HasOne("PhotoApp.Models.Felhasznalo", "ReferencedFelhasznalo")
                        .WithMany()
                        .HasForeignKey("felhasz_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReferencedFelhasznalo");
                });

            modelBuilder.Entity("PhotoApp.Models.Kep", b =>
                {
                    b.HasOne("PhotoApp.Models.Album", "ReferencedAlbum")
                        .WithMany()
                        .HasForeignKey("album_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoApp.Models.Felhasznalo", "ReferencedFelhasznalo")
                        .WithMany()
                        .HasForeignKey("felhasz_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoApp.Models.Orszag", "ReferencedOrszag")
                        .WithMany()
                        .HasForeignKey("orszag_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReferencedAlbum");

                    b.Navigation("ReferencedFelhasznalo");

                    b.Navigation("ReferencedOrszag");
                });

            modelBuilder.Entity("PhotoApp.Models.KepKategoria", b =>
                {
                    b.HasOne("PhotoApp.Models.Kategoria", "ReferencedKategoria")
                        .WithMany()
                        .HasForeignKey("kategoria_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoApp.Models.Kep", "ReferencedKep")
                        .WithMany()
                        .HasForeignKey("kep_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReferencedKategoria");

                    b.Navigation("ReferencedKep");
                });

            modelBuilder.Entity("PhotoApp.Models.Komment", b =>
                {
                    b.HasOne("PhotoApp.Models.Felhasznalo", "ReferencedFelhasznalo")
                        .WithMany()
                        .HasForeignKey("felhasz_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhotoApp.Models.Kep", "ReferencedKep")
                        .WithMany()
                        .HasForeignKey("kep_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReferencedFelhasznalo");

                    b.Navigation("ReferencedKep");
                });

            modelBuilder.Entity("PhotoApp.Models.Palyazat", b =>
                {
                    b.HasOne("PhotoApp.Models.Kategoria", "ReferencedKategoria")
                        .WithMany()
                        .HasForeignKey("kategoria_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReferencedKategoria");
                });
#pragma warning restore 612, 618
        }
    }
}
