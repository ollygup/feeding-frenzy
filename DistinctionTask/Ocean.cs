using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask{
    public class Ocean{
        private List<Creature> _creatureList;
        private List<Food> _foodList;
        private int _crabCount;
        private int _fishCount;
        private int _squidCount;
        private int _jellyfishCount;

        /// <summary>
        /// A constructor that creates an ocean 
        /// </summary>
        public Ocean(){
            _creatureList = new List<Creature>();
            _foodList = new List<Food>();
            _crabCount = 0;
            _fishCount = 0;
            _jellyfishCount = 0;
            _squidCount = 0;
        }
        
        /// <summary>
        /// Removes food object from the food list
        /// </summary>
        /// <param name="food"></param>
        public void RemoveFood(Food food){
           _foodList.Remove(food);
        }

        /// <summary>
        /// Adds food object into the food list
        /// </summary>
        /// <param name="food"></param>
        public void AddFood(Food food){
            _foodList.Add(food);
        }

        /// <summary>
        /// Adds Creature into the Ocean's CreatureList
        /// </summary>
        /// <param name="creature"></param>
        public void AddCreature(Creature creature){
            _creatureList.Add(creature);
        }

        /// <summary>
        /// Removes Creature from the Ocean's CreatureList
        /// </summary>
        /// <param name="creature"></param>
        public void RemoveCreature(Creature creature){
            _creatureList.Remove(creature);
        }
        
        /// <summary>
        /// A readonly property for Ocean's CreatureList
        /// </summary>
        /// <value></value>
        public List<Creature> CreatureList{
            get { return _creatureList; }
        }

        /// <summary>
        /// A readonly property for Ocean's FoodList
        /// </summary>
        /// <value></value>
        public List<Food> FoodList{
            get { return _foodList; }
        }
        
        /// <summary>
        /// Property for CrabCount in the ocean
        /// </summary>
        /// <value></value>
        public int CrabCount{
            get { return _crabCount; }
            set { _crabCount = value; }
        }

        /// <summary>
        /// Property for FishCount in the ocean
        /// </summary>
        /// <value></value>
        public int FishCount{
            get { return _fishCount; }
            set { _fishCount = value; }
        }

        /// <summary>
        /// Property for Jellyfish count in the ocean
        /// </summary>
        /// <value></value>
        public int JellyfishCount{
            get { return _jellyfishCount; }
            set { _jellyfishCount = value; }
        }

        /// <summary>
        /// Property for Squid count in the ocean
        /// </summary>
        /// <value></value>
        public int SquidCount{
            get { return _squidCount; }
            set { _squidCount = value; }
        }
        
        /// <summary>
        /// Creates food object with random location and add into the list
        /// </summary>
        public void PopulateFood(){

            int rndX = SplashKit.Rnd(-200,980);
            int rndY = SplashKit.Rnd(-200,780);

            Food e = new Food(rndX, rndY);
            this.AddFood(e);
        }
        
        /// <summary>
        /// Draws the food objects from the food list onto the ocean map
        /// </summary>
        public void DrawFood(){
            Sprite food = SplashKit.CreateSprite("food.png");
            foreach (Food f in _foodList){
                SplashKit.DrawSprite(food,f.FoodX, f.FoodY);
            }
        }

        /// <summary>
        /// Generate new creatures of all type (fish, Jellyfish, Crab, Squid) and add into the Ocean's CreatureList
        /// </summary>
        public void PopulateCreatures(){
            int RngGenerator = SplashKit.Rnd(0,100);

            foreach(Creature c in this._creatureList){
                if ((c.GetType() == typeof(Fish)) && _fishCount < 5){
                    _fishCount += 1;
                }else if ((c.GetType() == typeof(Jellyfish)) && _jellyfishCount < 5){
                    _jellyfishCount += 1;
                }else if ((c.GetType() == typeof(Crab)) && _crabCount < 5){
                    _crabCount += 1;
                }else if ((c.GetType() == typeof(Squid)) && _squidCount < 5){
                    _squidCount += 1;
                }
            }
            if (RngGenerator < 25 && _fishCount < 8){
                Fish f = new Fish();
                this.AddCreature(f);
                _fishCount += 1;
            }else if (RngGenerator >= 25 && RngGenerator < 50 && _jellyfishCount < 20){
                Jellyfish j = new Jellyfish();
                this.AddCreature(j);
                _jellyfishCount +=1;
            }else if (RngGenerator >= 50 && RngGenerator < 75 && _crabCount < 8){
                Crab c = new Crab();
                this.AddCreature(c);
                _crabCount +=1;
            }else if (RngGenerator >= 75 && RngGenerator < 100 && _squidCount < 8){
                Squid s = new Squid();
                this.AddCreature(s);
                _squidCount +=1;
            }
        }

        /// <summary>
        /// Draws all the creature inside the Ocean's CreatureList
        /// </summary>
        /// <param name="window"></param>
        public void DrawCreatures(Window window){
            foreach(Creature c in this._creatureList){
                if (c.GetType() == typeof(Fish)){
                    SplashKit.DrawBitmap(c.Image,c.CreatureX, c.CreatureY);
                }else if (c.GetType() == typeof(Jellyfish)){
                    SplashKit.DrawBitmap(c.Image, c.CreatureX, c.CreatureY);
                    c.DisplayCreatureHealth(window);
                    Jellyfish j = (Jellyfish)c;
                    j.MoveJellyfish();
                }else if (c.GetType() == typeof(Crab)){
                    SplashKit.DrawBitmap(c.Image, c.CreatureX, c.CreatureY);
                }else if (c.GetType() == typeof(Squid)){
                    SplashKit.DrawBitmap(c.Image, c.CreatureX, c.CreatureY);
                }
            }
        }

        /// <summary>
        /// allows the user to capture the creatures (apart from jellyfish) that are neutral
        /// and add it into user's ControlledCreatureList (at the same time remove it from the ocean's Creaturelist)
        /// </summary>
        /// <param name="oceanMap"></param>
        /// <param name="user"></param>
        public void CaptureNewCreatures(Ocean oceanMap, User user){
            List<Creature> tmplist = new List<Creature>();
            Bitmap fish = SplashKit.LoadBitmap("fish", "fish.png");
            Bitmap crab = SplashKit.LoadBitmap("crab", "crab.png");
            Bitmap squid = SplashKit.LoadBitmap("squid", "squid.png");
            foreach(Creature x in _creatureList){
                if (x.CaptureCreatures(user) == x){
                    tmplist.Add(x);
                }
            }
            foreach(Creature newCreatures in tmplist){
                double x, y;
                x = newCreatures.CreatureX;
                y = newCreatures.CreatureY;
                if (newCreatures.GetType() == typeof(Crab)){
                    Crab c = new Crab(x, y, 100, 50, crab);
                    user.ControlledCreatureList.Add(c);
                    _crabCount -=1;
                }else if (newCreatures.GetType() == typeof(Squid)){
                    Squid s = new Squid(x, y, 150, 10, squid);
                    user.ControlledCreatureList.Add(s);
                    _squidCount -=1;
                }else if (newCreatures.GetType() == typeof(Fish)){
                    Fish f = new Fish(x, y, 100, 10, fish);
                    user.ControlledCreatureList.Add(f);
                    _fishCount -=1;
                }
                oceanMap.RemoveCreature(newCreatures);
            }
        }

        /// <summary>
        /// Allows the Jellyfish objects that exist inside the ocean's CreatureList
        /// to Attack all creatures inside the User's ControlledCreatureList alongside the user itself
        /// </summary>
        /// <param name="user"></param>
        public void TriggerCreaturesTarget(User user){    
            foreach(Creature c in this._creatureList){
                if (c.GetType() == typeof(Jellyfish)){
                    Jellyfish j = (Jellyfish)c;      
                    if(j.Target(user, this) == j){   
                        this._creatureList.Remove(c);  
                        _jellyfishCount -=1;
                        break;
                    }
                }
            }
        }

      
    }

}