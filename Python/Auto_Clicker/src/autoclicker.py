import tkinter as tk
from tkinter import ttk
from tkinter import messagebox

from pynput import mouse, keyboard

import time

# Global Variables
m = mouse.Controller()
b = mouse.Button.left
kb = keyboard.Controller()
k = keyboard.Key.space


# Initialization Methods
def init():
    root = tk.Tk()
    root.title("Auto Clicker")
    root.resizable(False, False)
    root.iconbitmap('../images/icon.ico')
    return root

def get_menu_frame(root):
    frame = tk.Frame(root)
    frame.grid(column=0,row=0)

    #Add title label
    title = tk.Label(frame, text="Welcome! This Input Simulator can\n"+
            "simulate mouse and keyboard inputs. Just decide which button\n"+
            "or key to simulate and how fast to keep pressing the key.\n"+
            "Then press start and hey presto! I hope you enjoy!")
    title.grid(column=0,row=0,columnspan=2)

    #Add keyboard button
    keyboard_button = tk.Button(frame, text="Keyboard", command= lambda: goto_keyboard(root, frame))
    keyboard_button.grid(column=0,row=1, ipadx=10, padx=5, pady=5)

    #Add mouse button
    mouse_button = tk.Button(frame, text="Mouse", command= lambda: goto_mouse(root, frame))
    mouse_button.grid(column=1,row=1, ipadx=10, padx=5, pady=5)

    #Add credit label
    credit = tk.Label(frame, text="-Made by HartoeHajek, 25/01/2021", font=('Arial', 7))
    credit.grid(column=0,row=2,sticky="W", columnspan=2)

    return frame

def get_mouse_frame(root):
    frame = tk.Frame(root)
    frame.grid(column=0,row=0)

    #Add back button
    back = tk.Button(frame, text="<-", command= lambda: goto_menu(root, frame))
    back.grid(column=0,row=0,padx=0,sticky='W')

    #Add mouse label
    title = tk.Label(frame, text="Mouse Clicker")
    title.grid(column=1,row=0,columnspan=3,sticky='W')

    #Add combobox
    button = tk.StringVar()
    button_choice = ttk.Combobox(frame, width=15, values=["Left button", "Middle button", "Right button"], textvariable=button)
    button_choice.current(0)
    button_choice.grid(column=0,row=1,columnspan=2,padx=5,pady=5)

    #Add spinbox
    speed.set(1000)
    speed_choice = tk.Spinbox(frame, width=5, from_=0, to=10000, textvariable=speed)
    speed_choice.grid(column=3,row=1)

    #Add ms label
    ms = tk.Label(frame, text="ms")
    ms.grid(column=4,row=1)

    #Add start button
    start_button = tk.Button(frame, fg="green", text="Start", command= lambda: start_mouse_click(button))
    start_button.grid(column=0,row=2,columnspan=2, ipadx=10, padx=5, pady=5)

    #Add stop button
    stop_button = tk.Button(frame, fg="red", text="Stop", command=stop_mouse_click)
    stop_button.grid(column=2,row=2,columnspan=2, ipadx=10, padx=5, pady=5)

    #Add credit label
    credit = tk.Label(frame, text="-Made by HartoeHajek, 25/01/2021", font=('Arial', 7))
    credit.grid(column=0,row=3,sticky="W",columnspan=3)

    return frame

