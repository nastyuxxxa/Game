using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    internal abstract class Entity
    {
        public Texture2D spriteList;
        public Vector2 position;
        public Rectangle hitbox;
        public enum currentAnimation
        {
            Idle,
            Run
        }
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void Update();
    }
}
