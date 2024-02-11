using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Rest;

namespace Bot.Commands.Text
{
    public class User : TextCommand
    {
        public User()
        {
            command.Name = "user";
        }

        public override async void HandleExecute(SocketCommandContext context)
        {
            // gets the mentioned user (or current user if mention is null) as a SocketGuildUser (gives us more info on the user in a guild context)
            SocketGuildUser mentionedUser = (context.Message.MentionedUsers.FirstOrDefault() as SocketGuildUser) ?? (context.User as SocketGuildUser);

            if (mentionedUser == null)
                return;

            // building the embed
            EmbedBuilder embed = new EmbedBuilder()
                .WithAuthor(new EmbedAuthorBuilder()
                    .WithIconUrl(mentionedUser.GetAvatarUrl()) // sets to the users avatar url
                    .WithName(mentionedUser.Username)) // sets to the users username
                .WithFooter("Example on how to access user data");

            // gets the users status, e.g. Offline, DND, Invisible, Idle
            embed.AddField("Status", mentionedUser.Status.ToString(), true);

            // gets the users badges
            embed.AddField("Flags", mentionedUser.PublicFlags.HasValue ? mentionedUser.PublicFlags.Value.ToString() : "None", true);

            // gets the users display name
            embed.AddField("Global Name", mentionedUser.GlobalName ?? "None", true);

            // self explanatory
            embed.AddField("Server Nickname", mentionedUser.Nickname ?? "None", true);

            // gets the time when the user was joined discord (aka account creation date)
            embed.AddField("Created At", mentionedUser.CreatedAt.DateTime.ToString(), true);

            // gets if the user is boosting the server 
            embed.AddField("Server Booster", mentionedUser.PremiumSince.HasValue, true);

            // gets when the user joined the server
            embed.AddField("Joined At", mentionedUser.JoinedAt.HasValue ? mentionedUser.JoinedAt.Value.DateTime.ToString() : "Not obtainable at this time", true);

            // self explanatory
            embed.AddField("Actions", $"Timed out: {mentionedUser.TimedOutUntil.HasValue} | Server Deafened (VC): {mentionedUser.IsDeafened} | Server Muted (VC): {mentionedUser.IsMuted} | Streaming or Videoing: {mentionedUser.IsStreaming}", true);

            // lists roles that the user has (should skip @everyone)
            embed.AddField("Roles", mentionedUser.Roles.Count > 1 ? string.Join(", ", mentionedUser.Roles.Skip(1).Select(role => role.Name)) : "None", true);

            embed.WithCurrentTimestamp();

            // sends embed (cannot use ephemeral because its not replying to an interaction)
            await Reply(embed: embed.Build(), messageReference: new MessageReference(context.Message.Id), allowedMentions: AllowedMentions.None);
        }
    }

}