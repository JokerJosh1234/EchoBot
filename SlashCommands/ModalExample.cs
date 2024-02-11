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
    public class ModalExample : SlashCommand
    {
        public ModalExample()
        {
            command.Name = "modal-example";
            command.Description = "Example on how to use a modal";
        }

        public override async void HandleExecute(SocketSlashCommand command)
        {
            ModalBuilder builder = new ModalBuilder()
                .WithTitle("Example Modal")
                .WithCustomId("example");

            //                   label          custom id
            builder.AddTextInput("Whats is this", "wit", TextInputStyle.Short, "idk");

            builder.AddTextInput("Another thing", "at", TextInputStyle.Short, "bluh");
            builder.AddTextInput("Why is this here", "with", TextInputStyle.Paragraph, "what should i put here");

            await RespondWithModal(builder.Build());
        }

        public override async void OnModalSubmit(SocketModal modalResponse)
        {
            EmbedBuilder embed = new EmbedBuilder()
                .WithTitle("Questions")
                .AddField("What is this", modalResponse.Data.Get("wit"))
                .AddField("Another thing", modalResponse.Data.Get("at"))
                .AddField("Why is this here", modalResponse.Data.Get("with"));

            embed.WithCurrentTimestamp();

            await Reply(embed: embed.Build());
        }
    }
}
