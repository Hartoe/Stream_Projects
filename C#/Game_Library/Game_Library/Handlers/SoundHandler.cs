using Game_Library.Exceptions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Handlers
{
    public class SoundHandler
    {
        public Song Current_Song;
        public bool Paused { get; private set; }
        public bool Muted { get; private set; }

        private Dictionary<string, SoundEffect> sounds;
        private Dictionary<string, Song> songs;
        private float master_volume, sound_volume, song_volume;

        public SoundHandler()
        {
            sounds = new Dictionary<string, SoundEffect>();
            songs = new Dictionary<string, Song>();
            master_volume = sound_volume = song_volume = 1f;
            MediaPlayer.Volume = song_volume * master_volume;
            Paused = false;
            Muted = false;
        }

        public void Add_Sound(string name, SoundEffect sound)
        {
            if (sounds.ContainsKey(name))
                sounds[name] = sound;
            else
                sounds.Add(name, sound);
        }
        public void Add_Song(string name, Song song)
        {
            if (songs.ContainsKey(name))
                songs[name] = song;
            else
                songs.Add(name, song);
        }

        public void Remove_Sound(string name)
        {
            sounds.Remove(name);
        }
        public void Remove_Song(string name)
        {
            songs.Remove(name);
        }

        public SoundEffect Get_Sound(string name)
        {
            if (sounds.ContainsKey(name))
                return sounds[name];
            else
                throw new SoundExceptions(name);
        }
        public Song Get_Song(string name)
        {
            if (songs.ContainsKey(name))
                return songs[name];
            else
                throw new SoundExceptions(name);
        }

        public void Play_Sound(string name)
        {
            if (!Muted)
                sounds[name].Play(sound_volume * master_volume, 0.0f, 0.0f);
        }

        public void Play_Song(string name)
        {
            MediaPlayer.Play(songs[name]);
            Current_Song = songs[name];
        }

        public void Stop_Song()
        {
            MediaPlayer.Stop();
            Current_Song = null;
        }
        public void Stop_Song(string name)
        {
            if (Current_Song == songs[name])
                MediaPlayer.Stop();
        }

        public void Pause()
        {
            Paused = true;
            MediaPlayer.Pause();
        }
        public void Unpause()
        {
            Paused = false;
            MediaPlayer.Resume();
        }

        public void Set_Master_Volue(float vol)
        {
            master_volume = vol;
            MediaPlayer.Volume = song_volume * master_volume;
        }
        public void Set_Song_Volume(float vol)
        {
            song_volume = vol;
            MediaPlayer.Volume = song_volume * master_volume;
        }
        public void Set_Sound_Volume(float vol)
        {
            sound_volume = vol;
        }

        public float Get_Master_Volume() => master_volume;
        public float Get_Song_Volume() => song_volume;
        public float Get_Sound_Volume() => sound_volume;

        public void Mute()
        {
            Muted = true;
            MediaPlayer.Volume = 0;
        }
        public void Unmute()
        {
            Muted = false;
            MediaPlayer.Volume = song_volume * master_volume;
        }
    }
}
