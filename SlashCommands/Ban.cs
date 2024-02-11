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
    public class Ban : SlashCommand
    {
        public Ban()
        {
            // required stuff
            command.Name = "ban";
            command.Description = "Ban a user from the server";

            // define options
            command.AddOption("user", ApplicationCommandOptionType.User, "User to ban", true);
            command.AddOption("reason", ApplicationCommandOptionType.String, "Reason for the ban");
            command.AddOption("keep_messages", ApplicationCommandOptionType.Boolean, "Keep the user messages after ban");

            // no one without admin perms can see the command or use it
            command.WithDefaultMemberPermissions(GuildPermission.BanMembers);

            // no one will be able to see or use this command in dms
            command.WithDMPermission(false);
        }

        public override async void HandleExecute(SocketSlashCommand command)
        {
            SocketGuild guild = Hub.client.GetGuild(command.GuildId ?? 0);

            SocketGuildUser user = (IUser)command.GetOption("user").Value as SocketGuildUser;

            if (user.GuildPermissions.Has(GuildPermission.BanMembers))
            {
                await Reply($"Cannot ban a moderator", ephemeral: true);
                return;
            }

            bool keep_messages = (bool?)command.GetOption("keep_messages")?.Value ?? false;
            string reason = (string)command.GetOption("reason")?.Value ?? "No reason provided.";

            // for some reason it doesnt support length for bans, will look into later
            await guild.AddBanAsync(user, keep_messages ? 0 : 7, reason);

            await Reply($"Banned {user.Username}!", ephemeral: true);
        }
    }
}
