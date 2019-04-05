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
        private int dogSpeed;
        private int sizeOfTile;
        private int boardSizeX;
        private int boardSizeY;
        private List<Direction> path;
        private List<List<Tile>> board;
        private List<Shit> shit;


        public Dog(int x, int y, int speed, int List, int sizeOfTile, List<List<Tile>> board, List<Shit> shit)
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
        public void Move(int x, int y)
        {
            this.dogPositionX = this.dogPositionX + x;
            this.dogPositionY = this.dogPositionY + y;

        }
        public void Update(GameTime gameTime, List<Shit> shit)
        {
            this.shit = shit;
            CheckIfPathIsEmpty();
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

        public int Speed
        {
            get { return dogSpeed; }
            set { dogSpeed = value; }
        }

        private void CheckIfPathIsEmpty()
        {
            if (path.Count() == 0)
            {
                GeneratePath();
            }
        }

        private void UpdatePath()
        {
            // to nie działa XD
            int newX = dogPositionX / sizeOfTile;
            int newY = dogPositionY / sizeOfTile;
            if (newX != posX)
            {
                posX = newX;
                path.RemoveAt(path.Count - 1);

                return;
            }
            if (newY != posY)
            {
                posY = newY;
                path.RemoveAt(path.Count - 1);
                return;
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
