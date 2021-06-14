﻿using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

using System.Threading.Tasks;
using DiscordBot.Services;
using System.Diagnostics;

namespace DiscordBot
{
    public class StartUp
    {
        public IConfigurationRoot Configuration { get; }
        public StartUp(string [] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddYamlFile("config.yml");
            Configuration = builder.Build();
        }
        public static async Task RunAsync(string [] args)
        {
            var startup = new StartUp(args);
            Debug.WriteLine("fuck");
           
            Console.WriteLine("Fuck");
            await startup.RunAsync();
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigurationServices(services);
            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<CommandHandler>();
            await provider.GetRequiredService<StartupService>().StartAsync();
           
            await Task.Delay(-1);
        }


        private void ConfigurationServices(IServiceCollection services)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = Discord.LogSeverity.Verbose,
                MessageCacheSize = 1000,
            }
                ))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = Discord.LogSeverity.Verbose,
                    DefaultRunMode = RunMode.Async,
                    CaseSensitiveCommands = true
                }

                ))
                .AddSingleton<CommandHandler>()
                .AddSingleton<StartupService>()
                .AddSingleton(Configuration);
        }
    }

}
