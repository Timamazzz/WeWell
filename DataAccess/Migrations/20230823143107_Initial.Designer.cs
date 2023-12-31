﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230823143107_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateTimeEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GuestId")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsShowForCreator")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsShowForGuest")
                        .HasColumnType("boolean");

                    b.Property<int?>("MaxDurationHours")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("MaxPrice")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("MinDurationHours")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("MinPrice")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("GuestId");

                    b.HasIndex("PlaceId");

                    b.HasIndex("TypeId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("DataAccess.Models.MeetingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("MeetingTypes");
                });

            modelBuilder.Entity("DataAccess.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<int?>("MaxDurationHours")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("MaxPrice")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("MinDurationHours")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("MinPrice")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("DataAccess.Models.Preference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AvatarPath")
                        .HasColumnType("text");

                    b.Property<bool?>("IsAllPreferences")
                        .IsRequired()
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlacePreference", b =>
                {
                    b.Property<int>("PlacesId")
                        .HasColumnType("integer");

                    b.Property<int>("PreferencesId")
                        .HasColumnType("integer");

                    b.HasKey("PlacesId", "PreferencesId");

                    b.HasIndex("PreferencesId");

                    b.ToTable("PlacePreference");
                });

            modelBuilder.Entity("PreferenceUser", b =>
                {
                    b.Property<int>("PreferencesId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("PreferencesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("PreferenceUser");
                });

            modelBuilder.Entity("DataAccess.Models.Meeting", b =>
                {
                    b.HasOne("DataAccess.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.User", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Place", "Place")
                        .WithMany("Meetings")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.MeetingType", "Type")
                        .WithMany("Meetings")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Guest");

                    b.Navigation("Place");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("DataAccess.Models.MeetingType", b =>
                {
                    b.HasOne("DataAccess.Models.Place", null)
                        .WithMany("MeetingTypes")
                        .HasForeignKey("PlaceId");
                });

            modelBuilder.Entity("PlacePreference", b =>
                {
                    b.HasOne("DataAccess.Models.Place", null)
                        .WithMany()
                        .HasForeignKey("PlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Preference", null)
                        .WithMany()
                        .HasForeignKey("PreferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PreferenceUser", b =>
                {
                    b.HasOne("DataAccess.Models.Preference", null)
                        .WithMany()
                        .HasForeignKey("PreferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Models.MeetingType", b =>
                {
                    b.Navigation("Meetings");
                });

            modelBuilder.Entity("DataAccess.Models.Place", b =>
                {
                    b.Navigation("MeetingTypes");

                    b.Navigation("Meetings");
                });
#pragma warning restore 612, 618
        }
    }
}
