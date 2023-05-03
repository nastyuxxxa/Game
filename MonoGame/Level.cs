using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    internal class Level
    {
        public static void CreateMaps(List<Block> blocks, int numberLevel, Texture2D blockTexture1, Texture2D blockTexture2)
        {
            int[,] map;

            if (numberLevel == 1)
            {
                map = new int[,] {{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                                  {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                                  {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                                  {0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,0,0,2},
                                  {0,0,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,2},
                                  {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                                  {0,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,0,2},
                                  {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                                  {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,2},
                                  {0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,2},
                                  {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};
                DrawLevel(map, blocks, blockTexture1, blockTexture2);

            }
            else if (numberLevel == 2)
            {
                map = new int[,] {{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                                  {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                  {0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0},
                                  {0,0,0,0,0,0,1,0,0,0,0,2,2,2,0,0,0,0,0,0},
                                  {0,0,0,0,0,1,1,0,0,0,2,2,2,2,2,0,0,0,0,0},
                                  {0,0,0,0,1,1,1,0,0,0,0,2,2,2,0,0,0,0,0,0},
                                  {0,0,0,1,1,1,1,0,0,0,0,0,2,0,0,0,0,0,0,0},
                                  {0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                  {0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                  {1,1,1,1,1,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1},
                                  {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}};
                DrawLevel(map, blocks, blockTexture1, blockTexture2);
            }
        }

        static void DrawLevel(int[,] map, List<Block> blocks, Texture2D blockTexture1, Texture2D blockTexture2)
        {
            var x = 0;
            var y = 0;

            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    var rect = new Rectangle(x, y, 40, 40);
                    var a = map[i, j];

                    if (a == 1)
                    {
                        var block = new Block(blockTexture1, rect);
                        blocks.Add(block);
                    }
                    else if (a == 2)
                    {
                        var block = new Block(blockTexture2, rect);
                        blocks.Add(block);
                    }
                    x += 30;
                }
                x = 0;
                y += 30;
            }
        }
    }
}
