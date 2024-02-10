# EchoBot - A simple way to add a bot to your Discord server

**EchoBot** is designed to be a way for c# experienced coders to easily make a custom discord bot. 

> **Tip:** For bot hosting instructions, refer to the [deployment guide](https://discordnet.dev/guides/deployment/deployment.html).

## Custom Slash Command Creation

Creating custom slash commands is a breeze with EchoBot. Follow this simplified process to add your own commands.

<details>
<summary><b>How to Create a Custom Slash Command</b></summary>
<br>
  
> **Tip:** This tutorial is short and doesnt go into detail. Please refer to the example commands that are built in to get a better understanding on how it works.

#### Step 1: Script Creation

Start by naming your script 'Example'. Here’s a basic template to create a slash command:

```cs
using System.Linq;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

public class Example : SlashCommand
{
  public Example()
  {
  }
  public override void HandleExecute(SocketSlashCommand command)
  {
  }
}
```

#### Step 2: Command Definition

For recognition and registration, your command must have a name and a description:

```cs
using System.Linq;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

public class Example : SlashCommand
{
  public Example()
  {
      command.Name = "example";
      command.Description = "This is an example command";
  }
  public override void HandleExecute(SocketSlashCommand command)
  {
  }
}
```

#### Step 3: Command Reply

```cs
using System.Linq;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

public class Example : SlashCommand
{
  public Example()
  {
      command.Name = "example";
      command.Description = "This is an example command";
  }
  public override void HandleExecute(SocketSlashCommand command)
  {
      Reply("You executed the example command!");
  }
}
```

</details>

### Text Commands: A Parallel Universe

> **Note:** Ephemeral messages (messages that are hidden from other users) cannot be used with text commands. Please be cautious about the information you display.
> 
Without the need for a description, they are derived from the `TextCommand` class, with a `SocketCommandContext` parameter in the execution method.

## Built-in Commands

EchoBot comes pre-loaded with a suite of commands to kickstart your bot development journey. Here's a snapshot of what's available:

<details>
<summary><b>Built-in Commands Overview</b></summary>

EchoBot has built-in commands to help developers setup and use custom commands.

### Slash Commands

| Command            | Description                               | Usage                                        |
|--------------------|-------------------------------------------|----------------------------------------------|
| `Ban`                | Bans a specified user from the guild      | `/ban <user> <reason> <keep_messages>`       |
| `ModalExample`       | Demonstrates a Modal and its usage        | `/modal-example`                             |
| `SelectMenuExample`  | Shows a SelectMenu and its usage          | `/select-menu-example`                       |

### Text Commands

For Text Commands, the prefix `?` is used here as an example. The actual prefix can be configured to any character or string according to your servers settings.

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

Happy coding!
