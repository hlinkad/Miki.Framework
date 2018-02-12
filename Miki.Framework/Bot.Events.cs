﻿using Miki.Common;
using Miki.Common.Interfaces;
using System;
using System.Threading.Tasks;

#pragma warning disable CS1998

namespace Miki.Framework
{
	public partial class Bot
	{
		public event Func<IDiscordGuild, Task> GuildJoin;

		public event Func<IDiscordGuild, Task> GuildLeave;

		public event Func<IDiscordMessage, Task> MessageReceived;

		public event Func<IDiscordUser, Task> UserJoin;

		public event Func<IDiscordUser, Task> UserLeft;

		public event Func<IDiscordUser, IDiscordUser, Task> UserUpdated;

		public event Func<int, Task> OnShardConnect;

		public event Func<Exception, int, Task> OnShardDisconnect;

		public void LoadEvents()
		{
			Client.UserJoined += async (u) =>
			{
				Task.Run(() => UserJoin(new RuntimeUser(u)));
			};

			Client.UserLeft += async (u) =>
			{
				Task.Run(() => UserLeft(new RuntimeUser(u)));
			};

			Client.UserUpdated += async (u, unew) =>
			{
				RuntimeUser userOld = new RuntimeUser(u);
				RuntimeUser userNew = new RuntimeUser(unew);
				Task.Run(() => UserUpdated(userOld, userNew));
			};

			Client.MessageReceived += async (m) =>
			{
				RuntimeMessage newMessage = new RuntimeMessage(m);
				if (MessageReceived != null)
				{
					await MessageReceived(newMessage);
				}
			};

			Client.JoinedGuild += async (g) =>
			{
				Task.Run(async () =>
				{
					RuntimeGuild guild = new RuntimeGuild(g);
					await GuildJoin(guild);
				});
			};

			Client.LeftGuild += async (g) =>
			{
				RuntimeGuild guild = new RuntimeGuild(g);
				await GuildLeave?.Invoke(guild);
			};

			foreach(var shard in Client.Shards)
			{
				shard.Disconnected += async (ex) =>
				{
					await OnShardDisconnect(ex, shard.ShardId);
				};
				shard.Connected += async () =>
				{
					if (OnShardConnect != null)
					{
						await OnShardConnect(shard.ShardId);
					}
				};
			}
		}
	}
}