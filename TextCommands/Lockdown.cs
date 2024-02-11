using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Commands.Text
{
    public class Lockdown : TextCommand
    {
        public Lockdown() 
        { 
            command.Name = "lockdown"; 

            // only admins can use 
            command.WithDefaultMemberPermissions(GuildPermission.Administrator); 
        }

        // basic lockdown command, not recommended for practical use. requires fine tuning to your guilds needs
        // although it should work for most cases
        // usage: [prefix]lockdown #general (locks down the mention channel, assuming its accessable to everyone)
        // usage: [prefix]lockdown (locks down every channel that everyone can access, should leave moderation channels alone)
        // usage: [prefix]lockdown end (unlocks every channel that everyone can access)
        public override async void HandleExecute(SocketCommandContext context)
        {
            if (context.Message.MentionedChannels.Any())
            {
                foreach (SocketGuildChannel channel in context.Message.MentionedChannels)
                {
                    if (IsChannelAccessibleToEveryone(channel, context.Guild.EveryoneRole))
                        await channel.AddPermissionOverwriteAsync(context.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Deny));
                    else
                        Console.WriteLine($"Skipping channel {channel.Name} because everyone doesn't have access.");
                }
                await Reply($"Locked {string.Join(",", context.Message.MentionedChannels)}");
            }
            else if (context.Message.Content.Contains("end"))
            {
                foreach (SocketGuildChannel channel in context.Guild.TextChannels)
                {
                    OverwritePermissions? permissions = channel.GetPermissionOverwrite(context.Guild.EveryoneRole);
                    if (permissions.HasValue && !IsDefault(permissions.Value) && IsChannelAccessibleToEveryone(channel, context.Guild.EveryoneRole))
                        await channel.RemovePermissionOverwriteAsync(context.Guild.EveryoneRole);
                }
                await Reply($"Lockdown ended for all affected channels.");
            }
            else
            {
                foreach (SocketGuildChannel channel in context.Guild.TextChannels)
                {
                    if (IsChannelAccessibleToEveryone(channel, context.Guild.EveryoneRole))
                        await channel.AddPermissionOverwriteAsync(context.Guild.EveryoneRole, new OverwritePermissions(sendMessages: PermValue.Deny));
                    else
                        await Reply($"Skipping channel {channel.Name} because everyone doesn't have access.");
                }
                await Reply($"Locked {string.Join(", ", context.Guild.TextChannels.Where(c => IsChannelAccessibleToEveryone(c, context.Guild.EveryoneRole)).Select(x => x.Mention))}");
            }
        }

        private bool IsChannelAccessibleToEveryone(SocketGuildChannel channel, SocketRole everyoneRole) =>
            channel?.GetPermissionOverwrite(everyoneRole).GetValueOrDefault().ViewChannel != PermValue.Deny;

        private bool IsDefault(OverwritePermissions permissions) =>
            permissions.SendMessages == PermValue.Inherit && permissions.ViewChannel == PermValue.Inherit;
    }
}