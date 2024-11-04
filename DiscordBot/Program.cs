using Discord;
using Discord.WebSocket;

public class Program
{
    // https://docs.discordnet.dev/guides/getting_started/first-bot.html
    private static DiscordSocketClient _client;
    private static string Token { get; set; }
    
    public static async Task Main()
    {
        _client.Log += Log;
        
        var token = "";
        
        // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
        // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
        // var token = File.ReadAllText("token.txt");
        // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }
    
    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}

public class ConfigurationBuilder
{
}

