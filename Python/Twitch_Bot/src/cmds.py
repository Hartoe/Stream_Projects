import os
import random
from twitchio.ext import commands

magic_answer = [
    "As I see it, yes.",
    "Ask again later.",
    "Better not tell you now.",
    "Cannot predict now.",
    "Concentrate and ask again.",
    "Don’t count on it.",
    "It is certain.",
    "It is decidedly so.",
    "Most likely.",
    "My reply is no.",
    "My sources say no.",
    "Outlook not so good.",
    "Outlook good.",
    "Reply hazy, try again.",
    "Signs point to yes.",
    "Very doubtful.",
    "Without a doubt.",
    "Yes.",
    "Yes – definitely.",
    "You may rely on it."
]

class Cmd:
    def __init__(self, name, message):
        self.name = name
        self.message = message

    def add_command(self, bot):
        bot.add_command(commands.Command(name=self.name, func=self.generator(), aliases=None, instance=None))

    def generator(self):
        async def empty(ctx):
            await ctx.send('')
        return empty

class TextCmd(Cmd):
    def generator(self):
        async def print_message(ctx):
            print(f"Activated command {self.name}")
            if self.name == "8ball":
                _, msg = ctx.content.split(' ', 1)
                msg = "\"" + msg + "\" " + self.message.replace("%magic%", random.choice(magic_answer))
            else:
                msg = self.message.replace("%name%", ctx.author.name)
            await ctx.send(f"{msg}")
        return print_message