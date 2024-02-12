# EchoBot: Elevate Your Discord Experience

**EchoBot** offers an easy way for C# developers to create a custom Discord bot, streamlining the process with simplicity and efficiency in mind.

[![Deployment Guide](https://img.shields.io/badge/Deployment-Guide-blue.svg)](https://discordnet.dev/guides/deployment/deployment.html)

## Table of Contents
- [Setup](#setup)
- [Overview](#overview)
- [Custom Slash Command Creation](#custom-slash-command-creation)
- [Built-in Commands](#built-in-commands)
- [Community-Made Plugins](#community-made-plugins)

## Setup
1. **Create a Console Application**
2. **Install Discord.Net**, instructions can be found [here](https://discordnet.dev/guides/getting_started/installing.html)
3. **Setup your bot**, instructions can be found [here](https://discordnet.dev/guides/getting_started/first-bot.html) - Do not worry about the `Connecting to Discord` step or anything after.
4. **Download EchoBot's files** and import it into your project/application.
5. **Navigate to the `Hub.cs` script** and modify the token param to match your bot's token. You can also change the prefix, which is used for text commands.
6. **To use your app without hosting** (basically local hosting), you can build your application and run the .exe
7. **Have Fun!**

## Overview

<details>
<summary><b>Hub.cs</b>: This script manages the bot's functions and contains methods for modifying the bot.</summary>

- `Hub.client`
- `Hub.SetActivity(<name> <type> <flags> <details>)`
- `Hub.SetCustomStatus(<status>)`
- `Hub.SetGameStatus(<status> <streamUrl> <activity>)`
- `Hub.Logout()`
</details>

`Extensions.cs`: Contains quality of life extension methods that just make things easier.

`Context.cs`: Manages contexts for when a modal or select menu is created; data gets removed when it is not needed anymore. This design makes coding modals and select menus easier.

The inherit class `Event` is used not as a command, but has a callback hub. It contains callbacks like ReceivedMessage (commonly known as 'onMessageCreate') and over 60 more callbacks.

## Custom Slash Command Creation

Easily add your own custom slash commands with EchoBot by following a streamlined process designed for C# developers.

<details>
<summary><b>How to Create a Custom Slash Command</b></summary>
<p>

> **Tip:** This tutorial is concise. For a deeper understanding, explore the built-in example commands.

### Step 1: Script Creation

Begin with a script named 'Example' using this template:

```cs
using Discord;
using Discord.Commands;
using Discord.WebSocket;

public class Example : SlashCommand
{
    public Example() { }
    public override void HandleExecute(SocketSlashCommand command) { }
}
```

### Step 2: Command Definition

Define your command with a unique name and description:

```cs
public class Example : SlashCommand
{
    public Example()
    {
        command.Name = "example";
        command.Description = "This is an example command";
    }
    public override void HandleExecute(SocketSlashCommand command) { }
}
```

### Step 3: Command Reply

Craft a reply to execute upon command invocation:

```cs
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

</p>
</details>

### Text Commands: A Parallel Universe

> **Note:** Ephemeral messages are not compatible with text commands. Be careful with the information you show.

Text commands are derived from the `TextCommand` class and use a `SocketCommandContext` for execution.

## Built-in Commands

EchoBot includes a variety of pre-loaded commands to kickstart your bot development.

<details>
<summary><b>Built-in Commands Overview</b></summary>
<p>

EchoBot facilitates command setup and customization with built-in options.

### Slash Commands

| Command            | Description                             | Usage                                        |
|--------------------|-----------------------------------------|----------------------------------------------|
| `Ban`              | Bans a specified user from the guild    | `/ban <user> <reason> <keep_messages>`       |
| `ModalExample`     | Demonstrates a Modal and its usage      | `/modal-example`                             |
| `SelectMenuExample`| Shows a SelectMenu and its usage        | `/select-menu-example`                       |
| `Echo`             | Sends a message as the bot              | `/echo`                                      |

### Text Commands

Text commands use a configurable prefix, demonstrated here as `?`.

| Command           | Description                                                      | Usage                                                                              |
|-------------------|------------------------------------------------------------------|------------------------------------------------------------------------------------|
| `Avatar`          | Displays the user's profile picture                              | `?avatar` <br> `?avatar <user>`                                                    |
| `GuildInfo`       | Shows information about the guild                                | `?guild-info`                                                                      |
| `Lockdown`        | Locks down all or specified channels accessible to everyone      | `?lockdown` <br> `?lockdown <channel>` <br> `?lockdown end`                        |
| `MentionExample`  | Demonstrates working with mentions in a text command             | `?mention` <br> `?mention <user>` <br> `?mention <role>` <br> `?mention <channel>` |
| `Ping`            | Determines the bot's ping/latency                                | `?ping`                                                                            |
| `User`            | Provides information on a user                                   | `?user` <br> `?user <user>`                                                        |

Commands with `< >` are placeholders for user-specific input.

</p>
</details>

## Community-Made Plugins

Share and discover plugins made by the community. Plugins can also be a way for developers to learn the API.

| Plugin        | Description                                                      | Download                                                                              | 
|---------------|------------------------------------------------------------------|---------------------------------------------------------------------------------------|
| `PlayFab`     | PlayFab API integration to connect to your PlayFab title and moderate players and/or the title data | [link](https://github.com/JokerJosh1234/EchoBot-PlayFab)  |
| `ChatGPT`     | Integrate ChatGPT into your bot                                  | [link](https://github.com/JokerJosh1234/EchoBot-ChatGPT)   |

> **Note:** To submit a plugin, DM [joker.josh](https://discord.com/users/791550177780563998) on Discord.

Plugins offer a platform for the community to share and distribute their creations. Examples of plugins include moderation toolkits, featuring a wide array of commands designed to enhance and streamline moderation tasks.

## Credits

EchoBot was created by [JokerJosh](https://discord.com/users/791550177780563998)
