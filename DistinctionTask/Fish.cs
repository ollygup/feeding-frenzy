using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask{
    public class Fish : Creature {
        /// <summary>
        /// A default constructor to build a fish object
        /// </summary>
        /// <returns></returns>
        public Fish():this(SplashKit.Rnd(-180,900),SplashKit.Rnd(-180,700), 100, 10, SplashKit.LoadBitmap("fish", "fish.png")){

        }

        /// <summary>
        /// A pass by value constructor to build a fish object at a specific location 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="hp"></param>
        /// <param name="atk"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public Fish(double x, double y, int hp, int atk, Bitmap image):base(x, y, hp, atk, image){
        
        }

    }
}