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


        public Dog(int x, int y, int speed, int List, int sizeOfTile, List<List<Tile>> board)
        {
            this.dogPositionX = x;
            this.dogPositionY = y;
            this.posX = dogPositionX / sizeOfTile;
            this.posY = dogPositionY / sizeOfTile;
            this.dogSpeed = speed;
            this.sizeOfTile = sizeOfTile;
            this.path = new List<Direction>();
            this.board = board;
            this.boardSizeX = board.Count();
            this.boardSizeY = board.ElementAt(0).Count();
        }
        public void Move(int x, int y)
        {
            this.dogPositionX = this.dogPositionX + x;
            this.dogPositionY = this.dogPositionY + y;

        }
        public void Update(GameTime gameTime)
        {

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
                // Generate path
            }
        }

        private void UpdatePath()
        {
            // to nie działa XD
            int newX = dogPositionX/sizeOfTile;
            int newY = dogPositionY/sizeOfTile;
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

        private void GeneratePath(List<Direction> path)
        {


        }



    }
}
