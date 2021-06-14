using System;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class Commands : ModuleBase
    {
        [Command("fuck")]
        public async Task InsultBack() 
        {
            await Context.Channel.SendMessageAsync("no u beastly bastard");
        }
    }
}
