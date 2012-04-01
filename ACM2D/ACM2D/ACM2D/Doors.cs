using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Media;

namespace ACM2D
{
    class Doors
    {

        Vector2 spriteNumber;
        Vector2 position;
        public Rectangle destRect, sourceRect;
        Color colorCode;

        public Doors(String [] data, Color color){
            init(data);
            colorCode = color;
        }


         public void init(String [] data){
        
            spriteNumber = new Vector2(int.Parse(data[0]), int.Parse(data[1]));
            position = new Vector2(2 * int.Parse(data[2]), 2 * int.Parse(data[3]));
            destRect = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            sourceRect= new Rectangle((int)spriteNumber.X * 32, (int)spriteNumber.Y * 32, 32, 32);
        }

         public Color getColor(){
             return colorCode;
         }

    }
}
