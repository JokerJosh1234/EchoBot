using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Commands.Slash
{
    public class Echo : SlashCommand
    {
        public Echo()
        {
            command.Name = "echo";
            command.Description = "Sends a message as the bot";
            command.AddOption("message", ApplicationCommandOptionType.String, "Message that you want the bot to say", true);
            command.AddOption("reply-id", ApplicationCommandOptionType.String, "ID of the message you want the bot to reply to");

            command.WithDefaultMemberPermissions(GuildPermission.Administrator);

            command.WithDMPermission(false);
        }

        public override async void HandleExecute(SocketSlashCommand command)
        {
            string messageContent = (string)command.GetOption("message").Value;

            IMessage replyMessage = null;
            if (ulong.TryParse((string)command.GetOption("reply-id")?.Value, out ulong replyId) && replyId != 0)
            {
                replyMessage = await command.Channel.GetMessageAsync(replyId);
                if (replyMessage == null)
                {
                    await Reply("No message found with the specified ID to reply to.", ephemeral: true);
                    return;
                }
            }

            await command.Channel.SendMessageAsync(messageContent, messageReference: replyMessage != null ? new MessageReference(replyMessage.Id) : null);

            await Reply("Sent!", ephemeral: true);
        }
    }
}
