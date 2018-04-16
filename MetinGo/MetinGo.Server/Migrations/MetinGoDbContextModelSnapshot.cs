﻿// <auto-generated />
using MetinGo.Common;
using MetinGo.Server.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace MetinGo.Server.Migrations
{
    [DbContext(typeof(MetinGoDbContext))]
    partial class MetinGoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MetinGo.Server.Entities.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BaseAttack");

                    b.Property<int>("BaseDefence");

                    b.Property<int>("BaseMaxHP");

                    b.Property<int>("Experience");

                    b.Property<double>("Latitude");

                    b.Property<int>("Level");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<int>("StatPoints");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("MetinGo.Server.Entities.CharacterItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CharacterId");

                    b.Property<Guid>("FightId");

                    b.Property<int>("ItemId");

                    b.Property<int>("Level");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("FightId");

                    b.HasIndex("ItemId");

                    b.ToTable("CharacterItems");
                });

            modelBuilder.Entity("MetinGo.Server.Entities.Fight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CharacterId");

                    b.Property<int>("Experience");

                    b.Property<Guid>("MonsterId");

                    b.Property<bool>("PlayerWon");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("MonsterId");

                    b.ToTable("Fights");
                });

            modelBuilder.Entity("MetinGo.Server.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Attack");

                    b.Property<int>("Defence");

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<int>("ItemType");

                    b.Property<int>("MaxHP");

                    b.Property<string>("Name");

                    b.Property<int>("PerLevelAttack");

                    b.Property<int>("PerLevelDefence");

                    b.Property<int>("PerLevelMaxHP");

                    b.Property<int>("Rarity");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("MetinGo.Server.Entities.Monster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAlive");

                    b.Property<double>("Latitude");

                    b.Property<int>("Level");

                    b.Property<double>("Longitude");

                    b.Property<int>("MonsterType");

                    b.HasKey("Id");

                    b.ToTable("Monsters");
                });

            modelBuilder.Entity("MetinGo.Server.Entities.MonsterTypeLoot", b =>
                {
                    b.Property<int>("ItemId");

                    b.Property<int>("MonsterType");

                    b.Property<decimal>("Probability");

                    b.HasKey("ItemId", "MonsterType");

                    b.ToTable("MonsterTypeLoots");
                });

            modelBuilder.Entity("MetinGo.Server.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MetinGo.Server.Entities.Character", b =>
                {
                    b.HasOne("MetinGo.Server.Entities.User", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MetinGo.Server.Entities.CharacterItem", b =>
                {
                    b.HasOne("MetinGo.Server.Entities.Character", "Character")
                        .WithMany("CharacterItems")
                        .HasForeignKey("CharacterId");

                    b.HasOne("MetinGo.Server.Entities.Fight", "Fight")
                        .WithMany("Loot")
                        .HasForeignKey("FightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetinGo.Server.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MetinGo.Server.Entities.Fight", b =>
                {
                    b.HasOne("MetinGo.Server.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MetinGo.Server.Entities.Monster", "Monster")
                        .WithMany()
                        .HasForeignKey("MonsterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MetinGo.Server.Entities.MonsterTypeLoot", b =>
                {
                    b.HasOne("MetinGo.Server.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
