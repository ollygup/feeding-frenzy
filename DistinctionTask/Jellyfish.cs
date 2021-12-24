using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask{

    /// <summary>
    /// Has 2 enum values, Left and Right (Allow Jellyfish to redirect)
    /// </summary>
    public enum Direction{
        Left,
        Right
    }

    public class Jellyfish : Creature {
        private Direction _direction;

        /// <summary>
        /// A default constructor to build a Jellyfish object
        /// </summary>
        /// <returns></returns>
        public Jellyfish():this(SplashKit.Rnd(-180,900),SplashKit.Rnd(-180,700), 500, 20, SplashKit.LoadBitmap("jellyfish", "jellyfish.png")){
            _direction = Direction.Left;
        }

        /// <summary>
        /// A pass by value constructor to build a Jellyfish object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="hp"></param>
        /// <param name="atk"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public Jellyfish(double x, double y, int hp, int atk, Bitmap image):base(x, y, hp, atk, image){
            _direction = Direction.Left;
        }

        /// <summary>
        /// Property for Direction (enum)
        /// </summary>
        /// <value></value>
        public Direction Direction{
            get { return _direction; }
            set { _direction = value; }
        }
        
        /// <summary>
        /// overrides the virtual function from the parent class, 
        /// used to detect all possible collisions against the creatures in user's ControlledCreatureList,
        /// deals damage to specific target if collided, as well as taking damage
        /// creatures from user's ControlledCreatureList that died will be removed
        /// if jellyfish died, return jellyfish object, else return null
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oceanMap"></param>
        /// <returns></returns>
        public override Creature Target(User user, Ocean oceanMap)
        {
            Jellyfish j = null;
            List<Creature> tmplist = new List<Creature>();
            foreach (Creature c in user.ControlledCreatureList){
                Boolean collision = SplashKit.BitmapPointCollision(this.Image, this.CreatureX, this.CreatureY, c.CreatureX, c.CreatureY);
                if (collision == true){
                    c.CreatureHealth -= this.CreatureAttack; 
                    this.CreatureHealth -= c.CreatureAttack;
                    collision = false; 
                    if (c == c.Target(user, oceanMap)){
                        tmplist.Add(c);
                    }
                }
            }
            if (this.CreatureHealth == 0 || this.CreatureHealth <= 0){
                user.Score +=50;  
                j = this;
            }
            foreach (Creature x in tmplist){
                user.RemoveCreature(x);
            }
            return j;
        }
        
        /// <summary>
        /// Jellyfish will move to the left, if it hits/near the left screen border, change direction to right and vise versa
        /// </summary>
        public void MoveJellyfish(){
            if ((this.CreatureX != -200 || this.CreatureX > -195) && this.Direction == Direction.Left ){
                this.CreatureX -= 1;
            }else if ( (this.CreatureX == -200 || this.CreatureX <-195) && this.Direction == Direction.Left){
                this.Direction = Direction.Right;
                this.CreatureX += 1;
            }else if ((this.CreatureX != 1000 || this.CreatureX < 995) && this.Direction == Direction.Right){
                this.CreatureX += 1;
            }else if ((this.CreatureX == 1000 || this.CreatureX > 995) && this.Direction == Direction.Right){
                this.Direction = Direction.Left;
                this.CreatureX -= 1;
            }
        }

        /// <summary>
        /// Draws a rectangle representing the Jellyfish's Health
        /// </summary>
        /// <param name="window"></param>
        public override void DisplayCreatureHealth(Window window){  
            SplashKit.DrawRectangleOnWindow(window, Color.Yellow, this.CreatureX-16, this.CreatureY+99, 102, 12);
            SplashKit.FillRectangle(Color.Red, this.CreatureX-15, this.CreatureY+100, this.CreatureHealth / 5, 10);
        }
    }
}