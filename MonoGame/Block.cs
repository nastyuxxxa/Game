using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    internal class Block
    {
        public Texture2D textureBlock;

        public Rectangle rectangle;

        public Block(Texture2D textureBlock, Rectangle rectangle)
        {
            this.textureBlock = textureBlock;
            this.rectangle = rectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureBlock, rectangle, Color.White);
        }
    }
}
