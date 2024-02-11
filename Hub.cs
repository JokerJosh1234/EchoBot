using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public class Hub
{
    public static DiscordSocketClient client;

    // prefix for text commands
    private const char prefix = '?';
    // this can be found on youre bot application under 'Bot'. You may have to reset the token.
    private const string token = "";

    public static async Task Main(string[] args) => await new Hub().MainAsync();

    public async Task MainAsync()
    {
        client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.DirectMessages | GatewayIntents.MessageContent | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences | GatewayIntents.GuildVoiceStates
        });

        client.Log += Log;
        client.Ready += Client_Ready;
        client.SlashCommandExecuted += SlashCommandHandler;
        client.MessageReceived += HandleCommandAsync;
        client.SelectMenuExecuted += SelectMenuExecuted;
        client.ModalSubmitted += ModalSubmitted;

        await Login();

        await Task.Delay(-1);
    }

    public static async Task Login()
    {
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
    }

    public static async Task Logout()
    {
        await client.StopAsync();
        await client.LogoutAsync();
        client.Dispose();
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private static async Task ModalSubmitted(SocketModal arg)
    {
        if (Context.interactionContexts.TryGetValue(arg.Data.CustomId, out Context.ContextComponent slashCommandContext))
        {
            if (slashCommandContext.Type == ComponentType.ActionRow)
            {
                slashCommandContext.Context.OnModal(arg);
                Context.interactionContexts.TryRemove(arg.Data.CustomId, out _);
            }
            else
                await arg.RespondAsync($"Error executing the modal. Expected ActionRow but got {slashCommandContext.Type}");
        }
    }

    private static async Task SelectMenuExecuted(SocketMessageComponent arg)
    {
        if (Context.interactionContexts.TryGetValue(arg.Data.CustomId, out Context.ContextComponent slashCommandContext))
        {
            if (slashCommandContext.Type == ComponentType.SelectMenu)
            {
                slashCommandContext.Context.OnSelectMenu(arg);
                Context.interactionContexts.TryRemove(arg.Data.CustomId, out _);
            }
            else
                await arg.RespondAsync("Error executing the select menu. Incorrect component type.");
        }
        else
            await arg.RespondAsync("Error: Command context not found.");
    }

    private static async Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;
        if (message == null) return;

        int argPos = 0;

        if (!(message.HasCharPrefix(prefix, ref argPos) || message.Author.IsBot))
            return;

        var context = new SocketCommandContext(client, message);

        var commands = LoadTextCommands();
        var commandName = message.Content.Substring(argPos).Split(' ').First();
        TextCommand slashCommand = commands?.Find(x => x.command.Name == commandName);
        if (slashCommand == null) return;

        if (message.Channel.GetChannelType() != ChannelType.DM)
        {
            if ((context.User as SocketGuildUser).GuildPermissions.Has(slashCommand.command.DefaultMemberPermissions))
            {
                slashCommand?.Execute(context);
                return;
            }

            /// remove this if you dont want the bot to respond to the user when they dont have the correct perms
            var messageReference = new MessageReference(context.Message.Id);
            Discord.Rest.RestUserMessage tempMessage = await context.Channel.SendMessageAsync("You dont have permission to execute this command.", messageReference: messageReference);
            await Task.Delay(2000);
            await tempMessage.DeleteAsync();
            ///
        }
        else if (slashCommand.command.IsDMEnabled)
            slashCommand?.Execute(context);
    }

    private static async Task SlashCommandHandler(SocketSlashCommand command)
    {
        try
        {
            var commands = LoadSlashCommandBuilders();
            SlashCommand slashCommand = commands?.Find(x => x.command.Name == command.CommandName);
            if (slashCommand == null) return;
            slashCommand?.Execute(command);
            Console.WriteLine($"Executed slash command: {command.CommandName}");
        }
        catch (Exception e)
        {
            await command.RespondAsync($"Error: {e}", ephemeral: true);
            await Log(new LogMessage(LogSeverity.Error, "SlashCommandHandler", "Error Executing Command", e));
        }
    }

    private static List<SlashCommand> LoadSlashCommandBuilders()
    {
        var commandBuilders = new List<SlashCommand>();
        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            if (type.IsSubclassOf(typeof(SlashCommand)) && !type.IsAbstract)
            {
                var constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    var commandBuilderInstance = (SlashCommand)constructor.Invoke(null);
                    if (commandBuilderInstance != null)
                    {
                        commandBuilders.Add(commandBuilderInstance);
                    }
                }
            }
        }

        return commandBuilders;
    }

    private static List<TextCommand> LoadTextCommands()
    {
        var commandBuilders = new List<TextCommand>();
        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            if (type.IsSubclassOf(typeof(TextCommand)) && !type.IsAbstract)
            {
                var constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    var commandBuilderInstance = (TextCommand)constructor.Invoke(null);
                    if (commandBuilderInstance != null)
                    {
                        commandBuilders.Add(commandBuilderInstance);
                    }
                }
            }
        }

        return commandBuilders;
    }

    private static async Task Client_Ready()
    {
        var commands = LoadSlashCommandBuilders();
        if (commands == null)
            Console.WriteLine($"No commands found");

        try
        {
            List<ApplicationCommandProperties> applicationCommandProperties = new List<ApplicationCommandProperties>();
            for (int i = 0; i < commands.Count; i++)
            {
                applicationCommandProperties.Add(commands[i].command.Build());
                Console.WriteLine($"Loaded slash command {commands[i].command.Name}");
            }
            await client.BulkOverwriteGlobalApplicationCommandsAsync(applicationCommandProperties.ToArray());
        }
        catch (HttpException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }
    }

    public static async Task SetActivity(string name, ActivityType type, ActivityProperties flags, string details) => await client.SetActivityAsync(new Game(name, type, flags, details));
    public static async Task SetCustomStatus(string status) => await client.SetCustomStatusAsync(status);
    public static async Task SetGameStatus(string status, string streamUrl = null, ActivityType activity = ActivityType.Playing) => await client.SetGameAsync(status, streamUrl, activity);
}
