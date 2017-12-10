using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DataLibrary.Static_Data;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Loggers;
using DiscordBot.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot
{
    class Program
    {
        // Program entry point
        static void Main(string[] args)
        {
            Tests();
                Console.WriteLine(OptionManager.DiscordKey);


            // Call the Program constructor, followed by the 
            // MainAsync method and wait until it finishes (which should be never).
            new Program().MainAsync().GetAwaiter().GetResult();
        }
        private static void Tests()
        {
            DefaultLogger.Logger(new LogMessage(LogSeverity.Info, "Database", "Starting Database Check"));
            if (DatabaseManager.GetMock() != null)
            {
                DefaultLogger.Logger(new LogMessage(LogSeverity.Info, "Database", "Success"));
            }
            else
            {
                DefaultLogger.Logger(
                    new LogMessage(LogSeverity.Critical, "Database", "Mock database failed to connect"));
                Console.ReadLine();
            }
            DefaultLogger.Logger(new LogMessage(LogSeverity.Info, "Configuration", "Checking Configuration.json file"));
            try
            {
                string t = OptionManager.DiscordKey; //Write proper test later
                DefaultLogger.Logger(new LogMessage(LogSeverity.Info, "Configuration", "Configuration verified"));
            }
            catch
            {
                DefaultLogger.Logger(new LogMessage(LogSeverity.Critical, "Configuration",
                    "Failed to use configuration file"));
                Console.ReadLine();
            }
            DefaultLogger.Logger(new LogMessage(LogSeverity.Info, "Riot API", "Starting API Connection Check"));
            DefaultLogger.Logger(new LogMessage(LogSeverity.Error, "Riot API", "Not implemented"));
        }

        private readonly DiscordSocketClient _client;

        // Keep the CommandService and IServiceCollection around for use with commands.
        // These two types require you install the Discord.Net.Commands package.
        private readonly IServiceCollection _map = new ServiceCollection();
        private readonly CommandService _commands = new CommandService();


        private Program()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                // How much logging do you want to see?
                LogLevel = LogSeverity.Info,

                // If you or another service needs to do anything with messages
                // (eg. checking Reactions, checking the content of edited/deleted messages),
                // you must set the MessageCacheSize. You may adjust the number as needed.
                //MessageCacheSize = 50,

                // If your platform doesn't have native websockets,
                // add Discord.Net.Providers.WS4Net from NuGet,
                // add the `using` at the top, and uncomment this line:
                //WebSocketProvider = WS4NetProvider.Instance
            });
            // Subscribe the logging handler to both the client and the CommandService.
            _client.Log += DefaultLogger.Logger;
            _commands.Log += DefaultLogger.Logger;
            _client.ReactionAdded += ClientOnReactionAdded;
        }

        private Task ClientOnReactionAdded(Cacheable<IUserMessage, ulong> cacheable, ISocketMessageChannel socketMessageChannel, SocketReaction arg3)
        {
            ReactionTracker.ReactionAdded(cacheable, socketMessageChannel,arg3);
            return Task.CompletedTask;
        }

        // Example of a logging handler. This can be re-used by addons
        // that ask for a Func<LogMessage, Task>.


        private async Task MainAsync()
        {
            // Centralize the logic for commands into a seperate method.
            await InitCommands();

            // Login and connect.
            await _client.LoginAsync(TokenType.Bot, OptionManager.DiscordKey);
            await _client.StartAsync();

            // Wait infinitely so your bot actually stays connected.
            await Task.Delay(-1);
        }

        private IServiceProvider _services;


        private async Task InitCommands()
        {
            // Repeat this for all the service classes
            // and other dependencies that your commands might need.
            //_map.AddSingleton(new SomeServiceClass());

            // When all your required services are in the collection, build the container.
            // Tip: There's an overload taking in a 'validateScopes' bool to make sure
            // you haven't made any mistakes in your dependency graph.
            _services = _map.BuildServiceProvider();

            // Either search the program and add all Module classes that can be found.
            // Module classes MUST be marked 'public' or they will be ignored.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            // Or add Modules manually if you prefer to be a little more explicit:
            //await _commands.AddModuleAsync<SomeModule>();
            //await _commands.AddModuleAsync<TestModule>();
            //await _commands.AddModuleAsync<UserModule>();
            //await _commands.AddModuleAsync<ServerModule>();
            // Note that the first one is 'Modules' (plural) and the second is 'Module' (singular).

            // Subscribe a handler to see if a message invokes a command.
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            // Bail out if it's a System Message.
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            // We don't want the bot to respond to itself or other bots.
            // NOTE: Selfbots should invert this first check and remove the second
            // as they should ONLY be allowed to respond to messages from the same account.
            if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;
            // Replace the '!' with whatever character
            // you want to prefix your commands with.
            // Uncomment the second half if you also want
            // commands to be invoked by mentioning the bot instead.
            if (msg.HasCharPrefix('-', ref pos) || msg.HasMentionPrefix(_client.CurrentUser, ref pos))
            {
                // Create a Command Context.
                var context = new SocketCommandContext(_client, msg);

                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed succesfully).
                var result = await _commands.ExecuteAsync(context, pos, _services);
                
                // Uncomment the following lines if you want the bot
                // to send a message if it failed (not advised for most situations).
                //if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                //    await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
