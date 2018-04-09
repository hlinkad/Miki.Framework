﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Miki.Framework.Models.Context
{
    internal class IAContext : DbContext
    {
        public DbSet<Identifier> Identifiers { get; set; }
        public DbSet<CommandState> CommandStates { get; set; }
		public DbSet<ChannelLanguage> Languages { get; set; }
        public DbSet<ModuleState> ModuleStates { get; set; }

		public static readonly LoggerFactory logger
			= new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => __ > LogLevel.Information, false) });

		public IAContext()
        {
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(Bot.Instance.Information.DatabaseConnectionString);
			//optionsBuilder.UseLoggerFactory(logger);
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CommandState>()
				.HasKey(c => new { c.CommandName, c.ChannelId });

			modelBuilder.Entity<Identifier>()
				.HasKey(c => new { c.GuildId, c.DefaultValue });

			modelBuilder.Entity<ModuleState>()
				.HasKey(c => new { c.ModuleName, c.ChannelId});

			modelBuilder.HasDefaultSchema("dbo");
		}
	}
}