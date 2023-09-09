﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockWallet.Db.Mysql;

#nullable disable

namespace StockWallet.Db.Mysql.Migrations
{
    [DbContext(typeof(StockWalletContext))]
    partial class StockWalletContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StockWallet.Domain.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("StockWallet.Domain.Models.Serivces.StockSummary", b =>
                {
                    b.Property<int>("SummaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("AveragePrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("SummaryId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("WalletId");

                    b.ToTable("Summaries");
                });

            modelBuilder.Entity("StockWallet.Domain.Models.StockEvent", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("EventType")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("EventId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("WalletId");

                    b.ToTable("StockEvents");
                });

            modelBuilder.Entity("StockWallet.Domain.Models.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("WalletId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("StockWallet.Domain.Models.Serivces.StockSummary", b =>
                {
                    b.HasOne("StockWallet.Domain.Models.Company", "Company")
                        .WithMany("Summaries")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockWallet.Domain.Models.Wallet", "Wallet")
                        .WithMany("Summaries")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("StockWallet.Domain.Models.StockEvent", b =>
                {
                    b.HasOne("StockWallet.Domain.Models.Company", "Company")
                        .WithMany("StockEvents")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockWallet.Domain.Models.Wallet", "Wallet")
                        .WithMany("StockEvents")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("StockWallet.Domain.Models.Company", b =>
                {
                    b.Navigation("StockEvents");

                    b.Navigation("Summaries");
                });

            modelBuilder.Entity("StockWallet.Domain.Models.Wallet", b =>
                {
                    b.Navigation("StockEvents");

                    b.Navigation("Summaries");
                });
#pragma warning restore 612, 618
        }
    }
}
