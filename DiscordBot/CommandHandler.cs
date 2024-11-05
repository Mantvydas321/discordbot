using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot;

public class CommandHandler : CommandService
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;

    public CommandHandler(DiscordSocketClient client, CommandService commands)
    {
        _commands = commands;
        _client = client;
    }

    public async Task InstallCommandsAsync()
    {
        _client.MessageReceived += HandleCommandAsync;

        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;
        if (message == null || message.Author.IsBot) return;

        // Debug log to ensure server messages are being captured
        Console.WriteLine($"Message received: {message.Content} from {message.Author} in {message.Channel}");

        int argPos = 0;

        // Check if the message starts with the prefix '/' or mentions the bot
        if (!(message.HasCharPrefix('/', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
            return;

        var context = new SocketCommandContext(_client, message);

        await _commands.ExecuteAsync(context: context, argPos: argPos, services: null);
    }
}