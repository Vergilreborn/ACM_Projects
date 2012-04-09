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
        

        //The sprite location of the block
        Vector2 spriteNumber;
        Vector2 position;

        //The destination and the location rectangle
        public Rectangle destRect, sourceRect;
        Color colorCode;

        public Doors(String [] data, Color color){
            init(data);
            colorCode = color;
        }

         //Initiates the block from the data passed in.
         public void init(String [] data){
        
            //32 = width 
            //32 = height
            //2 = indicates the new position, since the tiles are two times the size
            //of the normal size of a tile
            spriteNumber = new Vector2(int.Parse(data[0]), int.Parse(data[1]));
            position = new Vector2(2 * int.Parse(data[2]), 2 * int.Parse(data[3]));
            destRect = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            sourceRect= new Rectangle((int)spriteNumber.X * 32, (int)spriteNumber.Y * 32, 32, 32);
        }

         //Returns the color of the door for debugging purposes
         public Color getColor(){
             return colorCode;
         }

    }
}
