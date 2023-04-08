﻿// <auto-generated />
using Account.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Account.Application.Migrations
{
    [DbContext(typeof(AccountDbContext))]
    [Migration("20230408160458_Migration_08_04_2023")]
    partial class Migration_08_04_2023
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Account.Domain.Entities.AccountEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Accounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AccountEntity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Account.Domain.Entities.GoogleAccountEntity", b =>
                {
                    b.HasBaseType("Account.Domain.Entities.AccountEntity");

                    b.HasDiscriminator().HasValue("GoogleAccountEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
