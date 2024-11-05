using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace DiscordBot;

public class Program
{
    private static DiscordSocketClient _client;
    private static CommandService _commands;
    private static CommandHandler commandHandler;

    public static async Task Main()
    {
        var configJson = File.ReadAllText("C:\\Users\\rekevman\\RiderProjects\\DiscordBot\\DiscordBot\\config.json");
        var config = JsonConvert.DeserializeObject<Config>(configJson);
        var token = config.Token;
        
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });
        
        _commands = new CommandService();
        commandHandler = new CommandHandler(_client, _commands);

        _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


        await commandHandler.InstallCommandsAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
    
    public class Config
    {
        public string Token { get; set; }
    }
}