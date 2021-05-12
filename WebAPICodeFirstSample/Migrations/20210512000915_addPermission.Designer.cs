﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPICodeFirstSample.Models;

namespace WebAPICodeFirstSample.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210512000915_addPermission")]
    partial class addPermission
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("WebAPICodeFirstSample.Models.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WebAPICodeFirstSample.Models.AccountPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("PermissionId");

                    b.ToTable("AccountPermissions");
                });

            modelBuilder.Entity("WebAPICodeFirstSample.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("WebAPICodeFirstSample.Models.AccountPermission", b =>
                {
                    b.HasOne("WebAPICodeFirstSample.Models.Account", "IDAccountNavigation")
                        .WithMany("AccountPermission")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPICodeFirstSample.Models.Permission", "IDPermissionNavigation")
                        .WithMany("AccountPermission")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IDAccountNavigation");

                    b.Navigation("IDPermissionNavigation");
                });

            modelBuilder.Entity("WebAPICodeFirstSample.Models.Account", b =>
                {
                    b.Navigation("AccountPermission");
                });

            modelBuilder.Entity("WebAPICodeFirstSample.Models.Permission", b =>
                {
                    b.Navigation("AccountPermission");
                });
#pragma warning restore 612, 618
        }
    }
}