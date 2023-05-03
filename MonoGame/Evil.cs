using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    internal class Evil : Entity
    {
        public Vector2 moving;
        public float evilSpeed = 2F;

        public Evil(Texture2D evilSprite)
        {
            spriteList = evilSprite;
            moving = new Vector2();

        }
        public override void Update()
        {
            //velocity.X += evilSpeed;
            //if (velocity.X > Window.ClientBounds.Width - evilTexture.Width || velocity.X < 0)
            //    evilSpeed *= -1;
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(spriteList, velocity, Color.White);
            throw new NotImplementedException();
        }
    }
}
