import os
import sys
import cmds
import db
import random
from twitchio.ext import commands

db.init_database()
cs = db.select_commands()
greeted = []

bot = commands.Bot(
    irc_token=os.environ['TMI_TOKEN'],
    client=os.environ['CLIENT_ID'],
    nick=os.environ['BOT_NICK'],
    prefix=os.environ['BOT_PREFIX'],
    initial_channels=[os.environ['CHANNEL']]
)

@bot.event
async def event_ready():
    print(f"{os.environ['BOT_NICK']} is online!")
    ws = bot._ws
    await ws.send_privmsg(os.environ['CHANNEL'], f"/me has entered the chat!")
    for c in cs:
        c.add_command(bot)

@bot.event
async def event_message(ctx):

    if ctx.author.name.lower() == os.environ['BOT_NICK'].lower():
        return

    await bot.handle_commands(ctx)

    for greet in ['hi', 'hello', 'hey', 'hoi', 'hola', 'hallo']:
        if greet in ctx.content.lower().replace('!', '').replace(',', '').split(' ') and ctx.author.name not in greeted:
            greeted.append(ctx.author.name)
            print(f"Greeted {ctx.author.name}")
            await ctx.channel.send(f'Hello, {ctx.author.name}!')

async def is_moderator(ctx):
    if ctx.author.is_mod:
        print(f"Permission granted to {ctx.author.name}")
        return True
    else:
        print(f"Permission denied to {ctx.author.name}")
        await ctx.send(f"@{ctx.author.name}: You do not have permission to use this command!")
        return False

@commands.core.check(is_moderator)
@bot.command(name='add')
async def add(ctx):
    _, name, message = ctx.content.split(' ', 2)
    new_command = cmds.TextCmd(name.lower(), message)
    cs.append(new_command)
    new_command.add_command(bot)
    db.insert_command(new_command)
    print(f"Added new command {name} : {message}")
    await ctx.send(f"New command '{name}' was created")

@commands.core.check(is_moderator)
@bot.command(name='edit')
async def edit(ctx):
    _, name, message = ctx.content.split(' ', 2)
    for c in filter(lambda x: x.name == name, cs):
        c.message = message
        db.update_command(c)
        print(f"Edit command {name} : {message}")
        await ctx.send(f"Command '{name}' was updated")
        return

@commands.core.check(is_moderator)
@bot.command(name='delete')
async def delete(ctx):
    _, name = ctx.content.split(' ', 1)
    for c in filter(lambda x: x.name == name, cs):
        db.delete_command(c)
        print(f"Deleted command {name}")
        await ctx.send(f"Command '{name}' was removed")
        return

@bot.command(name='quote')
async def quote(ctx):
    q = db.select_quote()
    qt, date = q
    print(f"Fetched a quote: {qt}")
    await ctx.send(f"'{qt} ', {date}")

@commands.core.check(is_moderator)
@bot.command(name='addquote')
async def addquote(ctx):
    _, quote = ctx.content.split(' ', 1)
    db.insert_quote(quote, ctx.message.timestamp)
    print(f"Added quote: {quote}")
    await ctx.send(f"Added new quote: '{quote} '")

@commands.core.check(is_moderator)
@bot.command(name='deletequote')
async def deletequote(ctx):
    _, quote = ctx.content.split(' ', 1)
    db.delete_quote(quote)
    print(f"Removed quote: {quote}")
    await ctx.send(f"Quote '{quote} ' was removed!")

@commands.core.check(is_moderator)
@bot.command(name="killbot")
async def killbot(ctx):
    await ctx.send("Goodbye!")
    stop_bot()
    

@bot.command(name="help")
async def help(ctx):
    txt = "Available Commands: !quote, "
    for c in cs:
        txt += "!" + c.name + ", "
    print("Printed help")
    await ctx.send(txt)

def stop_bot():
    print("Terminated bot!")
    sys.exit()

if __name__ == "__main__":
    bot.run()