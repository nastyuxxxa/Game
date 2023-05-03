using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    internal class Animation
    {
        Texture2D spriteList;
        int frames;
        int rows = 0;
        int counter = 0;
        float timeSienceLastFrame = 0;
        public Animation(Texture2D spriteList, float width = 32, float height = 32)
        {
            this.spriteList = spriteList;
            frames = (int)(spriteList.Width / width);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime, float milisecondsSperFrames = 500)
        {
            if (counter < frames)
            {
                var rectangle = new Rectangle(32 * counter, rows, 32, 32);
                spriteBatch.Draw(spriteList, position, rectangle, Color.White);
                timeSienceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timeSienceLastFrame > milisecondsSperFrames)
                {
                    timeSienceLastFrame -= milisecondsSperFrames;
                    counter++;
                    if (counter == frames) counter = 0;
                }
            }
        }
    }
}
