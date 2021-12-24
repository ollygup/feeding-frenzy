using System;
using SplashKitSDK;
using System.Collections.Generic;
using NUnit.Framework;

namespace DistinctionTask{
    [TestFixture]
    public class TestCreature{
        [Test]
        public void TestCreatureTest(){
            User user = new User();
            Ocean ocean = new Ocean();
            Bitmap fish = SplashKit.LoadBitmap("fish", "fish.png");
            Bitmap jellyfish = SplashKit.LoadBitmap("jellyfish", "jellyfish.png");
            Jellyfish j = new Jellyfish(0,0,500,20,jellyfish);
            Fish f = new Fish(20,20,100,10,fish);


            //Testing if normal fish can use virtual function provided by parent class
            user.ControlledCreatureList.Add(f);
            f.CreatureHealth -= 100;
            Assert.AreEqual(f,f.Target(user,ocean));


            //Add fish into user controlled list
            for ( int x = 0; x < 20; x++){
                Fish nfish = new Fish(20,20,100,10,fish);
                user.ControlledCreatureList.Add(nfish);
            }

            //Testing if the Jellyfish is dead using Jellyfish Target()
            j.CreatureHealth-=500;
            Assert.AreEqual(j, j.Target(user,ocean));

            //Testing if there is any creatures still surviving under user controlledcreaturelist
            Assert.AreEqual(0, ocean.CreatureList.Count);
        }
    }
}