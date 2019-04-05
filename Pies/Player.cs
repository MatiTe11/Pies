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

        public float playerSpeed;
        public int sizeOfTile;

        public bool downRight;
        public bool upLeft;

        public bool isMoving;

        public Player() {}

        public Player(int x, int y, float speed, int sizeOfTile)
        {
            this.playerPositionX = x;     
            this.playerPositionY = y;
            this.playerSpeed = speed;
            this.sizeOfTile = sizeOfTile;
            this.changePositionX = 0;
            this.changePositionY = 0;
            this.isMoving = false;
            this.upLeft = false;
            this.downRight = false;
        }

        public void Move(Direction direction)
        {
            if(direction == Direction.Up)
            {
                this.changePositionY += sizeOfTile;
                this.isMoving = true;
                this.upLeft = true;
            }
            else if(direction == Direction.Down)
            {
                this.changePositionY += sizeOfTile;
                this.isMoving = true;
                this.downRight = true;
            }
            else if (direction == Direction.Left)
            {
                this.changePositionX += sizeOfTile;
                this.isMoving = true;
                this.upLeft = true;
            }
            else if (direction == Direction.Right)
            {
                this.changePositionX += sizeOfTile;
                this.downRight = true;
                this.isMoving = true;
            }

        }
        public void Update(GameTime gameTime)
        {
            if(this.IsMoving())
            {
                if(this.changePositionX > 0)
                {
                    if (this.downRight) //move right
                    {
                        this.playerPositionX += (int)(playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionX -= (int)(playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if(changePositionX < 0)
                        {
                            this.playerPositionX -= this.changePositionX;
                            this.changePositionX = 0;
                        }
                    }
                    else //move left
                    {
                        this.playerPositionX -= (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionX -= (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionX < 0)
                        {
                            this.playerPositionX += this.changePositionX;
                            this.changePositionX = 0;
                        }
                    }                          
                }
                else if(this.changePositionY > 0)
                {
                    if (this.downRight) //move down
                    {
                        this.playerPositionY += (int)(playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionY -= (int)(playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionY < 0)
                        {
                            this.playerPositionY -= this.changePositionY;
                            this.changePositionY = 0;
                        }
                    }
                    else //move up
                    {
                        this.playerPositionY -= (int)(playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionY -= (int)(playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionY < 0)
                        {
                            this.playerPositionY += this.changePositionY;
                            this.changePositionY = 0;
                        }
                    }
                }
                else
                {
                    this.isMoving = false;
                }
            }
        }

        public bool IsMoving()
        {
            return isMoving;
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
