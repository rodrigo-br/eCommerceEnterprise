﻿// <auto-generated />
using System;
using ECE.Cart.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECE.Cart.API.Migrations
{
    [DbContext(typeof(CartContext))]
    partial class CartContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ECE.Cart.API.Models.CustomerCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("IDX_Customer");

                    b.ToTable("CustomerCart");
                });

            modelBuilder.Entity("ECE.Cart.API.Models.ProductCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductAmount")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductImage")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("ProductValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("ProductsCart");
                });

            modelBuilder.Entity("ECE.Cart.API.Models.ProductCart", b =>
                {
                    b.HasOne("ECE.Cart.API.Models.CustomerCart", "CustomerCart")
                        .WithMany("Products")
                        .HasForeignKey("CartId")
                        .IsRequired();

                    b.Navigation("CustomerCart");
                });

            modelBuilder.Entity("ECE.Cart.API.Models.CustomerCart", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}