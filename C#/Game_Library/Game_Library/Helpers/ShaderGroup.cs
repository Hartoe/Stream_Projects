using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Helpers
{
    public class ShaderGroup
    {
        public SpriteBatch end_target;
        public RenderTarget2D target;
        public Dictionary<string, Sprite> sprites;
        public Effect shader;

        public ShaderGroup(SpriteBatch end_target, Effect shader)
        {
            this.end_target = end_target;
            this.shader = shader;
            sprites = new Dictionary<string, Sprite>();
        }

        public void Create_Render_Target(GraphicsDevice graphicsDevice)
        {
            var pp = graphicsDevice.PresentationParameters;
            target = new RenderTarget2D(graphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, false, pp.BackBufferFormat, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.PreserveContents);
        }

        public void Bind(string id, Sprite sprite)
        {
            sprites.Add(id, sprite);
        }

        public void Unbind(string id)
        {
            sprites.Remove(id);
        }
    }
}
