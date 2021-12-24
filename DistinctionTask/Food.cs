using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask{
    public class Food{
        private int _point;
        private int _foodX;
        private int _foodY;

        /// <summary>
        /// A pass by value constructor to build a Food object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Food(int x, int y){
            _point = 20;
            _foodX = x;
            _foodY = y;
        }
        
        /// <summary>
        /// Readonly property for Point
        /// </summary>
        /// <value></value>
        public int Point{
            get { return _point; }
        }

        /// <summary>
        /// Readonly property for Food's X Coordinate
        /// </summary>
        /// <value></value>
        public int FoodX{
            get{ return _foodX; }
        }

        /// <summary>
        /// Readonly property for Food's Y Coordinate
        /// </summary>
        /// <value></value>
        public int FoodY{
            get { return _foodY; }
        }
    }
}