using Discord;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

public abstract class SlashCommand
{
    public SlashCommandBuilder command { get; } = new SlashCommandBuilder();
    protected SocketInteraction interaction { get; private set; }

    public void Execute(SocketSlashCommand command) { interaction = command; HandleExecute(command); }
    public abstract void HandleExecute(SocketSlashCommand context);

    public async Task Reply(string text = null, Embed[] embeds = null, bool isTTS = false, bool ephemeral = false, AllowedMentions allowedMentions = null, MessageComponent components = null, Embed embed = null, RequestOptions options = null)
    {
        if(components != null)
            new Context(components.Components.FirstOrDefault().Components.FirstOrDefault().CustomId, components.Components.FirstOrDefault().Components.FirstOrDefault().Type, this);

        await interaction?.RespondAsync(text, embeds, isTTS, ephemeral, allowedMentions, components, embed, options);
    }

    public async Task RespondWithModal(Modal modal, RequestOptions options = null)
    {
        new Context(modal.CustomId, ComponentType.ActionRow, this);
        await interaction?.RespondWithModalAsync(modal, options);
    }

    public void OnSelectMenu(SocketMessageComponent selectMenuResponse) { interaction = selectMenuResponse; OnSelectMenuExecute(selectMenuResponse); }
    public virtual void OnSelectMenuExecute(SocketMessageComponent selectMenuResponse) { }

    public void OnModal(SocketModal modalResponse) { interaction = modalResponse; OnModalSubmit(modalResponse); }
    public virtual void OnModalSubmit(SocketModal modalResponse) { }
}
