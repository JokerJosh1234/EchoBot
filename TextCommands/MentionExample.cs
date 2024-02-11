using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;

namespace Bot.Commands.Text
{
    public class MentionExample : TextCommand
    {
        public MentionExample()
        {
            command.Name = "mention";
            command.WithDMPermission(true);
        }

        public override async void HandleExecute(SocketCommandContext context)
        {
            if (context.Message.MentionedEveryone)
                await Reply("Hey dont ping everyone!");
            else if (context.Message.MentionedUsers.Any())
                await Reply($"You mentioned {context.Message.MentionedUsers.First().Mention}");
            else if (context.Message.MentionedRoles.Any())
                await Reply($"You mentioned {context.Message.MentionedRoles.First().Mention}");
            else if (context.Message.MentionedChannels.Any())
                await Reply($"You mentioned <#{context.Message.MentionedChannels.First().Id}>");
            else
                await Reply("You didnt mention anyone!");
        }
    }

}