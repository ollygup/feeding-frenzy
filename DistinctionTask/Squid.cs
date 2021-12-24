using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask{
    public class Squid : Creature {
        
        /// <summary>
        /// A default constructor that creates a Squid object
        /// </summary>
        /// <returns></returns>
        public Squid():this(SplashKit.Rnd(-180,900),SplashKit.Rnd(-180,700), 150, 10, SplashKit.LoadBitmap("squid", "squid.png")){

        }

        /// <summary>
        /// A pass by value constructor that is used to create a Squid object at a dedicated location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="hp"></param>
        /// <param name="atk"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public Squid(double x, double y, int hp, int atk, Bitmap image):base(x, y, hp, atk, image){

        }
        

    }
}