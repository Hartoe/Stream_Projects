using Game_Library.Exceptions;
using Game_Library.Helpers;
using Game_Library.Structures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Library.Handlers
{
    public class GraphicsHandler
    {
        public SpriteBatch Background { get; private set; }
        public SpriteBatch Foreground { get; private set; }
        public SpriteBatch GUI { get; private set; }

        Dictionary<string, ShaderGroup> shader_groups;
        GraphicsDevice graphicsDevice;

        public GraphicsHandler(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            shader_groups = new Dictionary<string, ShaderGroup>();

            Background = new SpriteBatch(graphicsDevice);
            Foreground = new SpriteBatch(graphicsDevice);
            GUI = new SpriteBatch(graphicsDevice);
        }

        public void Add_Group(string name, ShaderGroup group)
        {
            if (!shader_groups.ContainsKey(name))
                shader_groups.Add(name, group);
            else
                throw new DuplicateShaderGroupException(name);
        }

        public void Remove_Group(string name)
        {
            if (shader_groups.ContainsKey(name))
                shader_groups.Remove(name);
            else
                throw new ShaderGroupNotFoundException(name);
        }

        public ShaderGroup Get_Group(string name)
        {
            if (shader_groups.ContainsKey(name))
                return shader_groups[name];
            else
                throw new ShaderGroupNotFoundException(name);
        }

        public void Draw(Camera camera)
        {
            foreach (KeyValuePair<string, ShaderGroup> item in shader_groups)
            {
                Draw_Group(item.Value, camera);
            }
        }

        public void Begin(Camera camera, Effect effect = null, bool do_cam = true)
        {
            if (do_cam)
            {
                Background.Begin(transformMatrix: camera.Get_Background_Transform(), effect: effect);
                Foreground.Begin(transformMatrix: camera.Get_Transform(), effect: effect);
                GUI.Begin(effect: effect);
            }
            else
            {
                Background.Begin(effect: effect);
                Foreground.Begin(effect: effect);
                GUI.Begin(effect: effect);
            }
        }

        public void End()
        {
            Background.End();
            Foreground.End();
            GUI.End();
        }

        public void Draw_Group(ShaderGroup group, Camera camera)
        {
            group.Create_Render_Target(graphicsDevice);
            graphicsDevice.SetRenderTarget(group.target);

            Begin(camera, group.shader);
            foreach (KeyValuePair<string, RenderObject> spr in group.sprites)
            {
                if (spr.Value is Sprite)
                    Draw_Sprite(group.end_target, (Sprite) spr.Value);
                if (spr.Value is Text)
                    Draw_Text(group.end_target, (Text) spr.Value);
            }
            End();

            graphicsDevice.SetRenderTarget(null);

            Begin(camera, do_cam: false);
            group.end_target.Draw(group.target, Vector2.Zero, Color.White);
            End();

            group.target.Dispose();
        }

        public void Draw_Sprite(SpriteBatch layer, Sprite sprite)
        {
            layer.Draw(sprite.sprite, sprite.position, sprite.source, sprite.color, sprite.rotation, sprite.origin, sprite.scale, sprite.effect, sprite.layerDepth);
        }
        public void Draw_Sprite(ShaderGroup group, string id, Sprite sprite)
        {
            group.Bind(id, sprite);
        }

        public void Draw_Text(SpriteBatch layer, Text text)
        {
            layer.DrawString(text.font, text.text, text.position, text.color, text.rotation, text.origin, text.scale, text.effect, text.layerDepth);
        }
        public void Draw_Text(ShaderGroup group, string id, Text text)
        {
            group.Bind(id, text);
        }

        public void Draw_Animation(ShaderGroup group, string id, Animation animation)
        {
            group.Bind(id, animation.sprite);
        }
    }
}
