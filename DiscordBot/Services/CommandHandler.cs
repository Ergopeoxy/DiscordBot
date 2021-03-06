using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Services
{
    public class CommandHandler
    {
        public static IServiceProvider _provider;
        public static DiscordSocketClient _discord;
        public static CommandService _commands;
        public static IConfigurationRoot _config;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, IConfigurationRoot config)
        {
            _provider = provider;
            _discord = discord;
            _commands = commands;
            _config = config;

            _discord.Ready += OnReady;
            _discord.MessageReceived += OnMessageReceived;

        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg.Author.IsBot)
                return;
            var contex = new SocketCommandContext(_discord,msg);
            int pos = 0;
            if (msg.HasStringPrefix(_config["prefix"],ref pos)||msg.HasMentionPrefix(_discord.CurrentUser,ref pos)) 
            {
                var result = await _commands.ExecuteAsync(contex,pos,_provider);
                if (!result.IsSuccess)
                {
                    var reason = result.ErrorReason;
                    await contex.Channel.SendMessageAsync($"u or me fucked up , here is the error \n {reason}");
                    Console.WriteLine(reason);
                }
            }
        }

        private Task OnReady()
        {
            Console.WriteLine($"Connected as {_discord.CurrentUser} #{_discord.CurrentUser.Discriminator}");
            return Task.CompletedTask;
        }
    }
}
