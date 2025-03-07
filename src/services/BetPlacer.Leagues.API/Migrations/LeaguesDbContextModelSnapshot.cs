﻿// <auto-generated />
using System;
using BetPlacer.Leagues.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BetPlacer.Leagues.API.Migrations
{
    [DbContext(typeof(LeaguesDbContext))]
    partial class LeaguesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BetPlacer.Leagues.API.Models.LeagueModel", b =>
                {
                    b.Property<int?>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("code");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Code"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Code")
                        .HasName("p_k_leagues");

                    b.ToTable("leagues");
                });

            modelBuilder.Entity("BetPlacer.Leagues.API.Models.LeagueSeasonModel", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("code");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Code"));

                    b.Property<bool>("Current")
                        .HasColumnType("boolean")
                        .HasColumnName("current");

                    b.Property<int>("LeagueCode")
                        .HasColumnType("integer")
                        .HasColumnName("league_code");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("year");

                    b.HasKey("Code")
                        .HasName("p_k_league_seasons");

                    b.HasIndex("LeagueCode");

                    b.ToTable("league_seasons");
                });

            modelBuilder.Entity("BetPlacer.Leagues.API.Models.LeagueSeasonModel", b =>
                {
                    b.HasOne("BetPlacer.Leagues.API.Models.LeagueModel", "League")
                        .WithMany()
                        .HasForeignKey("LeagueCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_league_seasons_leagues_league_code");

                    b.Navigation("League");
                });
#pragma warning restore 612, 618
        }
    }
}
