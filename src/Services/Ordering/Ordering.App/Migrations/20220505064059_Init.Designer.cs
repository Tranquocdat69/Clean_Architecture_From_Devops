﻿// <auto-generated />
using System;
using FPTS.FIT.BDRD.Services.Ordering.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace FPTS.FIT.BDRD.Services.Ordering.App.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20220505064059_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("User_Id");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("Order_Date");

                    b.HasKey("Id");

                    b.ToTable("orders", "ORDERING");
                });

            modelBuilder.Entity("FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<int>("ProductId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<decimal>("_discount")
                        .HasColumnType("DECIMAL(18, 2)")
                        .HasColumnName("Discount");

                    b.Property<string>("_pictureUrl")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("PictureUrl");

                    b.Property<string>("_productName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("ProductName");

                    b.Property<decimal>("_unitPrice")
                        .HasColumnType("DECIMAL(18, 2)")
                        .HasColumnName("UnitPrice");

                    b.Property<int>("_units")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("Units");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("orderItems", "ORDERING");
                });

            modelBuilder.Entity("FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.Order", b =>
                {
                    b.OwnsOne("FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<string>("OrderId1")
                                .HasColumnType("NVARCHAR2(450)");

                            b1.Property<string>("City")
                                .HasColumnType("NVARCHAR2(2000)");

                            b1.Property<int>("OrderId")
                                .HasColumnType("NUMBER(10)");

                            b1.Property<string>("Street")
                                .HasColumnType("NVARCHAR2(2000)");

                            b1.HasKey("OrderId1");

                            b1.ToTable("orders", "ORDERING");

                            b1.WithOwner()
                                .HasForeignKey("OrderId1");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
