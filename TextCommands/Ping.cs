using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Commands.Text
{
    public class Ping : TextCommand
    {
        public Ping() 
        { 
            command.Name = "ping"; 
            command.WithDMPermission(true); 
        }

        public override async void HandleExecute(SocketCommandContext context)
        {
            DateTimeOffset start = DateTimeOffset.Now;
            IUserMessage message = await Reply("Pong!");
            await message.Edit($"Pong! `{(DateTimeOffset.Now - start).Milliseconds}ms`");
        }
    }
}