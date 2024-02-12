using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Rest;
using Discord;

public class Event
{
    public virtual async Task Ready() { }
    public virtual async Task SlashCommandExecuted(SocketSlashCommand command) { }
    public virtual async Task MessageReceived(SocketMessage message) { }
    public virtual async Task SelectMenuExecuted(SocketMessageComponent messageComponent) { }
    public virtual async Task ModalSubmitted(SocketModal modal) { }
    public virtual async Task AutocompleteExecuted(SocketAutocompleteInteraction interaction) { }
    public virtual async Task MessageCommandExecuted(SocketMessageCommand command) { }
    public virtual async Task UserCommandExecuted(SocketUserCommand command) { }
    public virtual async Task ButtonExecuted(SocketMessageComponent component) { }
    public virtual async Task InteractionCreated(SocketInteraction interaction) { }
    public virtual async Task InviteDeleted(SocketGuildChannel channel, string inviteCode) { }
    public virtual async Task InviteCreated(SocketInvite invite) { }
    public virtual async Task PresenceUpdated(SocketUser user, SocketPresence before, SocketPresence after) { }
    public virtual async Task RecipientRemoved(SocketGroupUser user) { }
    public virtual async Task RecipientAdded(SocketGroupUser user) { }
    public virtual async Task UserIsTyping(Cacheable<IUser, ulong> user, Cacheable<IMessageChannel, ulong> channel) { }
    public virtual async Task CurrentUserUpdated(SocketSelfUser before, SocketSelfUser after) { }
    public virtual async Task VoiceServerUpdated(SocketVoiceServer server) { }
    public virtual async Task UserVoiceStateUpdated(SocketUser user, SocketVoiceState before, SocketVoiceState after) { }
    public virtual async Task GuildMemberUpdated(Cacheable<SocketGuildUser, ulong> before, SocketGuildUser after) { }
    public virtual async Task UserUpdated(SocketUser before, SocketUser after) { }
    public virtual async Task UserUnbanned(SocketUser user, SocketGuild guild) { }
    public virtual async Task UserBanned(SocketUser user, SocketGuild guild) { }
    public virtual async Task UserLeft(SocketGuild guild, SocketUser user) { }
    public virtual async Task ApplicationCommandCreated(SocketApplicationCommand command) { }
    public virtual async Task ThreadUpdated(Cacheable<SocketThreadChannel, ulong> before, SocketThreadChannel after) { }
    public virtual async Task ApplicationCommandDeleted(SocketApplicationCommand command) { }
    public virtual async Task EntitlementCreated(SocketEntitlement entitlement) { }
    public virtual async Task AutoModActionExecuted(SocketGuild guild, AutoModRuleAction action, AutoModActionExecutedData data) { }
    public virtual async Task AutoModRuleDeleted(SocketAutoModRule rule) { }
    public virtual async Task AutoModRuleUpdated(Cacheable<SocketAutoModRule, ulong> before, SocketAutoModRule after) { }
    public virtual async Task AutoModRuleCreated(SocketAutoModRule rule) { }
    public virtual async Task AuditLogCreated(SocketAuditLogEntry logEntry, SocketGuild guild) { }
    public virtual async Task WebhooksUpdated(SocketGuild guild, SocketChannel channel) { }
    public virtual async Task GuildStickerDeleted(SocketCustomSticker sticker) { }
    public virtual async Task GuildStickerUpdated(SocketCustomSticker before, SocketCustomSticker after) { }
    public virtual async Task GuildStickerCreated(SocketCustomSticker sticker) { }
    public virtual async Task SpeakerRemoved(SocketStageChannel channel, SocketGuildUser user) { }
    public virtual async Task SpeakerAdded(SocketStageChannel channel, SocketGuildUser user) { }
    public virtual async Task RequestToSpeak(SocketStageChannel channel, SocketGuildUser user) { }
    public virtual async Task StageUpdated(SocketStageChannel before, SocketStageChannel after) { }
    public virtual async Task StageEnded(SocketStageChannel channel) { }
    public virtual async Task StageStarted(SocketStageChannel channel) { }
    public virtual async Task ThreadMemberLeft(SocketThreadUser user) { }
    public virtual async Task ThreadMemberJoined(SocketThreadUser user) { }
    public virtual async Task ThreadDeleted(Cacheable<SocketThreadChannel, ulong> thread) { }
    public virtual async Task UserJoined(SocketGuildUser user) { }
    public virtual async Task ThreadCreated(SocketThreadChannel thread) { }
    public virtual async Task ApplicationCommandUpdated(SocketApplicationCommand command) { }
    public virtual async Task GuildScheduledEventStarted(SocketGuildEvent guildEvent) { }
    public virtual async Task ChannelCreated(SocketChannel channel) { }
    public virtual async Task ChannelDestroyed(SocketChannel channel) { }
    public virtual async Task ChannelUpdated(SocketChannel before, SocketChannel after) { }
    public virtual async Task VoiceChannelStatusUpdated(Cacheable<SocketVoiceChannel, ulong> voiceChannel, string beforeStatus, string afterStatus) { }
    public virtual async Task MessageUpdated(Cacheable<IMessage, ulong> cacheMessage, SocketMessage newMessage, ISocketMessageChannel channel) {  }
    public virtual async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheMessage, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction) {  }
    public virtual async Task ReactionRemoved(Cacheable<IUserMessage, ulong> cacheable, ISocketMessageChannel channel, SocketReaction reaction) { }
    public virtual async Task ReactionsCleared(Cacheable<IUserMessage, ulong> cacheable, ISocketMessageChannel channel) { }
    public virtual async Task ReactionsRemovedForEmote(Cacheable<IUserMessage, ulong> cacheable, ISocketMessageChannel channel, IEmote emote) { }
    public virtual async Task RoleCreated(SocketRole role) { }
    public virtual async Task RoleDeleted(SocketRole role) { }
    public virtual async Task MessageDeleted(Cacheable<IMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel) { }
    public virtual async Task RoleUpdated(SocketRole before, SocketRole after) { }
    public virtual async Task GuildScheduledEventUserRemove(Cacheable<SocketUser, RestUser, IUser, ulong> user, SocketGuildEvent guildEvent) { }
    public virtual async Task GuildScheduledEventUserAdd(Cacheable<SocketUser, RestUser, IUser, ulong> user, SocketGuildEvent guildEvent) { }
    public virtual async Task GuildScheduledEventCompleted(SocketGuildEvent guildEvent) { }
    public virtual async Task GuildScheduledEventCancelled(SocketGuildEvent guildEvent) { }
    public virtual async Task GuildScheduledEventUpdated(Cacheable<SocketGuildEvent, ulong> before, SocketGuildEvent after) { }
    public virtual async Task GuildScheduledEventCreated(Cacheable<SocketGuildEvent, ulong> before, SocketGuildEvent after) { }
}
