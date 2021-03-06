﻿// <auto-generated />
using Miki.Framework.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Meru.Core.Migrations
{
    [DbContext(typeof(IAContext))]
    [Migration("20180121121338_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Miki.Framework.Models.CommandState", b =>
                {
                    b.Property<string>("CommandName")
                        .HasColumnName("CommandName");

                    b.Property<long>("ChannelId")
                        .HasColumnName("ChannelId");

                    b.Property<bool>("State")
                        .HasColumnName("State");

                    b.HasKey("CommandName", "ChannelId");

                    b.HasAlternateKey("ChannelId", "CommandName");

                    b.ToTable("CommandStates");
                });

            modelBuilder.Entity("Miki.Framework.Models.Identifier", b =>
                {
                    b.Property<long>("GuildId")
                        .HasColumnName("GuildId");

                    b.Property<string>("DefaultValue")
                        .HasColumnName("IdentifierId");

                    b.Property<string>("Value")
                        .HasColumnName("identifier");

                    b.HasKey("GuildId", "DefaultValue");

                    b.HasAlternateKey("DefaultValue", "GuildId");

                    b.ToTable("Identifiers");
                });

            modelBuilder.Entity("Miki.Framework.Models.ModuleState", b =>
                {
                    b.Property<string>("ModuleName")
                        .HasColumnName("ModuleName");

                    b.Property<long>("ChannelId")
                        .HasColumnName("ChannelId");

                    b.Property<bool>("State")
                        .HasColumnName("State");

                    b.HasKey("ModuleName", "ChannelId");

                    b.HasAlternateKey("ChannelId", "ModuleName");

                    b.ToTable("ModuleStates");
                });
#pragma warning restore 612, 618
        }
    }
}
