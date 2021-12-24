using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask{
    public class Program
    {
        public static void Main()
        {
            Window window;
            window = new Window("Feeding Frenzy", 800, 600);
            Bitmap wallpaper = new Bitmap("bg1", "bg1.jpg");

            User user1 = new User();
            
            Ocean oceanMap = new Ocean();
     
            //A list to deploy effects when character is low on health
            List<double[]> tmplist = new List<double[]>();

            Font font = SplashKit.LoadFont("gamerobot", "GameRobot.ttf");
            Bitmap background = SplashKit.LoadBitmap("bg","restartbg.jpg");
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                if (user1.StartGame == true){
                    user1.MainMenu(window, background);
                    if(SplashKit.AnyKeyPressed()){
                        user1.StartGame = false;
                    }
                }
                else if (user1.UserHealth > 0 && user1.StartGame == false){
                    window.DrawBitmap(wallpaper,-200, -200);
                    SplashKit.DrawBitmap(user1.UserImage, user1.UserX, user1.UserY);
                    
                    //---Display Health-----------
                    user1.DisplayHealth(window);
                    tmplist = user1.DisplayLowHealthEffect(tmplist);


                    //--Display Score---
                    user1.DrawScore(font, 0, 0);


                    //---Generate food randomly-------
                    if (oceanMap.FoodList.Count < 50){
                        oceanMap.PopulateFood();
                    }
                    oceanMap.DrawFood();


                    //---Delete Eaten food from Ocean-------
                    user1.EatFood(oceanMap, user1.UserX, user1.UserY, user1.UserImage);


                    //Draw controlled creatures---
                    user1.DrawCreatures();


                    //allow character movement---
                    user1.movement();
                    user1.CtrlCreaturesMovement();

                    
                    //---Update the position of the camera view from the player-----
                    user1.UpdateCameraPosition();


                    //---Generate Creatures Randomly---
                    oceanMap.PopulateCreatures();
                    oceanMap.DrawCreatures(window);


                    //---Allows Enemy to attack and user to get hit-----
                    oceanMap.TriggerCreaturesTarget(user1);
                    if (user1.UpdateTimer() == 119){
                        user1.GetHit(oceanMap);
                    }

                    //--Capture new creatures---
                    oceanMap.CaptureNewCreatures(oceanMap, user1);



                }
                else if(user1.UserHealth == 0 || user1.UserHealth <= 0){
                    user1.GamePage(background, font, "Score: " + user1.Score, 280);
                    user1 = user1.RestartGame(user1);
                    oceanMap = new Ocean();
                }

                window.Refresh(60);
            }while(!SplashKit.QuitRequested());
        }
    }
}