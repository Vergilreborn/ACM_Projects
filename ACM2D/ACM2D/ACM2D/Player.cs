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
    class Player : WeaponsPerks
    {
        //Information for drawing and the sprite itself
        private int width;
        private int height;
        private bool shipToggled;
      
        private float speed;
        private float rotationAngle;
        private Vector2 center;
        private Texture2D boxAura;
        private Texture2D ship;
        private Viewport screen;
        private MapReader map;
        public Rectangle destRectBox;
        public Vector2 destRectShip;
        public Rectangle sourceRectBox;
        public Rectangle sourceRectShip;

        String debug = "";
        
        //hard coded
        int maxFrames = 5;
        int currentFrame = 0;
        float animateTimer = 0f;
        public Keys up, down, left, right, toggle;
        int playerID;        


        //Player constructor
        //Takes in the Textures for the Box, the ship, the player's starting position,
        //the width, heigh, speed and the player's ID
        public Player(Texture2D boxAura,Texture2D ship, Vector2 position,Rectangle boxRect,
                        Rectangle shipRect, float speed, int playerID, ContentManager content, Viewport gameView){
           
            //Use of the viewport is allowed
            this.screen = gameView;
            //Ship information being setup
            this.ship = ship;
            sourceRectShip = shipRect;
            destRectShip = new Vector2(position.X , position.Y);

            //not debugging as soon as the game starts
         
            //center position of the ship
            //" / 2 " gets the center
            center = new Vector2((int)(shipRect.Width / 2), (int)(shipRect.Height / 2));  
           

            //This boxAura's information being setup
            this.boxAura = boxAura;
            this.width = boxRect.Width;
            this.height = boxRect.Height;
            this.speed = speed;
            destRectBox = new Rectangle((int)position.X  , (int)position.Y , width, height);

            //Initiate the weapons
            init(content);

            //The player's ID
            this.playerID = playerID;

            //The player ID and KeyBoard setup (For 1 computer currently)
            if (this.playerID == 0)
                setKeys(Keys.Up, Keys.Down, Keys.Left, Keys.Right,Keys.End);
            else
                setKeys(Keys.W, Keys.S, Keys.A, Keys.D,Keys.Q);
            
        }

        //Method - setKeys
        //Sets up the keys according to the current player's ID
        //Will be optional to change them later in the game
        public void setKeys(Keys up, Keys down, Keys left, Keys right, Keys toggle){
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
            this.toggle = toggle;
            
        }

        //Method - update
        //Simple keyStrokes and updation the animation depending on if the ship is being controlled
        //or if the boxAura is being controlled
        //Box Aura will be controlled by mouse in later development ---IDEA---
        public void update(GameTime time, KeyboardState currentKeyBoard, KeyboardState previous){
            
         

                //If any bullets where shot update them
                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].updateMovement();
                    if (bulletOffScreen(bullets[i].position))
                        bullets.RemoveAt(i);
                }

                //Changes the toggled object
                if (currentKeyBoard.IsKeyDown(toggle) && previous.IsKeyUp(toggle))
                    shipToggled = !shipToggled;

                //Depending on what is toggled, movement occurs accordingly
                if (!shipToggled)
                {
                    if (currentKeyBoard.IsKeyDown(right))
                        destRectBox.X += (int)speed;
                    if (currentKeyBoard.IsKeyDown(left))
                        destRectBox.X -= (int)speed;
                    if (currentKeyBoard.IsKeyDown(up))
                        destRectBox.Y -= (int)speed;
                    if (currentKeyBoard.IsKeyDown(down))
                        destRectBox.Y += (int)speed;

                    //Otherwise the player has control of the ship and must draw accordingly
                }
                else
                {
                    debug = "X Speed: " + ((Math.Sin(rotationAngle)) * speed) + "\nY Speed: " + ((Math.Cos(rotationAngle)) * speed);
                    if (currentKeyBoard.IsKeyDown(right))
                        rotationAngle += .07f;
                    if (currentKeyBoard.IsKeyDown(left))
                        rotationAngle -= .07f;
                    if (currentKeyBoard.IsKeyDown(up))
                    {
                        destRectShip.X += (float)(Math.Sin(rotationAngle) * speed);
                        destRectShip.Y += (float)-(Math.Cos(rotationAngle) * speed);

                    }
                    //Fire at nothing
                    if (currentKeyBoard.IsKeyDown(down) && previous.IsKeyUp(down))
                        if (maxBullets > bullets.Count)
                        {
                            Vector2 bulletPos = new Vector2(destRectShip.X, destRectShip.Y);
                            bullets.Add(new BulletPaths(bulletPos, rotationAngle));
                            debug += "\nSHOOT!";
                        }

                    rotationAngle = rotationAngle % (MathHelper.Pi * 2);
                }
            

         

            //Animates the box
            animateSprite(time);

        }

        //Method - animateSprite
        //Animate the sprite according to number of frames
        //as well as through a timer
        private void animateSprite(GameTime time){
            animateTimer += time.ElapsedGameTime.Milliseconds;

            if (animateTimer > 50f){
                currentFrame++;
                if (currentFrame > maxFrames)
                    currentFrame = 0;
                animateTimer = 0f;
                sourceRectBox = new Rectangle(currentFrame * width, 0, width, height);
            }
            
        }

        //Method - Draw
        //Draws ship on the screen with according data
        public void DrawShip(SpriteBatch spriteBatch){
        
           
            foreach (BulletPaths bullet in bullets)
            {
                //spriteBatch.Draw(unlocked[current], bullet.position,  Color.White);
                spriteBatch.Draw(unlocked[current], bullet.position, null, Color.White, bullet.angle, center, 1.0f, SpriteEffects.None, 1.0f);
            }

            spriteBatch.Draw(ship, destRectShip, sourceRectShip, Color.White, rotationAngle, center, 1.0f, SpriteEffects.None, 1.0f);


        }

        //Checks to see if the bullet is on the screen.
        private bool bulletOffScreen(Vector2 bulletPosition){

            //Sees if the bullet is inbetween the screens position, and height
            if (bulletPosition.X > screen.X && bulletPosition.X < screen.X + screen.Width
                   && bulletPosition.Y > screen.Y && bulletPosition.Y < screen.Y + screen.Height)
                return false;            
            return true;
            
        }

        //Method - Draw
        //Draws boxAura on the scrren with according data
        public void DrawAura(SpriteBatch spriteBatch, SpriteFont font){
            spriteBatch.Draw(boxAura, destRectBox, sourceRectBox, Color.White);

            //40 represents the offset to draw the sprites data information
            spriteBatch.DrawString(font, debug, new Vector2(destRectShip.X + 40, destRectShip.Y), Color.White);

            //draws the terminal for entering data information

          

        }

        //Returns the center of this sprite
        public Vector2 getCenter(){
            return destRectShip;
        }

        public void connect(MapReader map){
            this.map = map;

        }

    }
}
