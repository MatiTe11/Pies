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

        List<Texture2D> textures;
        int currentFrame;
        float totalTime;

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
            currentFrame = 0;
            totalTime = 0;
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
        public void LoadContent(List<Texture2D> textures)
        {
            this.textures = textures;
        }
        public void Update(GameTime gameTime)
        {
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
                        this.playerPositionX += (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionX -= (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionX < 0)
                        {
                            this.playerPositionX += this.changePositionX;
                            this.changePositionX = 0;
                        }
                    }
                    else //move left
                    {
                        this.playerPositionX -= (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionX -= (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionX < 0)
                        {
                            this.playerPositionX -= this.changePositionX;
                            this.changePositionX = 0;
                        }
                    }
                }
                else if (this.changePositionY > 0)
                {
                    if (this.downRight) //move down
                    {
                        this.playerPositionY += (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionY -= (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionY < 0)
                        {
                            this.playerPositionY += this.changePositionY;
                            this.changePositionY = 0;
                        }
                    }
                    else //move up
                    {
                        this.playerPositionY -= (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        this.changePositionY -= (int)(playerSpeed * sizeOfTile * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        if (changePositionY < 0)
                        {
                            this.playerPositionY -= this.changePositionY;
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
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(textures[currentFrame], new Vector2(PosX, PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(scale), SpriteEffects.None, 0f);
            spriteBatch.End();
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

        public float Speed
        {
            get { return playerSpeed; }
            set { playerSpeed = value; }
        }

    }
}
