using System;
using System.Collections.Generic;
using SplashKitSDK;
using System.Timers;


namespace DistinctionTask{

    public abstract class Creature{
        private double _creatureX;
        private double _creatureY;
        private int _creatureHealth;
        private int _creatureAttack;
        private int _creatureSpeed;
        private Bitmap _image;

        /// <summary>
        /// A pass by value constructor that will help to fill all these necessary values for the child classes
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="hp"></param>
        /// <param name="atk"></param>
        /// <param name="image"></param>
        public Creature(double x, double y, int hp, int atk, Bitmap image){
            _creatureX = x;
            _creatureY = y;
            _creatureHealth = hp;
            _creatureAttack = atk;
            _creatureSpeed = 2;
            _image = image;
        }

        /// <summary>
        /// Property for Creature's X coordinate
        /// </summary>
        /// <value></value>
        public double CreatureX{
            get { return _creatureX; }
            set { _creatureX = value; }
        }

        /// <summary>
        /// Property for Creature's Y coordinate
        /// </summary>
        /// <value></value>
        public double CreatureY{
            get { return _creatureY; }
            set { _creatureY = value; }
        }

        /// <summary>
        /// Property for Creature's Health
        /// </summary>
        /// <value></value>
        public int CreatureHealth{
            get { return _creatureHealth; }
            set { _creatureHealth = value; }
        }

        /// <summary>
        /// Property for Creature's Attack
        /// </summary>
        /// <value></value>
        public int CreatureAttack{
            get { return _creatureAttack; }
            set { _creatureAttack = value; }
        }

        /// <summary>
        /// Property for Creature's Speed
        /// </summary>
        /// <value></value>
        public int CreatureSpeed{
            get { return _creatureSpeed; }
            set { _creatureSpeed = value; }
        }

        /// <summary>
        /// An image of the creature
        /// </summary>
        /// <value></value>
        public Bitmap Image{
            get { return _image; }
            set { _image = value; }
        }
        
        /// <summary>
        /// Checks the collision between all the creatures under user's ControlledCreatureList and 
        /// the (creature) that calls this function, provided it is type of fish, squid or crab,
        /// return that (creature) if true, else return null
        /// </summary>
        /// <param name="oceanMap"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Creature CaptureCreatures(User user){
            Boolean collision;
            foreach(Creature c in user.ControlledCreatureList){
                if (this.GetType() == typeof(Fish) || this.GetType() == typeof(Squid) || this.GetType() == typeof(Crab)){
                    collision = SplashKit.BitmapPointCollision(c.Image, c.CreatureX, c.CreatureY, this.CreatureX, this.CreatureY);
                    if (collision == true){
                        return this;
                    }
                    collision = false;
                } 
            }
            return null; 
        }

        /// <summary>
        /// A virtual function that detects if the creature is dead, returns creature or null value
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oceanMap"></param>
        /// <returns></returns>
        public virtual Creature Target(User user, Ocean oceanMap){
            if (this.CreatureHealth == 0 || this.CreatureHealth <= 0){
                return this;
            }
            return null;
        }
        
        /// <summary>
        /// A virtual function that display creature's Health in a rectangle form
        /// </summary>
        /// <param name="window"></param>
        public virtual void DisplayCreatureHealth(Window window){
            SplashKit.DrawRectangleOnWindow(window, Color.Yellow, this.CreatureX-16, this.CreatureY+31, 30, 12);
            SplashKit.FillRectangle(Color.Red, this.CreatureX-15, this.CreatureY+30, this.CreatureHealth / 5, 10);
        }

    }
}