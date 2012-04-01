using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ACM2D
{
    class Camera
    {

        public Viewport view;
        public Vector2 position;
        public Vector2 savedPosition;
        public Vector2 focusPoint;
        public float positionShakeAmount;
        public float maxShakeTime;
        public float zoom;
        public Matrix transform;
        public Vector2 source;
        TimeSpan shaketimer;
        Random random;

        public Camera(Viewport view, Vector2 position)
        {
            this.view = view;
            this.position = position;
            zoom = 1.0f;
            random = new Random();
            focusPoint = new Vector2(view.Width / 2, view.Height / 2);

        }

        public void Update(GameTime gametime)
        {
            if (shaketimer.TotalSeconds > 0)
            {
                focusPoint = savedPosition;
                /* We want to subtract the elapsed time with the shaketimer
                   * If it is still above 0. we will perform the camera shake
                   * Otherwise, we will be done for this loop and the next loop
                   * the saved data will be restored and it will go in the main 'else'
                   * block below
                   * */
                shaketimer = shaketimer.Subtract(gametime.ElapsedGameTime);
                if (shaketimer.TotalSeconds > 0)
                {
                    focusPoint += new Vector2((float)((random.NextDouble() * 2) - 1) * positionShakeAmount,
                        (float)((random.NextDouble() * 2) - 1) * positionShakeAmount);
                    
                }
            }
            /* Create a transform matrix through position, scale, rotation, and translation to the focus point
             * We use Math.Pow on the zoom to speed up or slow down the zoom.  Both X and Y will have the same zoom levels
             * so there will be no stretching.
             * */

            Vector2 objectPosition = source;

            transform = Matrix.CreateTranslation(new Vector3(-objectPosition, 0)) *
                Matrix.CreateTranslation(new Vector3(focusPoint.X, focusPoint.Y, 0));
        }

        public void Update(GameTime gametime, Vector2 source)
        {
            if (shaketimer.TotalSeconds > 0)
            {
                focusPoint = savedPosition;
                /* We want to subtract the elapsed time with the shaketimer
                   * If it is still above 0. we will perform the camera shake
                   * Otherwise, we will be done for this loop and the next loop
                   * the saved data will be restored and it will go in the main 'else'
                   * block below
                   * */
                shaketimer = shaketimer.Subtract(gametime.ElapsedGameTime);
                if (shaketimer.TotalSeconds > 0)
                {
                    focusPoint += new Vector2((float)((random.NextDouble() * 2) - 1) * positionShakeAmount,
                        (float)((random.NextDouble() * 2) - 1) * positionShakeAmount);
                    ;
                }
            }
            /* Create a transform matrix through position, scale, rotation, and translation to the focus point
             * We use Math.Pow on the zoom to speed up or slow down the zoom.  Both X and Y will have the same zoom levels
             * so there will be no stretching.
             * */

            Vector2 objectPosition = source;

            transform = Matrix.CreateTranslation(new Vector3(-objectPosition, 0)) *
                     Matrix.CreateTranslation(new Vector3(focusPoint.X, focusPoint.Y, 0));


        }


        public void Shake(float shakeTime, float positionAmount)
        {
            //We only want to perform one shake.  If one is going on currently, we have to
            //wait for that shake to be over before we can do another one.
            if (shaketimer.TotalSeconds <= 0)
            {
                maxShakeTime = shakeTime;
                shaketimer = TimeSpan.FromSeconds(maxShakeTime);
                positionShakeAmount = positionAmount;
                savedPosition = focusPoint;

            }
        }
        public void Follow(Player source)
        {
            this.source = source.getCenter();

        }
   
    }
}
