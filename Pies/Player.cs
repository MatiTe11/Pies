using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;


namespace Pies

{
    class Player
    {
        protected int playerPositionX;
        protected int playerPositionY;
        public int playerSpeed;

        protected int keyPressed;

        public Player() {}

        public Player(int x, int y, int speed)
        {
            this.playerPositionX = x;     
            this.playerPositionY = y;
            this.playerSpeed = speed;
        }
        public void Move(int x, int y)
        {

        }


        
    }
}
