using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Commands.Text
{
    public class GuildInfo : TextCommand
    {
        public GuildInfo()
        {
            command.Name = "guild-info";
        }

        public override async void HandleExecute(SocketCommandContext context)
        {
            // easily reference the guild when using it alot (better than doing context.Guild.thing)
            SocketGuild guild = context.Guild;

            // builder for the embed
            EmbedBuilder embed = new EmbedBuilder()
                .WithAuthor(new EmbedAuthorBuilder()
                    .WithIconUrl(guild.IconUrl) // set to the guilds icon
                    .WithName(guild.Name)) // set to the guilds name
                .WithFooter("Example on how to access guild data");

            // displays the guild owner
            embed.AddField("Owner", guild.Owner.ToString(), true);

            // display the guilds creation date
            embed.AddField("Created", guild.CreatedAt.DateTime.ToString(), true);

            // displays the vanity link for the guild, if theres none then it will get or create a perm invite link
            embed.AddField("Vanity URL", !string.IsNullOrEmpty(context.Guild.VanityURLCode) ? $"{context.Guild.VanityURLCode}" : $"No vanity link found [perm invite (non vanity)]({GetOrCreateNonVanityInviteAsync(context.Guild)})", true);

            // display the current channel where the message was sent
            embed.AddField("Current Channel", context.Channel.Socket().Mention, true);

            // display the guilds member count
            embed.AddField("Member Count", context.Guild.MemberCount, true);

            // display the number of boosts (if any)
            embed.AddField("Boosts", context.Guild.PremiumSubscriptionCount != 0 ? context.Guild.PremiumSubscriptionCount.ToString() : "No Server Boosts", true);

            // display text and voice channel counts
            embed.AddField("Channels", $"Text - {context.Guild.TextChannels.Count}, Voice - {context.Guild.VoiceChannels.Count}", true);

            // display the guild features (theres not much use for this afaik)
            embed.AddField("Features", context.Guild.Features.Value, true);

            // display the rules channel (if any)
            embed.AddField("Rules Channel", guild.RulesChannel?.Mention ?? "None set", true);

            // display the boost tier
            embed.AddField("Boost Tier", guild.PremiumTier, true);

            // displays the guilds roles (should skip @everyone)
            embed.AddField("Roles", guild.Roles.Count > 1 ? string.Join(", ", guild.Roles.Skip(1).Select(role => role.Mention)) : "None", true);

            embed.WithCurrentTimestamp();

            // sends the embed in the channel (cannot use ephemeral because its not replying to an interaction)
            await Reply(embed: embed.Build(), messageReference: new MessageReference(context.Message.Id), allowedMentions: AllowedMentions.None);
        }

        public string GetOrCreateNonVanityInviteAsync(SocketGuild guild)
        {
            if (!string.IsNullOrEmpty(guild.VanityURLCode)) // check if the guild has a vanity url
                return guild.VanityURLCode; // return the vanity url

            // attempt to retrieve a perm invite for the guild
            RestInviteMetadata permanentInvite = guild.GetInvitesAsync().Result.FirstOrDefault(invite => !invite.IsTemporary);

            // if a permanent invite is found, return its url
            if (permanentInvite != null)
                return permanentInvite.Url;
            // if no permanent invite is found, create a new perm invite for the default channel and return the url
            else
                return guild.DefaultChannel.CreateInviteAsync(isTemporary: false, maxAge: 0).Result.Url;
        }

    }

}