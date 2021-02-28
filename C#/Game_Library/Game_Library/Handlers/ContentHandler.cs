using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Handlers
{
    public class ContentHandler
    {
        private ContentManager Content;

        public ContentHandler(ContentManager Content)
        {
            this.Content = Content;
        }

        public Texture2D Load_Sprite(string path)
        {
            try
            {
                return Content.Load<Texture2D>(path);
            }
            catch
            {
                throw new ContentLoadException($"Texture2D at {path} was not found!");
            }
        }

        public SpriteFont Load_Font(string path)
        {
            try
            {
                return Content.Load<SpriteFont>(path);
            }
            catch
            {
                throw new ContentLoadException($"Font at {path} was not found!");
            }
        }

        public SoundEffect Load_Sound(string path)
        {
            try
            {
                return Content.Load<SoundEffect>(path);
            }
            catch
            {
                throw new ContentLoadException($"Sound at {path} was not found!");
            }
        }

        public Song Load_Song(string path)
        {
            try
            {
                return Content.Load<Song>(path);
            }
            catch
            {
                throw new ContentLoadException($"Song at {path} was not found!");
            }
        }

        public Effect Load_Shader(string path)
        {
            try
            {
                return Content.Load<Effect>(path);
            }
            catch
            {
                throw new ContentLoadException($"Shader at {path} was not found!");
            }
        }
    }
}
