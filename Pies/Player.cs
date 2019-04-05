using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Pies

{
    class Player
    {
        protected int playerPositionX;
        protected int playerPositionY;
        public int playerSpeed;
        public int sizeOfTile;

        protected int keyPressed;

        public Player() {}

        public Player(int x, int y, int speed, int sizeOfTile)
        {
            this.playerPositionX = x;     
            this.playerPositionY = y;
            this.playerSpeed = speed;
            this.sizeOfTile = sizeOfTile;
        }
        public void Move(int x, int y)
        {
            this.playerPositionX = this.playerPositionX + x;
            this.playerPositionY = this.playerPositionY + y;
           
        }
        public void Update(GameTime gameTime)
        {

        }

        public int PosX
        {
            get { return playerPositionX; }
            set { playerPositionX = value; }
        }

        public int PosY
        {
            get { return playerPositionY; }
            set { playerPositionY = value; }
        }

        public int Speed
        {
            get { return playerSpeed; }
            set { playerSpeed = value; }
        }

    }
}
