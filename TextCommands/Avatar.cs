using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Commands.Text
{
    public class Avatar : TextCommand
    {
        // really simple command
        public Avatar()
        {
            command.Name = "avatar";
            command.WithDMPermission(true);
        }

        public override async void HandleExecute(SocketCommandContext context)
        {
            if (context.Message.MentionedUsers.Any())
                await Reply(context.Message.MentionedUsers.First().GetAvatarUrl(), messageReference: new MessageReference(context.Message.Id), allowedMentions: AllowedMentions.None);
            else
                await Reply(context.User.GetAvatarUrl(), messageReference: new MessageReference(context.Message.Id), allowedMentions: AllowedMentions.None);
        }
    }

}