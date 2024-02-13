using Discord;
using System.Collections.Concurrent;

public class Context
{
    public class ContextComponent
    {
        public ComponentType Type { get; set; }
        public SlashCommand slashCommand { get; set; }
    }

    public static ConcurrentDictionary<string, ContextComponent> interactionContexts { get; private set; } = new ConcurrentDictionary<string, ContextComponent>();

    public Context(string customId, ComponentType type, SlashCommand context) =>
        interactionContexts.TryAdd(customId, new ContextComponent { Type = type, slashCommand = context });
}