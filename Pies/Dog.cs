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
        public float dogSpeed;
        private int sizeOfTile;
        private int boardSizeX;
        private int boardSizeY;
        private int shitTime;
        private List<Direction> path;
        private List<List<Tile>> board;
        private List<Shit> shit;
        public int changePositionX;
        public int changePositionY;
        public bool downRight;
        public bool upLeft;

        public bool isMoving;


        public Dog(int x, int y, float speed, int List, int sizeOfTile, List<List<Tile>> board, List<Shit> shit)
        {
            this.dogPositionX = x;
            this.dogPositionY = y;
            this.posX = dogPositionX / sizeOfTile;
            this.posY = dogPositionY / sizeOfTile;
            this.dogSpeed = speed;
            this.sizeOfTile = sizeOfTile;
            this.path = new List<Direction>();
            this.shit = shit;
            this.board = board;
            this.boardSizeX = board.Count();
            this.boardSizeY = board.ElementAt(0).Count();
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

        public void Update(GameTime gameTime, List<Shit> shit)
        {
            this.shit = shit;
            this.shitTime -= (int)(gameTime.ElapsedGameTime.TotalSeconds);
            CheckIfPathIsEmpty();

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

        private void CheckIfPathIsEmpty()
        {
            if (path.Count() == 0)
            {
                if (shitTime == 0)
                {
                    GeneratePath();
                }
            }
        }



        private void GeneratePath()
        {
            int destX;
            int destY;

            Random rand = new Random();
            while (true)
            {
                destX = rand.Next(0, boardSizeX);
                destY = rand.Next(0, boardSizeY);
                bool s = false;
                bool d = false;
                if ((board[destX][destY]) is TileDoors)
                {
                    d = true;
                }
                for (int i = 0; i < shit.Count(); i++)
                {
                    if (shit[i].positionX == destX && shit[i].positionY == destY)
                    {
                        s = true;
                        break;
                    }
                }
                if (s == false && d == true)
                {
                    break;
                }
            }
            int maxSteps = (Math.Abs(posX - destX) + Math.Abs(posY - destY) + 5);
            path = GenerateStep(path, destX, destY, posX, posY, posX, posY, maxSteps);
        }

        private List<Direction> GenerateStep(List<Direction> path, int destX, int destY, int currentX, int currentY, int prevX, int prevY, int maxSteps)
        {
            if (path.Count() == maxSteps)
            {
                path.RemoveRange(0, path.Count() - 1);
                return path;
            }

            if (destX == currentX && destY == currentY)
            {
                return path;
            }

            List<Direction> pathU = path;
            List<Direction> pathD = path;
            List<Direction> pathL = path;
            List<Direction> pathR = path;

            pathU.Add(Direction.Up);
            pathD.Add(Direction.Down);
            pathL.Add(Direction.Left);
            pathR.Add(Direction.Right);

            if (currentY - 1 >= 0 && (!(board[currentX][currentY - 1] is TileEmpty)) && currentY - 1 != prevY)
            {
                GenerateStep(pathU, destX, destY, currentX, currentY - 1, currentX, currentY, maxSteps);
            }
            else
            {
                pathU.RemoveRange(0, path.Count() - 1);
            }
            if (currentY + 1 < boardSizeY && (!(board[currentX][currentY + 1] is TileEmpty)) && currentY + 1 != prevY)
            {
                GenerateStep(pathD, destX, destY, currentX, currentY + 1, currentX, currentY, maxSteps);
            }
            else
            {
                pathD.RemoveRange(0, path.Count() - 1);
            }
            if (currentX - 1 >= 0 && (!(board[currentX - 1][currentY] is TileEmpty)) && currentX - 1 != prevX)
            {
                GenerateStep(pathL, destX, destY, currentX - 1, currentY, currentX, currentY, maxSteps);
            }
            else
            {
                pathD.RemoveRange(0, path.Count() - 1);
            }
            if (currentX + 1 < boardSizeX && (!(board[currentX + 1][currentY] is TileEmpty)) && currentX + 1 != prevX)
            {
                GenerateStep(pathR, destX, destY, currentX + 1, currentY, currentX, currentY, maxSteps);
            }
            else
            {
                pathR.RemoveRange(0, path.Count() - 1);
            }

            int lU;
            int lD;
            int lL;
            int lR;
            if (pathU.Count() == 0)
            {
                lU = maxSteps + 1;
            }
            else
            {
                lU = pathU.Count();
            }
            if (pathD.Count() == 0)
            {
                lD = maxSteps + 1;
            }
            else
            {
                lD = pathU.Count();
            }
            if (pathL.Count() == 0)
            {
                lL = maxSteps + 1;
            }
            else
            {
                lL = pathU.Count();
            }
            if (pathR.Count() == 0)
            {
                lR = maxSteps + 1;
            }
            else
            {
                lR = pathU.Count();
            }
            if (lU <= lD && lU <= lR && lU <= lL)
            {
                return pathU;
            }
            if (lD <= lU && lD <= lR && lD <= lL)
            {
                return pathD;
            }
            if (lL <= lU && lL <= lR && lL <= lD)
            {
                return pathL;
            }
            if (lR <= lU && lR <= lD && lR <= lL)
            {
                return pathR;
            }
            return pathU;
        }
    }

}
