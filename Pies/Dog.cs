﻿using System;
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

        List<Texture2D> textures;
        int currentFrame;
        float totalTime;

        private int dogPositionX;
        private int dogPositionY;
        private int posX;
        private int posY;
        private int prevX;
        private int prevY;
        private float dogSpeed;
        private int sizeOfTile;
        private int boardSizeX;
        private int boardSizeY;
        private float shitTime;
        private bool isShitting;
        private List<Direction> path;
        private List<List<Tile>> board;
        private List<Shit> shit;
        public int changePositionX;
        public int changePositionY;
        public bool downRight;
        public bool upLeft;
        private int firstTailPositionX;
        private int firstTailPositionY;

        public bool isMoving;


        public Dog(int x, int y, int firstTailPositionX, int firstTailPositionY, float speed, int sizeOfTile, List<List<Tile>> board, List<Shit> shit)
        {
            this.dogPositionX = x;
            this.dogPositionY = y;
            this.posX = (x - firstTailPositionX + sizeOfTile / 2) / sizeOfTile;
            this.posY = (y - firstTailPositionY + sizeOfTile / 2) / sizeOfTile;
            this.firstTailPositionX = firstTailPositionX;
            this.firstTailPositionY = firstTailPositionY;
            this.prevX = posX  - 1;
            this.prevY = posY;
            this.dogSpeed = speed;
            this.sizeOfTile = sizeOfTile;
            this.path = new List<Direction>();
            this.shit = shit;
            this.board = board;
            shitTime = 0;
            isShitting = false;
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

        private void UpdatePosition()
        {
            int newPosX = (dogPositionX - firstTailPositionX + sizeOfTile / 2) / sizeOfTile;
            int newPosY = (dogPositionY - firstTailPositionY + sizeOfTile / 2) / sizeOfTile;
            if (newPosX != posX || newPosY != posY)
            {
                prevX = posX;
                prevY = posY;
                posX = newPosX;
                posY = newPosY;
            }
        }

        public void LoadContent(List<Texture2D> textures)
        {
            this.textures = textures;
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
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (totalTime > 0.1f)
                {
                    totalTime = 0;
                    currentFrame++;
                }
                if (currentFrame == textures.Count())
                    currentFrame = 0;

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
                            this.dogPositionY += this.changePositionY;
                            this.changePositionY = 0;
                        }
                    }
                    else //move up
                    {
                        this.dogPositionY -= (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionY -= (int)(dogSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionY < 0)
                        {
                            this.dogPositionY -= this.changePositionY;
                            this.changePositionY = 0;
                        }
                    }
                }
                else
                {
                    this.isMoving = false;
                    currentFrame = 0;
                }
            }
            else
            {
                CheckIfPathIsEmpty();
            }

        }

        public void Draw(SpriteBatch spriteBatch, float scale, int sizeOfTale)
        {
            if (!isShitting)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textures[currentFrame], new Vector2(PosX, PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale), SpriteEffects.None, 0f);
                spriteBatch.End();
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
                isShitting = true;
                return true;
            }
            else
            {
                if (isShitting == true)
                {
                    shit.Add(new Shit(posX, posY));
                    isShitting = false;
                }
                return false;
            }
        }

        public List<Shit> Shit
        {
            get { return shit; }
        }

        private void GenerateMove()
        {
            Random rand = new Random();
            if (rand.Next(0, 7) == 0)
            {
                bool kupa = false;
                foreach (Shit x in shit)
                {
                    if (x.positionX == posX && x.positionY == posY)
                    {
                        kupa = true;
                        break;
                    }
                }
                if (kupa == false && (board[posX][posY] == Tile.Door1 || board[posX][posY] == Tile.Door2 || board[posX][posY] == Tile.Door3))
                {
                    shitTime = 1;
                    return;
                }
                return;
            }
            if (rand.Next(0, 7) > 0)
            {

                int dir = rand.Next(0, 4);
                if (dir == 0 && posY - 1 >= 0 && board[posX][posY-1] != Tile.Empty && !(posX == prevX && posY - 1 == prevY))
                {
                    Move(Direction.Up);
                    return;
                }
                if (dir == 1 && posY + 1 < boardSizeY && board[posX][posY + 1] != Tile.Empty && !(posX == prevX && posY + 1 == prevY))
                {
                    Move(Direction.Down);
                    return;
                }
                if (dir == 2 && posX - 1 >= 0 && board[posX - 1][posY] != Tile.Empty && !(posX - 1 == prevX && posY == prevY))
                {
                    Move(Direction.Left);
                    return;
                }
                if (dir == 3 && posX + 1 < boardSizeX && board[posX + 1][posY] != Tile.Empty && !(posX + 1 == prevX && posY == prevY))
                {
                    Move(Direction.Right);
                    return;
                }

            }
            else
            {
                if (posY - 1 >= 0 && board[posX][posY - 1] != Tile.Empty && (posX == prevX && posY - 1 == prevY))
                {
                    Move(Direction.Up);
                    return;
                }
                if (posY + 1 < boardSizeY && board[posX][posY + 1] != Tile.Empty && (posX == prevX && posY + 1 == prevY))
                {
                    Move(Direction.Down);
                    return;
                }
                if (posX - 1 >= 0 && board[posX - 1][posY] != Tile.Empty && (posX - 1 == prevX && posY == prevY))
                {
                    Move(Direction.Left);
                    return;
                }
                if (posX + 1 < boardSizeX && board[posX + 1][posY] != Tile.Empty && (posX + 1 == prevX && posY == prevY))
                {
                    Move(Direction.Right);
                    return;
                }
                return;
            }
        }

        }

}
