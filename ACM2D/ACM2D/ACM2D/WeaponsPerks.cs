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
    class WeaponsPerks
    {
        //The content manager and to load things when necessary
        ContentManager content;
        
        //information weapons
        public List<String> information = new List<String>();
        public List<Texture2D> unlocked = new List<Texture2D>();

        //Weapon choice along with weapons unlocked
        public int current = 0;
        public int weaponsUnlocked = 2;

        //List of Bullet Paths, seen below
        public List<BulletPaths> bullets= new List<BulletPaths>();
        public int maxBullets = 5;

        //Initiates the files to be loaded
        public void init(ContentManager content){
            this.content = content;

            //Hardcoding some weapons for the meantime
            information.Add("Sprites/Laser");
            information.Add("Sprites/Missile");

            //adding contents to Texture2D
            for (int i = 0; i < weaponsUnlocked; i++){
                unlocked.Add(content.Load<Texture2D>(information[i]));
            }
        }

        //Unlock the next weapon
        public void unlockNextWeapon(){
            weaponsUnlocked++;
            unlocked.Add(content.Load<Texture2D>(information[weaponsUnlocked]));
        }
    }

    //A class that has the trajectory path as well as the position
    class BulletPaths{

        public Vector2 position;
        public Vector2 trajectory;
        public float angle;
        private float speed = 10f;

        public BulletPaths(Vector2 position, float angle){
            this.angle = angle;
            this.position = position;
            trajectory = new Vector2((float)Math.Sin(angle) * speed, -(float)Math.Cos(angle) * speed);
        }

        public Vector2 updateMovement(){
            position = new Vector2(position.X + trajectory.X, position.Y + trajectory.Y);
            return position;
        }
        public void changeTrajectorySpeed(float speed){
            this.speed = speed;
            trajectory = new Vector2((float)Math.Sin(angle) * speed, -(float)Math.Cos(angle) * speed);
        }
        public void changeTrajectoryAngle(float angle){
            this.angle = angle;
            trajectory = new Vector2((float)Math.Sin(angle) * speed, -(float)Math.Cos(angle) * speed);
        }
    }
}
