using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask{
    public class Crab : Creature {
        /// <summary>
        /// A default constructor that creates a Crab object
        /// </summary>
        /// <returns></returns>
        public Crab():this(SplashKit.Rnd(-180,900),SplashKit.Rnd(-180,700), 100, 50, SplashKit.LoadBitmap("crab", "crab.png")){

        }

        /// <summary>
        /// A pass by value constructor to specify a location when creating a Crab object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="hp"></param>
        /// <param name="atk"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public Crab(double x, double y, int hp, int atk, Bitmap image):base(x, y, hp, atk, image){
            
        }
    }
}