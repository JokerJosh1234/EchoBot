# EchoBot

> **Note:** Ephemeral messages (messages that are hidden from other users) cannot be used with text commands. Please be cautious about the information you display.

> **Tip:** For bot hosting instructions, refer to the [deployment guide](https://discordnet.dev/guides/deployment/deployment.html).

<details>
<summary>Built-in Commands</summary>
<br>
EchoBot has built-in commands to help developers setup and use custom commands.

### Slash Commands

Slash commands offer a modern, integrated way to use bot features directly from the chat input box in Discord, providing users with immediate access to bot functionalities.

| Command            | Description                               | Usage                                        |
|--------------------|-------------------------------------------|----------------------------------------------|
| `Ban`                | Bans a specified user from the guild      | `/ban <user> <reason> <keep_messages>`       |
| `ModalExample`       | Demonstrates a Modal and its usage        | `/modal-example`                             |
| `SelectMenuExample`  | Shows a SelectMenu and its usage          | `/select-menu-example`                       |

### Text Commands

Text commands are prefixed with a specific character (e.g., `?`) and allow users to interact with the bot through traditional text-based input.

| Command         | Description                                                      | Usage                                               |
|-----------------|------------------------------------------------------------------|-----------------------------------------------------|
| `Avatar`          | Displays the user's profile picture                              | `?avatar` <br> `?avatar <user>`                     |
| `GuildInfo`       | Shows information about the guild                                | `?guild-info`                                       |
| `Lockdown`        | Locks down all or specified channels accessible to everyone      | `?lockdown` <br> `?lockdown <channel>` <br> `?lockdown end` |
| `MentionExample`  | Demonstrates working with mentions in a text command             | `?mention` <br> `?mention <user>` <br> `?mention <role>` <br> `?mention <channel>` |
| `Ping`            | Determines the bot's ping/latency                                | `?ping`                                             |
| `User`            | Provides information on a user                                   | `?user` <br> `?user <user>`                         |

Commands enclosed in angle brackets `< >` are placeholders for the user to replace with specific details. Commands listed with multiple lines indicate alternative usages or options.

</details>
