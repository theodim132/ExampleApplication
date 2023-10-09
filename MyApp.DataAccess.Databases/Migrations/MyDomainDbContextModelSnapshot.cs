﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyApp.DataAccess.Databases.MyDomain;

#nullable disable

namespace MyApp.DataAccess.Databases.Migrations
{
    [DbContext(typeof(MyDomainDbContext))]
    partial class MyDomainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyApp.DataAccess.Abstractions.MyDomain.Entities.Border", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Borders");
                });

            modelBuilder.Entity("MyApp.DataAccess.Abstractions.MyDomain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Capital")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NativeNameSpaCommon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NativeNameSpaOfficial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MyApp.DataAccess.Abstractions.MyDomain.Entities.Border", b =>
                {
                    b.HasOne("MyApp.DataAccess.Abstractions.MyDomain.Entities.Country", "Country")
                        .WithMany("Borders")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MyApp.DataAccess.Abstractions.MyDomain.Entities.Country", b =>
                {
                    b.Navigation("Borders");
                });
#pragma warning restore 612, 618
        }
    }
}