def get_keyboard_frame(root):
    frame = tk.Frame(root)
    frame.grid(column=0,row=0)

    #Add back button
    back = tk.Button(frame, text="<-", command= lambda: goto_menu(root, frame))
    back.grid(column=0,row=0, padx=0, sticky="W")

    #Add keyboard label
    title = tk.Label(frame, text="Keyboard Presser")
    title.grid(column=2,row=0)

    #Add text entry
    button = tk.StringVar()
    button.set('a')
    button_choice = tk.Entry(frame, width=10, textvariable=button)
    button_choice.grid(column=0,row=1,columnspan=2, padx=5, pady=5)

    #Add help button
    help_button = tk.Button(frame, text="?", command=show_help)
    help_button.grid(column=2,row=1,sticky="W")

    #Add spinbox
    speed.set(1000)
    speed_choice = tk.Spinbox(frame, width=5, from_=0, to=10000, textvariable=speed)
    speed_choice.grid(column=3,row=1)

    #Add miliseconds label
    ms = tk.Label(frame, text="ms")
    ms.grid(column=4,row=1)

    #Add start button
    start_button = tk.Button(frame, fg="green", text="Start", command=  lambda: start_keyboard_click(button))
    start_button.grid(column=0,row=2,columnspan=2,ipadx=10,padx=5,pady=5)

    #Add stop button
    stop_button = tk.Button(frame, fg="red", text="Stop", command=stop_keyboard_click)
    stop_button.grid(column=3,row=2,columnspan=2,ipadx=10,padx=5,pady=5)

    #Add credit label
    credit = tk.Label(frame, text="-Made by HartoeHajek, 25/01/2021", font=('Arial', 7))
    credit.grid(column=0,row=3,sticky="W",columnspan=3)

    return frame

# Frame Handling Methods
def goto_keyboard(root, frame):
    frame.destroy()
    get_keyboard_frame(root)

def goto_mouse(root, frame):
    frame.destroy()
    get_mouse_frame(root)

def goto_menu(root, frame):
    frame.destroy()
    get_menu_frame(root)

# Input Simulation Methods
def start_mouse_click(button):
    global b
    if button.get() == "Left button":
        b = mouse.Button.left
    elif button.get() == "Right button":
        b = mouse.Button.right
    else:
        b = mouse.Button.middle
    global click
    click = True

def stop_mouse_click():
    global click
    click = False

def start_keyboard_click(button):
    global k
    if button.get() == "shift":
        k = keyboard.Key.shift
    elif button.get() == "ctrl":
        k = keyboard.Key.ctrl
    elif button.get() == "alt":
        k = keyboard.Key.alt
    elif button.get() == "tab":
        k = keyboard.Key.tab
    elif button.get() == "enter":
        k = keyboard.Key.enter
    elif button.get() == "esc":
        k = keyboard.Key.esc
    elif button.get() == "space":
        k = keyboard.Key.space
    elif button.get() == "left":
        k = keyboard.Key.left
    elif button.get() == "right":
        k = keyboard.Key.right
    elif button.get() == "up":
        k = keyboard.Key.up
    elif button.get() == "down":
        k = keyboard.Key.down
    elif button.get() == "caps":
        k = keyboard.Key.caps_lock
    elif button.get() == "back":
        k = keyboard.Key.backspace
    elif button.get() == "delete":
        k = keyboard.Key.delete
    else:
        k = button.get()
    global press
    press = True

def stop_keyboard_click():
    global press
    press = False

# Show Helper Message
def show_help():
    messagebox.showinfo("Special Keys", "To press special keys type the following words.\n" +
            "Shift: 'shift'\tSpace: 'space'\n" +
            "Capslock: 'caps'\tControl: 'ctrl'\n" +
            "Alt: 'alt'\t\tTab: 'tab'\n" +
            "Right: 'right'\tLeft: 'left'\n" +
            "Up: 'up'\t\tDown: 'down'\n" +
            "Escape: 'esc'\tDelete: 'delete'\n" +
            "Backspace: 'back'\tEnter: 'enter'")

# Program Entry
if __name__ == "__main__":
    root = init()
    window = get_menu_frame(root)

    global speed
    speed = tk.IntVar()
    global press
    press = False
    global click
    click = False


    while True:
        root.update()
        count = 0
       
        if click:
            m.click(b)
            time.sleep(speed.get()/1000)
        elif press:
            kb.press(k)
            kb.release(k)
            time.sleep(speed.get()/1000)
