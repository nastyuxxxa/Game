using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    internal class Atack : Entity
    {
        public Vector2 position;
        public float atackSpeed = 4;
        public Atack(Texture2D spriteAtack)
        {

        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
