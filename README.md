# EchoBot

> [!NOTE]
> Text commands cannot use ephemeral (hide messages from other users) so be careful what information you show.

> [!TIP]
> To host the bot, navigate to the [deployment guide](https://discordnet.dev/guides/deployment/deployment.html)

<details>
<summary>Built-in Commands</summary>
- If a usage is encased in [], it means theres multiple variants of the command
  
### Slash Commands

| Command | Description | Usage |
| --- | --- | --- |
| Ban | Bans a spedified user from the guild | /ban `user` `reason` `keep_messages` |
| ModalExample | Displays a Modal and how to use it | /modal-example |
| SelectMenuExample | Displays a SelectmMenu and how to use it | /select-menu-example |

### Text Commands
- Assuming '?' is the set prefix.

| Command | Description | Usage |
| --- | --- | --- |
| Avatar | Displays the users profile picture | [?avatar] [?avatar `user`] |
| GuildInfo | Displays info about a guild | ?guild-info |
| Lockdown | Lockdown all channels that everyone has access to, or a specified one.  not recommended for practical use. requires fine tuning to your guilds needs, although it should work for most cases. | [?lockdown]  [?lockdown `channel`]  [?lockdown end] |
| MentionExample | Example on how to work with mentions in a text command | [?mention] [?mention `user`] [?mention `role`] [?mention `channel`] |
| Ping | Determine the ping/latency of the bot | ?ping |
| User | Displays info on a user | [?user] [?user `user`] |

  
</details>
