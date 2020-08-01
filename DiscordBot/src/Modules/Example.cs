using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Modules
{
    [Name("Example")]
    public class ExampleModule : ModuleBase<SocketCommandContext>
    {
        [Command("say"), Alias("s")]
        [Summary("Makes the bot repeat after you.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Say([Remainder] string text)
        {
            await Context.Message.DeleteAsync();
            await ReplyAsync(text);
        }

        [Group("set"), Name("Example")]
        [RequireContext(ContextType.Guild)]
        public class Set : ModuleBase
        {
            [Command("nick"), Priority(0)]
            [Summary("Change your nickname to your message.")]
            [RequireUserPermission(GuildPermission.ChangeNickname)]
            public async Task AuthorNick([Remainder] string name)
            {
                await Context.Message.DeleteAsync();
                var user = Context.User as SocketGuildUser;
                if (user == null)
                {
                    await ReplyAsync("Guild member object could not be found.");
                }
                else
                {
                    await user.ModifyAsync(x => x.Nickname = name);
                    await ReplyAsync($"{Context.User.Mention} has a new nickname!");
                }
            }

            [Command("nick"), Priority(1)]
            [Summary("Change another user's nickname to the specified test.")]
            [RequireUserPermission(GuildPermission.ManageNicknames)]
            public async Task OtherNick(SocketGuildUser user, [Remainder] string name)
            {
                await Context.Message.DeleteAsync();
                Console.WriteLine(user.Username);
                await user.ModifyAsync(x => x.Nickname = name);
                await ReplyAsync($"{user.Mention}, I've updated your nickname to `{name}` at {Context.User.Mention}'s request!");
            }
        }
    }
}