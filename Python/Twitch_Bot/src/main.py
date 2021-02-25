import os
import db
import cmds
import tkinter as tk
from tkinter import messagebox
import tkinter.font as tkFont

def start_bot():
    os.system('start cmd /c "pipenv install twitchio && pipenv run py ../src/bot.py"')

def add_command(name, message):
    if name.get().replace(' ', '') == "":
        messagebox.showerror("Empty Name", "Name can not be empty")
    else:
        name.set(name.get().replace(' ', ''))
        db.insert_command(cmds.Cmd(name.get(), message.get()))
        name.set("")
        message.set("")

def init():
    root = tk.Tk()
    root.configure(bg='black')
    root.title("Twitch Bot")
    root.resizable(False, False)
    return root

if __name__ == "__main__":
    root = init()

    name = tk.StringVar()
    message = tk.StringVar()
    font = tkFont.Font(family="Trebuchet MS", size=16)
    font2 = tkFont.Font(family="Trebuchet MS", size=24)

    name_label = tk.Label(root, font=font, text="Name:", bg='black', fg='white')
    message_label = tk.Label(root, font=font, text="Message:", bg='black', fg='white')
    name_label.grid(column=0,row=0,sticky="W")
    message_label.grid(column=1,row=0,sticky="W")

    name_entry = tk.Entry(root, font=font, textvariable=name, bg='gray20', fg='white')
    message_entry = tk.Entry(root, font=font, width=50, textvariable=message, bg='gray20', fg='white')
    add_button = tk.Button(root, font=font, text="Add", command= lambda: add_command(name, message), bg='purple2', fg='white')
    name_entry.grid(column=0, row=1, padx=5)
    message_entry.grid(column=1,row=1, padx=5)
    add_button.grid(column=2,row=1, padx=5)

    start_button = tk.Button(root, font=font2, text="Start Bot", command=start_bot, bg='purple2', fg='white')
    start_button.grid(column=0,row=2,columnspan=3,ipadx=10,ipady=5,pady=5)

    root.mainloop()
