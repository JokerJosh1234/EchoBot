# EchoBot: Elevate Your Discord Experience

**EchoBot** offers an easy way for C# developers to create a custom Discord bot, streamlining the process with simplicity and efficiency in mind.

[![Deployment Guide](https://img.shields.io/badge/Deployment-Guide-blue.svg)](https://discordnet.dev/guides/deployment/deployment.html) 

## Table of Contents
- [Custom Slash Command Creation](#custom-slash-command-creation)
- [Text Commands: A Parallel Universe](#text-commands-a-parallel-universe)
- [Built-in Commands](#built-in-commands)
- [PlayFab Integration](#playfab-integration)
- [Community-Made Plugins](#community-made-plugins)
- [Contributing](#contributing)

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

## Text Commands: A Parallel Universe

> **Note:** Ephemeral messages are not compatible with text commands. Exercise discretion.

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

### Text Commands

Text commands use a configurable prefix, demonstrated here as `?`.

| Command           | Description                                                      | Usage                                               |
|-------------------|------------------------------------------------------------------|-----------------------------------------------------|
| `Avatar`          | Displays the user's profile picture                              | `?avatar` <br> `?avatar <user>`                     |
| `GuildInfo`       | Shows information about the guild                                | `?guild-info`                                       |
| `Lockdown`        | Locks down all or specified channels accessible to everyone      | `?lockdown` <br> `?lockdown <channel>` <br> `?lockdown end` |
| `MentionExample`  | Demonstrates working with mentions in a text command             | `?mention` <br> `?mention <user>` <br> `?mention <role>` <br> `?mention <channel>` |
| `Ping`            | Determines the bot's ping/latency                                | `?ping`                                             |
| `User`            | Provides information on a user                                   | `?user` <br> `?user <user>`                         |


Commands with `< >` are placeholders for user-specific input.

</p>
</details>

## PlayFab Integration

EchoBot supports PlayFab API integration for enhanced functionality.

<details>
<summary>Setup and Download</summary>
<p>

Download the integration [here](https://github.com/JokerJosh1234/EchoBot-PlayFab-Integration). Configure the `Playfab.cs` script with your PlayFab `TitleId` and `X_SecretKey`.

</p>
</details>

## Community-Made Plugins

Share and discover plugins made by the EchoBot community.

> **Note:** To submit a plugin, DM joker.josh on Discord.

## Contributing

Your contributions are welcome! Feel free to submit pull requests or suggest features to enhance EchoBot's capabilities.

## Credits

| User               | Contribution                            | 
|--------------------|-----------------------------------------|
| JokerJosh          | Creator                                 |
