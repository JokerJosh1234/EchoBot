using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Commands.Slash
{
    public class SelectMenuExample : SlashCommand
    {
        public SelectMenuExample()
        {
            command.Name = "select-menu-example";
            command.Description = "Example for a select menu";
        }

        public override async void HandleExecute(SocketSlashCommand command)
        {
            SelectMenuBuilder selectMenu = new SelectMenuBuilder().WithCustomId("example-menu").WithPlaceholder("Select Item");
            selectMenu.AddOption("Example 1", "ex-1");
            selectMenu.AddOption("Example 2", "ex-2");
            selectMenu.AddOption("Example 3", "ex-3");
            ComponentBuilder component = new ComponentBuilder().WithSelectMenu(selectMenu);
            await Reply(components: component.Build());
        }

        // define the select menu callback (required)
        public override async void OnSelectMenuExecute(SocketMessageComponent selectMenuResponse)
        {
            Console.WriteLine(selectMenuResponse.Data.Value);
            await Reply($"Selected {selectMenuResponse.Data.Values.First()}");
        }
    }
}
