import os
from pytube import YouTube, Playlist
import tkinter as tk
from tkinter import ttk, messagebox

# Create the window space.
def init_window():
    window = tk.Tk()
    window.title("Youtube Converter")
    window.resizable(False, False)
    window.iconbitmap('../images/icon.ico')
    return window

# Creates a new Radio Button
def create_radio_button(frame, txt, val, var):
    return tk.Radiobutton(frame, text=txt, value=val, variable=var)

# Creates a new Entry field
def create_text_field(frame, var, w):
    return tk.Entry(frame, textvariable=var, width=w)

# Creates a new Label
def create_label(frame, txt, var=None):
    return tk.Label(frame, text=txt, textvariable=var)

# Creates a new Combo Box
def create_combo_box(frame, w, txt, var):
    return ttk.Combobox(frame, width=w, text=txt, textvariable=var)

# Creates a new Button
def create_button(frame, txt, f, b, cmd):
    return tk.Button(frame, text=txt, fg=f, bg=b, command=cmd)

# Event handler when download button is clicked
def on_button_click():
    # Check if there is a URL to download
    if url.get() == "":
        messagebox.showinfo("Field Empty", "The URL field is currently empty. You must enter a URL to a youtube video or playlist to use the converter.")
    else:
        if url_type.get() == 1:
            handle_video()
        elif url_type.get() == 2:
            handle_playlist()
        else:
            raise Exception("Something went wrong with the URL type!")

# Handles a Video URL
def handle_video():
    video = YouTube(url.get())
    stream, name = get_stream(video)
    download_stream(video, stream, name)
    download_text.set("Download Complete!")

# Handles a Playlist URL
def handle_playlist():
    playlist = Playlist(url.get())
    for video in playlist.videos:
        stream, name = get_stream(video)
        download_stream(video, stream, name)
    download_text.set("Download Complete!")

# Gets the wanted stream based on the selected file type
def get_stream(video):
    if file_type.get() == 'MP4':
        return video.streams.filter(progressive=True, subtype="mp4").first(), 'MP4'
    elif file_type.get() == 'MP3':
        return video.streams.filter(only_audio=True, subtype="mp4").first(), 'MP3'
    else:
        return video.streams.filter(only_video=True, subtype="mp4").first(), 'No Audio'

# Downloads the given stream of a video
def download_stream(video, stream, name):
    download_text.set(f"Downloading {video.title}...")
    stream.download(directory.get(), filename=stream.default_filename + name)
    download_text.set(f"Downloaded {video.title}!")

# Initial run
if __name__ == "__main__":
    # Init window
    root = init_window()
    
    # Create and set the welcoming text
    welcome_label = create_label(root, "Welcome!\nPlease paste in a YouTube URL! The URL can be of a video or a playlist,\nas long as you select the correct type down below. Choose a file type\nand enter a download directory(current directory is the default).\nPress download to start the downloading!")
    welcome_label.grid(columnspan=3, row=0)

    # Add a separator for beauty <3
    sep_one = ttk.Separator(root, orient='horizontal')
    sep_one.grid(columnspan=3, row=1, sticky="ew", pady=5)
    
    # Create the Radio Buttons and link them to a variable
    url_type = tk.IntVar(root, value=1)
    vid_button = create_radio_button(root, "Video", 1, url_type)
    list_button = create_radio_button(root, "Playlist", 2, url_type)
    vid_button.grid(column=0, row=2)
    list_button.grid(column=0, row=3)

    # Create the file type combo box and text
    file_label = create_label(root, "File Type: ")
    file_label.grid(column=1, row=2, rowspan=2)
    file_type = tk.StringVar(root, value='MP4')
    file_combobox = create_combo_box(root, 15, "Choose file type", file_type)
    file_combobox['values'] = ('MP4',
            'MP3',
            'MP4 (No Audio)')
    file_combobox.grid(column=2, row=2, rowspan=2)
    file_combobox.current()

    # Create the URL entry field and text
    url_label = create_label(root, "YouTube URL: ")
    url_label.grid(column=0, row=4)
    url = tk.StringVar(root)
    url_field = create_text_field(root, url, 60)
    url_field.grid(column=1, columnspan=2, row=4)

    # Create the Dir entry field and text
    dir_label = create_label(root, "Directory: ")
    dir_label.grid(column=0, row=5)
    directory = tk.StringVar(root, value=os.getcwd())
    dir_field = create_text_field(root, directory, 60)
    dir_field.grid(column=1, columnspan=2, row=5)

    # Second separator for even more beauty <3 <3
    sep_two = ttk.Separator(root, orient='horizontal')
    sep_two.grid(columnspan=3, row=6, sticky="ew", pady=5)

    # Create Download text and button
    download_text = tk.StringVar(root, value="")
    download_label = create_label(root, "", download_text)
    download_label.grid(columnspan=3, row=7)
    download = create_button(root, "Download", "white", "deep sky blue", on_button_click)
    download.grid(columnspan=3, row=8, ipadx=20, pady=10)
    
    # Add credits text
    credit_label = create_label(root, "Made by HartoeHajek, 22/01/2021")
    credit_label.grid(column=0, row=9)

    # Run the main loop
    root.mainloop()
