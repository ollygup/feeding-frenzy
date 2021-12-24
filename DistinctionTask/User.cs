using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace DistinctionTask{
    public class User{
        private int _userHealth;
        private int _userX;
        private int _userY;
        private int _userSpeed;
        private int _foodBar;
        private Bitmap _userImage;
        private List<Creature> _controlledCreatureList;
        private int _timer;
        private Boolean _startGame;
        private int _score;

        /// <summary>
        /// A constructor that creates a User object
        /// </summary>
        public User(){
            _userHealth = 100;
            _userX = 200;
            _userY = 200;
            _userSpeed = 2;
            _foodBar = 100;
            _controlledCreatureList = new List<Creature>();
            _userImage = SplashKit.LoadBitmap("user", "user.png");
            _timer = 0;
            _startGame = true;
            _score = 0;
        }

        /// <summary>
        /// Property for user's health
        /// </summary>
        /// <value></value>
        public int UserHealth{
            get { return _userHealth; }
            set { _userHealth = value; }
        }
        
        /// <summary>
        /// Property for User's X coordinate
        /// </summary>
        /// <value></value>
        public int UserX{
            get { return _userX; }
            set { _userX = value; }
        }

        /// <summary>
        /// property for User's Y Coordinate
        /// </summary>
        /// <value></value>
        public int UserY{
            get { return _userY; }
            set { _userY = value; }
        }

        /// <summary>
        /// Readonly property for User's Speed
        /// </summary>
        /// <value></value>
        public int UserSpeed{
            get { return _userSpeed; }
        }

        /// <summary>
        /// Property for User's FoodBar
        /// </summary>
        /// <value></value>
        public int FoodBar{
            get { return _foodBar; }
            set { _foodBar = value; }
        }

        /// <summary>
        /// Readonly property for User's Image
        /// </summary>
        /// <value></value>
        public Bitmap UserImage{
            get { return _userImage; }
        }

        /// <summary>
        /// Readonly property for User's ControlledCreatureList
        /// </summary>
        /// <value></value>
        public List<Creature> ControlledCreatureList{
            get { return _controlledCreatureList; }
        }
        
        /// <summary>
        /// Property that controls if the user should be on the main menu page or inside the game 
        /// </summary>
        /// <value></value>
        public Boolean StartGame{
            get { return _startGame; }
            set { _startGame = value; }
        }

        /// <summary>
        /// Property for User's Score
        /// </summary>
        /// <value></value>
        public int Score{
            get { return _score; }
            set { _score = value; }
        }
        
        /// <summary>
        /// Adds Creature into the user's ControlledCreatureList
        /// </summary>
        /// <param name="creature"></param>
        public void AddCCreature(Creature creature){
            _controlledCreatureList.Add(creature);
        }

        /// <summary>
        /// Removes Creature from the user's ControlledCreatureList
        /// </summary>
        /// <param name="creature"></param>
        public void RemoveCreature(Creature creature){
            _controlledCreatureList.Remove(creature);
        }

        /// <summary>
        /// Allow user to eat food that lies on the ocean map
        /// and gain new Fish object that will be added into the user's ControlledCreatureList
        /// and heals the user if not on full health
        /// </summary>
        /// <param name="oceanMap"></param>
        /// <param name="userX"></param>
        /// <param name="userY"></param>
        /// <param name="bmp1"></param>
        public void EatFood(Ocean oceanMap, double userX, double userY, Bitmap bmp1){
            List<Food> results = new List<Food>();
            foreach (Food f in oceanMap.FoodList){
                Boolean collision = SplashKit.BitmapPointCollision(bmp1, userX, userY,f.FoodX, f.FoodY);
                if (collision == true){
                    results.Add(f);
                }
                collision = false;
            }
            foreach(Food x in results){
                oceanMap.RemoveFood(x);
                this._foodBar -= x.Point;
                if (_foodBar == 0 && _userHealth != 100){
                    _foodBar = 100;
                    _userHealth += x.Point;
                }
                else if (_foodBar == 0){
                    _foodBar = 100;
                    Random rnd = new Random();
                    double rndX = rnd.Next(this._userX-100,this._userX+100);
                    double rndY = rnd.Next(this._userY-100,this._userY+100);
                    //create new fish
                    Bitmap fish = new Bitmap("fish","fish.png");
                    Fish f = new Fish(rndX, rndY, 100, 10, fish);
                    //add into user creature list
                    this.AddCCreature(f);
                }
            }
        }

        /// <summary>
        /// Draws all the creature from user's ControlledCreatureList into the ocean map 
        /// </summary>
        public void DrawCreatures(){
            foreach(Creature c in this.ControlledCreatureList){
                if (c.GetType() == typeof(Fish)){
                    SplashKit.DrawBitmap(c.Image,c.CreatureX, c.CreatureY);
                }else if (c.GetType() == typeof(Crab)){
                    SplashKit.DrawBitmap(c.Image,c.CreatureX, c.CreatureY);
                }else if (c.GetType() == typeof(Squid)){
                    SplashKit.DrawBitmap(c.Image,c.CreatureX, c.CreatureY);
                }
            }
        }

        /// <summary>
        /// The window itself is 800x600 but the entire ocean map is actually 1000x800
        /// therefore this function allows the user to move the camera around (size of 800x600)
        /// depending on where the user is
        /// </summary>
        public void UpdateCameraPosition(){
            int uncoveredScreen = 200;
            double hiddenLeft = Camera.X + uncoveredScreen;
            double hiddenRight = hiddenLeft + SplashKit.ScreenWidth() -  2 * uncoveredScreen;  
            double hiddenTop = Camera.Y + uncoveredScreen;
            double hiddenBottom = hiddenTop + SplashKit.ScreenHeight() - 2 * uncoveredScreen;  
            //Test Top bottom screen
            if ((this._userY < hiddenTop) && (this._userY >0)){
                SplashKit.MoveCameraBy(0, this._userY - hiddenTop);
            }else if ((this._userY > hiddenBottom) && (this._userY < 600)){
                SplashKit.MoveCameraBy(0, this._userY - hiddenBottom);
            }
            //Test left right screen
            if ((this._userX < hiddenLeft) && (this._userX > 0)){
                SplashKit.MoveCameraBy(this._userX - hiddenLeft, 0);
            }else if ((this._userX > hiddenRight) && (this._userX < 800)){
                SplashKit.MoveCameraBy(this._userX - hiddenRight, 0);
            }
        }

        /// <summary>
        /// Displays user's health with a rectangle bar on a fixed window location
        /// </summary>
        /// <param name="window"></param>
        public void DisplayHealth(Window window){
            SplashKit.DrawRectangleOnWindow(window, Color.Black, SplashKit.ToWorldX(200), SplashKit.ToWorldY(570), 400, 20);
            SplashKit.FillRectangle(Color.GreenYellow, SplashKit.ToWorldX(200), SplashKit.ToWorldY(570), this.UserHealth * 4, 20);
        }
        
        /// <summary>
        /// Returns 4 random value in an array 
        /// </summary>
        /// <returns></returns>
        public double[] ReturnRandomValue(){
            double[] array = new double[4];
            array[0] = SplashKit.Rnd(0,550);
            array[1] = SplashKit.Rnd(0,450);
            array[2] = SplashKit.Rnd(0,360);
            array[3] = SplashKit.Rnd(1, 2);
            return array;
        }
        
        /// <summary>
        /// Draws blood at a random location on the window if the user's health go below a certain value
        /// and remove it if the user regains health
        /// </summary>
        /// <param name="tmplist"></param>
        /// <returns></returns>
        public List<double[]> DisplayLowHealthEffect(List<double[]> tmplist){
            if (this._userHealth < 80 && tmplist.Count == 0){
                tmplist.Add(this.ReturnRandomValue());
            }else if (this._userHealth < 50 && tmplist.Count == 1){
                tmplist.Add(this.ReturnRandomValue());
            }else if (this.UserHealth < 21 && tmplist.Count == 2){
                tmplist.Add(this.ReturnRandomValue());
            }
            
            if (this._userHealth >= 80 & tmplist.Count >= 1){
                tmplist.Remove(tmplist[0]);
            }else if (this._userHealth >= 50 && tmplist.Count >= 2){
                tmplist.Remove(tmplist[1]);
            }else if (this.UserHealth >= 21 && tmplist.Count >= 3){
                tmplist.Remove(tmplist[2]);
            }
            Bitmap blood = SplashKit.LoadBitmap("blood", "blood.png");
            foreach( double[] i in tmplist){
                blood.Draw(SplashKit.ToWorldX(i[0]), SplashKit.ToWorldY(i[1]), SplashKit.OptionRotateBmp(i[2], SplashKit.OptionScaleBmp(i[3],i[3])));
            }
            return tmplist;
        }

        /// <summary>
        /// if the user's collide with jellyfish, takes damage and reduces health
        /// and set the user's health to 0 if it went below 0
        /// </summary>
        /// <param name="oceanMap"></param>
        public void GetHit(Ocean oceanMap){
            foreach (Creature c in oceanMap.CreatureList){
                if (c.GetType() == typeof(Jellyfish)){
                    Boolean userCollision = SplashKit.BitmapCollision(this._userImage, this._userX, this._userY, c.Image, c.CreatureX, c.CreatureY);
                    if (userCollision == true){
                        this._userHealth -= c.CreatureAttack;
                        userCollision = false;
                        if (this._userHealth == 0 || this._userHealth < 0){
                            this._userHealth = 0;
                        }
                    }
                }
            }    
        }

        /// <summary>
        /// Allows user to move around the ocean map and 
        /// controls the Creatures in user's ControlledCreatureList
        /// to move around in the same direction
        /// </summary>
        public void movement(){
            if(SplashKit.KeyDown(KeyCode.UpKey) && (this._userY > -200)){
                this._userY -= this._userSpeed;
                foreach (Creature c in this._controlledCreatureList){
                    if (c.CreatureY >-200){  
                        c.CreatureY -= c.CreatureSpeed;
                    }
                }
            }else if(SplashKit.KeyDown(KeyCode.DownKey) && (this._userY < 700)){
                this._userY += this._userSpeed;
                foreach (Creature c in this._controlledCreatureList){
                    if (c.CreatureY < 700){
                        c.CreatureY += c.CreatureSpeed;
                    }
                }
            }else if(SplashKit.KeyDown(KeyCode.LeftKey) && (this._userX > -200)){
                this._userX -= this._userSpeed;
                foreach (Creature c in this._controlledCreatureList){
                    if (c.CreatureX > -200){
                        c.CreatureX -= c.CreatureSpeed;
                    }
                }
            }else if(SplashKit.KeyDown(KeyCode.RightKey) && (this._userX < 900)){
                this._userX += this._userSpeed;
                foreach (Creature c in this._controlledCreatureList){
                    if (c.CreatureX < 900){
                        c.CreatureX += c.CreatureSpeed;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a timer that limits the frequency of attack that can be dealt on the user
        /// </summary>
        /// <returns></returns>
        public int UpdateTimer(){
            if (_timer != 120){
                _timer += 1;
            }else {
                _timer = 0;
            }
            return _timer;
        }

        /// <summary>
        /// Controls the creature inside the user's ControlledCreatureList
        /// by holding left mouse button on a specific location within the game window
        /// to redirect the creatures towards that location
        /// </summary>
        public void CtrlCreaturesMovement(){
            if (SplashKit.MouseShown() == true){
                if (SplashKit.MouseDown(MouseButton.LeftButton) == true){
                    double xcord = SplashKit.ToWorldX(SplashKit.MouseX());
                    double ycord = SplashKit.ToWorldY(SplashKit.MouseY());
                    foreach(Creature c in this._controlledCreatureList){
                        double xDist = c.CreatureX - xcord;
                        double yDist = c.CreatureY - ycord;
                        if (yDist > 1){
                            c.CreatureY -= c.CreatureSpeed;
                        }
                        if(yDist < -1){
                            c.CreatureY += c.CreatureSpeed;
                        }
                        if (xDist > 1){
                            c.CreatureX -= c.CreatureSpeed;
                        }
                        if(xDist < -1){
                            c.CreatureX += c.CreatureSpeed;
                        }
                    }
                }
            }       
        }

        /// <summary>
        /// Draws a general main page and end game page that can be reused
        /// </summary>
        /// <param name="background"></param>
        /// <param name="font"></param>
        /// <param name="message"></param>
        /// <param name="mesgcoordX"></param>
        public void GamePage(Bitmap background, Font font, string message, int mesgcoordX){
            SplashKit.DrawBitmap(background, 0, 0);
            SplashKit.MoveCameraTo(0,0);
            SplashKit.DrawText(message, Color.Black, font, 50,  mesgcoordX, 410);
            SplashKit.DrawText("Feeding Frenzy", Color.Black, font, 80,  111, 20);
            SplashKit.DrawText("Feeding Frenzy", Color.LimeGreen, font, 80,  105, 20);    
        }
        
        /// <summary>
        /// Restart the game/ start the game if replay button is clicked
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User RestartGame(User user){
            Bitmap restart = SplashKit.LoadBitmap("replay", "replay.png");
            SplashKit.MoveCameraTo(0,0);
            if (SplashKit.MouseX() > 690 && SplashKit.MouseX() < 790 && SplashKit.MouseY() > 490 && SplashKit.MouseY() < 590){
                SplashKit.DrawBitmap(restart, 690, 490, SplashKit.OptionScaleBmp(1.1, 1.1));
                if(SplashKit.MouseClicked(MouseButton.LeftButton)){
                    user = new User();
                }
            }else{
                SplashKit.DrawBitmap(restart, 690, 490, SplashKit.OptionScaleBmp(1,1));
            }
            return user;
        }

        /// <summary>
        /// Draws a main menu with info button that displays the game objects and all functionalities
        /// </summary>
        /// <param name="window"></param>
        /// <param name="background"></param>
        public void MainMenu(Window window, Bitmap background){
            Bitmap fish = SplashKit.LoadBitmap("fish", "fish.png");
            Bitmap crab = SplashKit.LoadBitmap("crab", "crab.png");
            Bitmap squid = SplashKit.LoadBitmap("squid", "squid.png");
            Bitmap jellyfish = SplashKit.LoadBitmap("jellyfish", "jellyfish.png");
            Bitmap user = SplashKit.LoadBitmap("user", "user.png");
            Bitmap food = SplashKit.LoadBitmap("food", "food.png");
            Bitmap restart = SplashKit.LoadBitmap("replay", "replay.png");
            Bitmap info = SplashKit.LoadBitmap("info", "info.png");
            Font font = SplashKit.LoadFont("gamerobot", "GameRobot.ttf");
            if (SplashKit.MouseX() > 30 && SplashKit.MouseX() < 140 && SplashKit.MouseY() > 460 && SplashKit.MouseY() < 580){
                SplashKit.FillRectangle(Color.LightBlue, 0, 0, 800, 600);
                SplashKit.DrawText("Food  -  eat food to gain new fish as your sidekick", Color.Black, font, 18, 20, 120);
                SplashKit.DrawText("Fish  -  can be captured or spawned through consuming food", Color.Black, font, 18, 20, 195);
                SplashKit.DrawText("Crab  -  can be captured and become your sidekick", Color.Black, font, 18, 20, 145);
                SplashKit.DrawText("Squid -  can be captured and become your sidekick", Color.Black, font, 18, 20, 170);
                SplashKit.DrawText("Jellyfish  -  an enemy, kill or be killed!", Color.Black, font, 18, 20, 220);
                SplashKit.DrawText("You  -  cannot attack enemy but can control your creatures to engage", Color.Black, font, 18, 20, 245);
                SplashKit.DrawBitmap(food, 100, 340, SplashKit.OptionScaleBmp(2,2));
                SplashKit.DrawBitmap(fish, 200, 300, SplashKit.OptionScaleBmp(2,2));
                SplashKit.DrawBitmap(crab, 300, 300, SplashKit.OptionScaleBmp(2,2));
                SplashKit.DrawBitmap(squid, 400, 300, SplashKit.OptionScaleBmp(2,2));
                SplashKit.DrawBitmap(jellyfish, 500, 280);
                SplashKit.DrawBitmap(user, 620, 280);
                SplashKit.DrawText("Movement", Color.Black, font, 30, 150, 400);
                SplashKit.DrawText("KeyUp, KeyDown, KeyLeft, KeyRight  -  control character movement", Color.Black, font, 18, 20, 435);
                SplashKit.DrawText("Mouse Left Click Hold  -  control your creatures to move towards mouse pointer", Color.Black, font, 18, 20, 460);
                SplashKit.DrawBitmap(info, 10, 450, SplashKit.OptionScaleBmp(1.1, 1.1));
                SplashKit.DrawText("Feeding Frenzy", Color.Black, font, 80,  111, 20);
                SplashKit.DrawText("Feeding Frenzy", Color.LimeGreen, font, 80,  105, 20);
            }else{
                this.GamePage(background, font, "Press Replay to Start", 110);
                SplashKit.DrawBitmap(restart, 690, 490, SplashKit.OptionScaleBmp(1,1));
                SplashKit.DrawBitmap(info, 10, 450, SplashKit.OptionScaleBmp(1,1));
                if (SplashKit.MouseX() > 690 && SplashKit.MouseX() < 790 && SplashKit.MouseY() > 490 && SplashKit.MouseY() < 590){
                    SplashKit.DrawBitmap(restart, 690, 490, SplashKit.OptionScaleBmp(1.1, 1.1));
                    if(SplashKit.MouseClicked(MouseButton.LeftButton)){
                        this._startGame = false;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the score obtained by the user at certain coordinate
        /// </summary>
        /// <param name="font"></param>
        /// <param name="coordX"></param>
        /// <param name="coordY"></param>
        public void DrawScore(Font font, int coordX, int coordY){
            SplashKit.DrawText("Score: " + _score, Color.White, font, 30, SplashKit.ToWorldX(coordX), SplashKit.ToWorldY(coordY));
        }      
    }
}