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
using Microsoft.Xna.Framework.Media;

namespace ACM2D
{
    class Player
    {

        //Information for drawing and the sprite itself
        private int width;
        private int height;
        private float speed;
        public Texture2D manSheet;
        public Rectangle destRect;
        public Rectangle sourceRect;


        //hard coded
        int maxFrames = 5;
        int currentFrame = 0;
        float animateTimer = 0f;
        public Keys up, down, left, right, slowDown;
        int playerID;        

        public Player(Texture2D manSheet, Vector2 position,int width, int height, float speed, int playerID){
            this.manSheet = manSheet;
            this.width = width;
            this.height = height;
            this.speed = speed;
            destRect = new Rectangle((int)position.X,(int)position.Y, width, height);
            this.playerID = playerID;

            if (this.playerID == 0)
                setKeys(Keys.Up, Keys.Down, Keys.Left, Keys.Right,Keys.End);
            else
                setKeys(Keys.W, Keys.S, Keys.A, Keys.D,Keys.Q);
            
        }

        public void setKeys(Keys up, Keys down, Keys left, Keys right, Keys slowDown){
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
            this.slowDown = slowDown;
        }

        //Simple keyStrokes and updation the animation
        public void update(GameTime time, KeyboardState currentKeyBoard){
            if (currentKeyBoard.IsKeyDown(right))
                destRect.X += (int)speed;
            if (currentKeyBoard.IsKeyDown(left))
                destRect.X -= (int)speed;
            if (currentKeyBoard.IsKeyDown(up))
                destRect.Y -= (int)speed;
            if (currentKeyBoard.IsKeyDown(down))
                destRect.Y += (int)speed;

            animateSprite(time);

        }

        //Animate the sprite according to number of frames
        //as well as through a timer
        public void animateSprite(GameTime time){
            animateTimer += time.ElapsedGameTime.Milliseconds;

            if (animateTimer > 50f){
                currentFrame++;
                if (currentFrame > maxFrames)
                    currentFrame = 0;
                animateTimer = 0f;
                sourceRect = new Rectangle(currentFrame * width, 0, width, height);
            }
            
        }

    }
}
