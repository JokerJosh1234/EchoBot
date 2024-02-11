using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Extensions
{
    public static SocketSlashCommandDataOption GetOption(this SocketSlashCommand command, string name) =>
        command.Data.Options.FirstOrDefault(x => x.Name == name);

    //fix for various integer and number errors
    public static int Number(this SocketSlashCommandDataOption option) =>
        (int)Convert.ToInt64(option.Value);

    public static async Task Edit(this IUserMessage message, string newMessage) =>
        await message.ModifyAsync(msg => msg.Content = newMessage);

    public static string Get(this SocketModalData modalData, string id) =>
        modalData.Components.ToList().First(x => x.CustomId == id).Value;

    public static SocketTextChannel Socket(this ISocketMessageChannel channel) =>
        channel as SocketTextChannel;
}
