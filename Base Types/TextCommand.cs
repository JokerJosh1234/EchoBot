using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Discord
{
    public class TextCommandBuilder
    {
        public string Name { get; set; } = string.Empty;
        public GuildPermission DefaultMemberPermissions { get; private set; } = GuildPermission.SendMessages;
        public bool IsDMEnabled { get; private set; } = false;
        public void WithDefaultMemberPermissions(GuildPermission permissions) =>
            DefaultMemberPermissions = permissions;

        public void WithDMPermission(bool permission) =>
            IsDMEnabled = permission;
    }
}

public abstract class TextCommand
{
    public TextCommandBuilder command = new TextCommandBuilder();
    protected ICommandContext Context { get; private set; }
    public void Execute(SocketCommandContext context)
    {
        Context = context;
        HandleExecute(context);
        Console.WriteLine($"Executed text command: {context.Message.Content.Split(' ')[0].TrimStart('?')}");
    }

    public abstract void HandleExecute(SocketCommandContext context);

    public async Task<IUserMessage> Reply(string text = null, bool isTTS = false, Embed embed = null, RequestOptions options = null, AllowedMentions allowedMentions = null, MessageReference messageReference = null, MessageComponent components = null, ISticker[] stickers = null, Embed[] embeds = null, MessageFlags flags = MessageFlags.None) =>
        await Context?.Channel?.SendMessageAsync(text, isTTS, embed, options, allowedMentions, messageReference, components, stickers, embeds, flags);
}
