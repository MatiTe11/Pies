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

        public int changePositionX;
        public int changePositionY;

        public int playerSpeed;
        public int sizeOfTile;

        public Player() {}

        public Player(int x, int y, int speed, int sizeOfTile)
        {
            this.playerPositionX = x;     
            this.playerPositionY = y;
            this.playerSpeed = speed;
            this.sizeOfTile = sizeOfTile;
            this.changePositionX = 0;
            this.changePositionY = 0;
        }
        public void Move(Direction direction)
        {
            if(direction == Direction.Up)
            {
                this.changePositionY -= sizeOfTile;
            }
            else if(direction == Direction.Down)
            {
                this.changePositionY += sizeOfTile;
            }
            else if (direction == Direction.Left)
            {
                this.changePositionX -= sizeOfTile;
            }
            else if (direction == Direction.Right)
            {
                this.changePositionX += sizeOfTile;
            }

        }
        public void Update(GameTime gameTime)
        {

        }

        public bool isMoving()
        {

            return false;
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
