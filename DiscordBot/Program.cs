using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot;

public class Program
{
    private static DiscordSocketClient _client;
    public static TokenFile _token;
    private static CommandService _commands;
    private static CommandHandler commandHandler;

    public static async Task Main()
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });

        _token = new TokenFile();
        _commands = new CommandService();
        commandHandler = new CommandHandler(_client, _commands);

        _client.Log += Log;

        if (_token != null)
        {
            await _client.LoginAsync(TokenType.Bot, _token.Token);
            await _client.StartAsync();
        }
        else
        {
            Console.WriteLine("TokenFile instance is null.");
        }

        await commandHandler.InstallCommandsAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}