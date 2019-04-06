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
    class Dog
    {
        private int dogPositionX;
        private int dogPositionY;
        private int posX;
        private int posY;
        private int prevX;
        private int prevY;
        public float dogSpeed;
        private int sizeOfTile;
        private int boardSizeX;
        private int boardSizeY;
        private float shitTime;
        private List<Direction> path;
        private List<List<Tile>> board;
        private List<Shit> shit;
        public int changePositionX;
        public int changePositionY;
        public bool downRight;
        public bool upLeft;

        public bool isMoving;


        public Dog(int x, int y, float speed, int sizeOfTile, List<List<Tile>> board, List<Shit> shit)
        {
            this.dogPositionX = x;
            this.dogPositionY = y;
            this.posX = dogPositionX / sizeOfTile;
            this.posY = dogPositionY / sizeOfTile;
            this.prevX = posX;
            this.prevY = posY;
            this.dogSpeed = speed;
            this.sizeOfTile = sizeOfTile;
            this.path = new List<Direction>();
            this.shit = shit;
            this.board = board;
            //this.boardSizeX = board.Count();
            //this.boardSizeY = board.ElementAt(0).Count();
            this.boardSizeX = 3;
            this.boardSizeY = 3;
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Up)
            {
                this.changePositionY += sizeOfTile;
                this.isMoving = true;
                this.downRight = false;
                this.upLeft = true;
            }
            else if (direction == Direction.Down)
            {
                this.changePositionY += sizeOfTile;
                this.isMoving = true;
                this.upLeft = false;
                this.downRight = true;
            }
            else if (direction == Direction.Left)
            {
                this.changePositionX += sizeOfTile;
                this.isMoving = true;
                this.downRight = false;
                this.upLeft = true;
            }
            else if (direction == Direction.Right)
            {
                this.changePositionX += sizeOfTile;
                this.downRight = true;
                this.upLeft = false;
                this.isMoving = true;
            }

        }

        public bool IsMoving()
        {
            return isMoving;
        }

        private void UpdatePosition()
        {
            int newPosX = dogPositionX / sizeOfTile;
            int newPosY = dogPositionY / sizeOfTile;
            if (newPosX != posX || newPosY != posY)
            {
                prevX = posX;
                prevY = posY;
                posX = newPosX;
                posY = newPosY;
            }
        }

        public void Update(GameTime gameTime, List<Shit> shit)
        {
            this.shit = shit;
            this.shitTime -= (float)(gameTime.ElapsedGameTime.TotalSeconds);
            if(this.shitTime < 0)
            {
                this.shitTime = 0;
            }

            UpdatePosition();
      
            if (this.IsMoving())
            {
                if (this.changePositionX > 0)
                {
                    if (this.downRight) //move right
                    {
                        this.dogPositionX += (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionX -= (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionX < 0)
                        {
                            this.dogPositionX -= this.changePositionX;
                            this.changePositionX = 0;
                        }
                    }
                    else //move left
                    {
                        this.dogPositionX -= (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionX -= (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionX < 0)
                        {
                            this.dogPositionX += this.changePositionX;
                            this.changePositionX = 0;
                        }
                    }
                }
                else if (this.changePositionY > 0)
                {
                    if (this.downRight) //move down
                    {
                        this.dogPositionY += (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionY -= (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionY < 0)
                        {
                            this.dogPositionY -= this.changePositionY;
                            this.changePositionY = 0;
                        }
                    }
                    else //move up
                    {
                        this.dogPositionY -= (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionY -= (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionY < 0)
                        {
                            this.dogPositionY += this.changePositionY;
                            this.changePositionY = 0;
                        }
                    }
                }
                else
                {
                    this.isMoving = false;
                }
            }
            else
            {
                CheckIfPathIsEmpty();
            }

        }

        public int PosX
        {
            get { return dogPositionX; }
            set { dogPositionX = value; }
        }

        public int PosY
        {
            get { return dogPositionY; }
            set { dogPositionY = value; }
        }

        public float Speed
        {
            get { return dogSpeed; }
            set { dogSpeed = value; }
        }

        private bool CheckIfPathIsEmpty()
        {
            if (shitTime == 0)
            {
                GenerateMove();
                return false;
            }
            return false;
        }

        public bool IsShitting()
        {
            if (shitTime > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GenerateMove()
        {
            Random rand = new Random();
            if (rand.Next(0, 4) % 4 != 0)
            {
                int dir = rand.Next(0, 4);
                if (dir == 0 && posY - 1 >= 0 && board[posX][posY-1] != Tile.Empty && !(posX == prevX && posY - 1 == prevY))
                {
                    Move(Direction.Up);
                    return;
                }
                if (dir == 1 && posY + 1 < boardSizeX && board[posX][posY + 1] != Tile.Empty && !(posX == prevX && posY + 1 == prevY))
                {
                    Move(Direction.Down);
                    return;
                }
                if (dir == 2 && posX - 1 >= 0 && board[posX - 1][posY] != Tile.Empty && !(posX - 1 == prevX && posY == prevY))
                {
                    Move(Direction.Left);
                    return;
                }
                if (dir == 3 && posX + 1 < boardSizeY && board[posX + 1][posY] != Tile.Empty && !(posX + 1 == prevX && posY == prevY))
                {
                    Move(Direction.Right);
                    return;
                }
            }

            if (rand.Next(0,2) % 2 == 0)
            {
                if (posY - 1 >= 0 && board[posX][posY - 1] != Tile.Empty && (posX == prevX && posY - 1 == prevY))
                {
                    Move(Direction.Up);
                    return;
                }
                if (posY + 1 < boardSizeX && board[posX][posY + 1] != Tile.Empty && (posX == prevX && posY + 1 == prevY))
                {
                    Move(Direction.Down);
                    return;
                }
                if (posX - 1 >= 0 && board[posX - 1][posY] != Tile.Empty && (posX - 1 == prevX && posY == prevY))
                {
                    Move(Direction.Left);
                    return;
                }
                if (posX + 1 < boardSizeY && board[posX + 1][posY] != Tile.Empty && (posX + 1 == prevX && posY == prevY))
                {
                    Move(Direction.Right);
                    return;
                }
            }
            bool kupa = false;
            foreach (Shit x in shit)
            {
                if (x.positionX == posX && x.positionY == posY)
                {
                    kupa = true;
                }
            }
            if(kupa == false && (board[posX][posY] == Tile.Door1 || board[posX][posY] == Tile.Door2 || board[posX][posY] == Tile.Door3))
            {
                shitTime = 5;
                return;
            }
        }

        }

}
