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

    // can make static if needed
    public List<SlashCommand> slashCommands = new List<SlashCommand>();
    public List<TextCommand> textCommands = new List<TextCommand>();
    public List<Event> events = new List<Event>();

    public async Task MainAsync()
    {
        client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.DirectMessages | GatewayIntents.MessageContent | GatewayIntents.GuildMembers | GatewayIntents.GuildPresences | GatewayIntents.GuildVoiceStates
        });

        client.Log += Log;
        client.Ready += Ready;
        client.SlashCommandExecuted += SlashCommandHandler;
        client.MessageReceived += HandleCommandAsync;
        client.SelectMenuExecuted += SelectMenuExecuted;
        client.ModalSubmitted += ModalSubmitted;

        LoadCommands();

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
                slashCommandContext.slashCommand.OnModal(arg);
                Context.interactionContexts.TryRemove(arg.Data.CustomId, out _);
            }
            else
                await arg.RespondAsync($"Error executing the modal. Expected ActionRow but got {slashCommandContext.Type}");
        }
        else
            await arg.RespondAsync("Error: Command context not found.", ephemeral: true);
    }

    private static async Task SelectMenuExecuted(SocketMessageComponent arg)
    {

        if (Context.interactionContexts.TryGetValue(arg.Data.CustomId, out Context.ContextComponent slashCommandContext))
        {
            if (slashCommandContext.Type == ComponentType.SelectMenu)
                slashCommandContext.slashCommand.OnSelectMenu(arg);
            else
                await arg.RespondAsync("Error executing the select menu. Incorrect component type.");
        }
        else
            await arg.RespondAsync("Error: Command context not found.", ephemeral: true);
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;
        if (message == null) return;

        int argPos = 0;

        if (!(message.HasCharPrefix(prefix, ref argPos) || message.Author.IsBot))
            return;

        var context = new SocketCommandContext(client, message);

        var commandName = message.Content.Substring(argPos).Split(' ').First();
        TextCommand slashCommand = textCommands?.Find(x => x.command.Name == commandName);
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

    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        try
        {
            SlashCommand slashCommand = slashCommands?.Find(x => x.command.Name == command.CommandName);
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

    public void LoadCommands()
    {
        var assembly = Assembly.GetExecutingAssembly();

        textCommands = assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(TextCommand)) && !type.IsAbstract)
            .Select(type => Activator.CreateInstance(type) as TextCommand)
            .Where(instance => instance != null)
            .ToList();

        slashCommands = assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(SlashCommand)) && !type.IsAbstract)
            .Select(type => Activator.CreateInstance(type) as SlashCommand)
            .Where(instance => instance != null)
            .ToList();

        events = assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Event)) && !type.IsAbstract)
            .Select(type => Activator.CreateInstance(type) as Event)
            .Where(instance => instance != null)
            .ToList();

        SubscribeToEventCallbacks();
    }

    private async Task Ready()
    {
        try
        {
            List<ApplicationCommandProperties> applicationCommandProperties = new List<ApplicationCommandProperties>();
            for (int i = 0; i < slashCommands.Count; i++)
            {
                applicationCommandProperties.Add(slashCommands[i].command.Build());
                Console.WriteLine($"Loaded slash command {slashCommands[i].command.Name}");
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


    // this is ugly pls dont look any further
    public void SubscribeToEventCallbacks()
    {
        foreach(Event x in events)
        {
            client.Ready += x.Ready;
            client.SlashCommandExecuted += x.SlashCommandExecuted;
            client.MessageReceived += x.MessageReceived;
            client.SelectMenuExecuted += x.SelectMenuExecuted;
            client.ModalSubmitted += x.ModalSubmitted;
            client.AutocompleteExecuted += x.AutocompleteExecuted;
            client.MessageCommandExecuted += x.MessageCommandExecuted;
            client.UserCommandExecuted += x.UserCommandExecuted;
            client.ButtonExecuted += x.ButtonExecuted;
            client.InteractionCreated += x.InteractionCreated;
            client.InviteDeleted += x.InviteDeleted;
            client.InviteCreated += x.InviteCreated;
            client.PresenceUpdated += x.PresenceUpdated;
            client.RecipientRemoved += x.RecipientRemoved;
            client.RecipientAdded += x.RecipientAdded;
            client.UserIsTyping += x.UserIsTyping;
            client.CurrentUserUpdated += x.CurrentUserUpdated;
            client.VoiceServerUpdated += x.VoiceServerUpdated;
            client.UserVoiceStateUpdated += x.UserVoiceStateUpdated;
            client.GuildMemberUpdated += x.GuildMemberUpdated;
            client.UserUpdated += x.UserUpdated;
            client.UserUnbanned += x.UserUnbanned;
            client.UserBanned += x.UserBanned;
            client.UserLeft += x.UserLeft;
            client.ApplicationCommandCreated += x.ApplicationCommandCreated;
            client.ThreadUpdated += x.ThreadUpdated;
            client.ApplicationCommandDeleted += x.ApplicationCommandDeleted;
            client.EntitlementCreated += x.EntitlementCreated;
            client.AutoModActionExecuted += x.AutoModActionExecuted;
            client.AutoModRuleDeleted += x.AutoModRuleDeleted;
            client.AutoModRuleUpdated += x.AutoModRuleUpdated;
            client.AutoModRuleCreated += x.AutoModRuleCreated;
            client.AuditLogCreated += x.AuditLogCreated;
            client.WebhooksUpdated += x.WebhooksUpdated;
            client.GuildStickerDeleted += x.GuildStickerDeleted;
            client.GuildStickerUpdated += x.GuildStickerUpdated;
            client.GuildStickerCreated += x.GuildStickerCreated;
            client.SpeakerRemoved += x.SpeakerRemoved;
            client.SpeakerAdded += x.SpeakerAdded;
            client.RequestToSpeak += x.RequestToSpeak;
            client.StageUpdated += x.StageUpdated;
            client.StageEnded += x.StageEnded;
            client.StageStarted += x.StageStarted;
            client.ThreadMemberLeft += x.ThreadMemberLeft;
            client.ThreadMemberJoined += x.ThreadMemberJoined;
            client.ThreadDeleted += x.ThreadDeleted;
            client.UserJoined += x.UserJoined;
            client.ThreadCreated += x.ThreadCreated;
            client.ApplicationCommandUpdated += x.ApplicationCommandUpdated;
            client.MessageDeleted += async (message, channel) => await x.MessageDeleted(message, channel);
            client.RoleUpdated += async (before, after) => await x.RoleUpdated(before, after);
            client.GuildScheduledEventUserRemove += async (user, guildEvent) => await x.GuildScheduledEventUserRemove(user, guildEvent);
            client.GuildScheduledEventUserAdd += async (user, guildEvent) => await x.GuildScheduledEventUserAdd(user, guildEvent);
            client.GuildScheduledEventCompleted += async (guildEvent) => await x.GuildScheduledEventCompleted(guildEvent);
            client.GuildScheduledEventCancelled += async (guildEvent) => await x.GuildScheduledEventCancelled(guildEvent);
            client.GuildScheduledEventUpdated += async (before, after) => await x.GuildScheduledEventUpdated(before, after);
            client.VoiceChannelStatusUpdated += async (voiceChannel, beforeStatus, afterStatus) => await x.VoiceChannelStatusUpdated(voiceChannel, beforeStatus, afterStatus);
        }
    }
}
