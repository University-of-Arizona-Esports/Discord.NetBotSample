using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot
{
    public class StartupService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _provider;

        public StartupService(
            DiscordSocketClient client,
            CommandService commands,
            IServiceProvider provider)
        {
            _client = client;
            _commands = commands;
            _provider = provider;
        }
        
        public async Task StartAsync()
        {
            // Read token from environment variables.
            var token = Environment.GetEnvironmentVariable("AZEsportsTestingBot");
            Console.WriteLine("Logging in...");
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _provider);
        }
    }
}