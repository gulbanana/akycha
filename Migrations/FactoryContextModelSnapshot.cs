﻿// <auto-generated />
using System;
using Akycha.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Akycha.Migrations
{
    [DbContext(typeof(FactoryContext))]
    partial class FactoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Akycha.Model.Facility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Icon")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Facility");
                });

            modelBuilder.Entity("Akycha.Model.Input", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FromId")
                        .HasColumnType("int");

                    b.Property<int?>("PartId")
                        .HasColumnType("int");

                    b.Property<float>("QuantityPerMinute")
                        .HasColumnType("real");

                    b.Property<int?>("ToId")
                        .HasColumnType("int");

                    b.Property<string>("TransportMethod")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("PartId");

                    b.HasIndex("ToId");

                    b.ToTable("Input");
                });

            modelBuilder.Entity("Akycha.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PartId")
                        .HasColumnType("int");

                    b.Property<float>("QuantityPerMinute")
                        .HasColumnType("real");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Akycha.Model.Machine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Icon")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PowerUsageMegawatts")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Machine");
                });

            modelBuilder.Entity("Akycha.Model.Part", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Icon")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Part");
                });

            modelBuilder.Entity("Akycha.Model.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FacilityId")
                        .HasColumnType("int");

                    b.Property<int>("PowerShards")
                        .HasColumnType("int");

                    b.Property<int>("QuantityMachines")
                        .HasColumnType("int");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Process");
                });

            modelBuilder.Entity("Akycha.Model.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MachineId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MachineId");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("Akycha.Model.Input", b =>
                {
                    b.HasOne("Akycha.Model.Facility", "From")
                        .WithMany("Outputs")
                        .HasForeignKey("FromId");

                    b.HasOne("Akycha.Model.Part", "Part")
                        .WithMany()
                        .HasForeignKey("PartId");

                    b.HasOne("Akycha.Model.Facility", "To")
                        .WithMany("Inputs")
                        .HasForeignKey("ToId");

                    b.Navigation("From");

                    b.Navigation("Part");

                    b.Navigation("To");
                });

            modelBuilder.Entity("Akycha.Model.Item", b =>
                {
                    b.HasOne("Akycha.Model.Part", "Part")
                        .WithMany()
                        .HasForeignKey("PartId");

                    b.HasOne("Akycha.Model.Recipe", "Recipe")
                        .WithMany("Items")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Part");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Akycha.Model.Process", b =>
                {
                    b.HasOne("Akycha.Model.Facility", "Facility")
                        .WithMany("Processes")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Akycha.Model.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");

                    b.Navigation("Facility");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Akycha.Model.Recipe", b =>
                {
                    b.HasOne("Akycha.Model.Machine", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Akycha.Model.Facility", b =>
                {
                    b.Navigation("Inputs");

                    b.Navigation("Outputs");

                    b.Navigation("Processes");
                });

            modelBuilder.Entity("Akycha.Model.Recipe", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
