using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace Pies
{
    class GameScreen : Screen
    {
        private Player player;
        private PickingShitScreen pickingScreen;
        private Dog dog;
        private int sizeOfBoardX = 3;
        private int sizeOfBoardY = 3;

        private float shittingTime = 0.0f;
        private float shitTime = 0.0f;

        private float textureScale;

        private int playerStartingPositionX;
        private int playerStartingPositionY;

        private int firstTailPositionX;
        private int firstTailPositionY;

        private List<List<Tile>> tiles;
        
        private List<Shit> shits;
        private int shitCounter;
        private int shitsCollected = 0;

        int sizeOfTile; //size in pixels
        Texture2D doorWhiteTex, doorRedTex, doorBlueTex, shitTex1, shitTex2, dogL0Tex, dogL2Tex, dogL1Tex, dogP0Tex, dogP1Tex, dogP2Tex, dogSta0Tex, dogSta1Tex, dogSta2Tex, dogSta3Tex, player0Tex, playerLTex, playerPTex, stairsWithDoorsTex, stairsWithWallTex, wallTex;
        SpriteFont font;

        private bool minigameRunning;

        public GameScreen(int screenWidth, int screenHeight) : base(screenWidth,screenHeight)
        {
            
            tiles = new List<List<Tile>>();
            shits = new List<Shit>();

            LoadMap();
            if (screenHeight / sizeOfBoardY >= screenWidth / sizeOfBoardX)
            {
                this.sizeOfTile = (int)(screenWidth / sizeOfBoardX);
            }
            else
            {
                this.sizeOfTile = (int)(screenHeight / sizeOfBoardY);
            }
            firstTailPositionX = (int)((screenWidth-(sizeOfTile * sizeOfBoardX)) / 2);
            firstTailPositionY = (int)((screenHeight-(sizeOfTile * sizeOfBoardY)) / 2);

            playerStartingPositionX = firstTailPositionX;
            playerStartingPositionY = firstTailPositionY;

            pickingScreen = new PickingShitScreen(screenWidth, screenHeight, 0.4f);
            minigameRunning = false;
        }

        override public void LoadContent(ContentManager Content) 
        {
            //texture scale counting
            doorWhiteTex = Content.Load<Texture2D>("DrzwiBiale");
            doorRedTex = Content.Load<Texture2D>("DrzwiCzerwone");
            doorBlueTex = Content.Load<Texture2D>("DrzwiNiebieskie");
            shitTex1 = Content.Load<Texture2D>("Kupsko");
            shitTex2 = Content.Load<Texture2D>("Kupsko2");
            dogL0Tex = Content.Load<Texture2D>("PiesL0");
            dogL2Tex = Content.Load<Texture2D>("PiesL1");
            dogL1Tex = Content.Load<Texture2D>("PiesL2");
            dogP0Tex = Content.Load<Texture2D>("PiesP0");
            dogP1Tex = Content.Load<Texture2D>("PiesP1");
            dogP2Tex = Content.Load<Texture2D>("PiesP2");
            dogSta0Tex = Content.Load<Texture2D>("PiesSra0");
            dogSta1Tex = Content.Load<Texture2D>("PiesSra1");
            dogSta2Tex = Content.Load<Texture2D>("PiesSra2");
            dogSta3Tex = Content.Load<Texture2D>("PiesSra3");
            player0Tex = Content.Load<Texture2D>("Player0");
            playerLTex = Content.Load<Texture2D>("PlayerL");
            playerPTex = Content.Load<Texture2D>("PlayerP");
            font = Content.Load<SpriteFont>("Font");
            stairsWithDoorsTex = Content.Load<Texture2D>("SchodyZDrzwiami");
            stairsWithWallTex = Content.Load<Texture2D>("SchodyZeSciana");
            wallTex = Content.Load<Texture2D>("Sciana");
            List<Texture2D> playerFrames = new List<Texture2D>(){ player0Tex, playerLTex, player0Tex, playerPTex };
            List<Texture2D> dogFrames = new List<Texture2D>() { dogL0Tex, dogL1Tex, dogL2Tex, dogP0Tex, dogP1Tex, dogP2Tex};
            this.textureScale =  (float)this.sizeOfTile / (float)doorWhiteTex.Width;
            dog = new Dog(playerStartingPositionX, playerStartingPositionY, playerStartingPositionX, playerStartingPositionY, 2.0f,sizeOfTile, tiles, shits);
            player = new Player(playerStartingPositionX, playerStartingPositionY, 1.5f, sizeOfTile);
            player.LoadContent(playerFrames);
            dog.LoadContent(dogFrames);

            List<Texture2D> doorsTex = new List<Texture2D>();
            List<Texture2D> pooTex = new List<Texture2D>();

            doorsTex.Add(doorWhiteTex);
            doorsTex.Add(doorRedTex);
            doorsTex.Add(doorBlueTex);
            pooTex.Add(Content.Load<Texture2D>("Zolte"));
            pooTex.Add(Content.Load<Texture2D>("Zielone"));

            pickingScreen.LoadContent(pooTex, doorsTex, Content.Load<Texture2D>("hand"), Content.Load<Texture2D>("background"));
            //pickingScreen.Reset(Tile.Door2);
        }

        override public void Reset()
        {
            throw new NotImplementedException();
        }

        override public void Update(GameTime gameTime)
        {
            if (minigameRunning)
            {
                pickingScreen.Update(gameTime);
                if (pickingScreen.isEnd() > -1)
                    minigameRunning = false;
            }
            else
            {
                shits = dog.Shit;
                inputManager.Update();
                player.Update(gameTime);
                shitCounter = shits.Count();
                dog.IsShitting();
                dog.Update(gameTime, shits);
                Tile currentTile = tiles[GetTileNumberX(player.PosX)][GetTileNumberY(player.PosY)];
                if (shitsCollected >= 5)
                {
                    dog.Speed = 5.0F;
                    player.Speed = 3.0F;
                }
                if (shitsCollected >= 10)
                {
                    dog.Speed = 10.0F;
                    player.Speed = 5.0F;
                }

                if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
                {
                    if (!player.isMoving && GetTileNumberX(player.PosX) > 0 && tiles[GetTileNumberX(player.PosX) - 1][GetTileNumberY(player.PosY)] != Tile.Empty)
                    {
                        player.Move(Direction.Left);
                    }
                }
                else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
                {
                    if (!player.isMoving && GetTileNumberX(player.PosX) < sizeOfBoardX - 1 && tiles[GetTileNumberX(player.PosX) + 1][GetTileNumberY(player.PosY)] != Tile.Empty)
                    {
                        player.Move(Direction.Right);
                    }
                }
                else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
                {
                    if (!player.isMoving && (currentTile == Tile.StairsWall || currentTile == Tile.StairsNoWall) && GetTileNumberY(player.PosY) > 0)
                    {
                        player.Move(Direction.Up);
                    }
                }
                else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
                {
                    if (!player.isMoving && ((currentTile == Tile.StairsWall || currentTile == Tile.StairsNoWall)) && GetTileNumberY(player.PosY) < (sizeOfBoardY - 1))
                    {
                        player.Move(Direction.Down);
                    }
                }
                else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    int pX = ((player.PosX - playerStartingPositionX) / sizeOfTile);
                    int pY = ((player.PosY - playerStartingPositionY) / sizeOfTile);
                    for (int i = 0; i < shits.Count(); i++)
                    {
                        if (pX == shits.ElementAt(i).positionX && pY == shits.ElementAt(i).positionY)
                        {
                            shits.RemoveAt(i);
                            shitsCollected++;
                            if (shitCounter > 5)
                            {
                                pickingScreen.Reset(Tile.Door2);
                                minigameRunning = true;
                            }

                            break;
                        }
                    }
                }
            }
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            if (minigameRunning)
            {
                pickingScreen.Draw(spriteBatch);
            }
            else
            {
                DrawBoard(spriteBatch);
                //DrawPlayer(spriteBatch);
                player.Draw(spriteBatch, textureScale);
                dog.Draw(spriteBatch, textureScale, sizeOfTile);
                DrawCounter(spriteBatch);
            }
        }

        private int GetTileNumberX(int px)
        {
            return (px-firstTailPositionX+sizeOfTile/2) / sizeOfTile;
        }
        private int GetTileNumberY(int py)
        {
            return (py - firstTailPositionY+sizeOfTile / 2) / sizeOfTile;
        }

        private void DrawPlayer(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(player0Tex, new Vector2(player.PosX,player.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
            spriteBatch.End();
        }
        private void DrawDog(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(dogL0Tex, new Vector2(dog.PosX+sizeOfTile, dog.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
            spriteBatch.End();
        }
        private void DrawCounter(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Shits on the screen: " + shitCounter.ToString(), new Vector2(20, 50), Color.Red);
            spriteBatch.DrawString(font, "Shits collected: " + shitsCollected.ToString(), new Vector2(20, 100), Color.Red);
            spriteBatch.End();
        }

        private void DrawBoard(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int i = 0; i < sizeOfBoardX; i++)
            {
                for (int j = 0; j < sizeOfBoardY; j++)
                {
                    if(tiles[i][j] == Tile.Door1)
                        spriteBatch.Draw(doorWhiteTex, new Vector2(firstTailPositionX + i * sizeOfTile, firstTailPositionY + j * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    else if(tiles[i][j] == Tile.Door2)
                    {
                        spriteBatch.Draw(doorRedTex, new Vector2(firstTailPositionX + i * sizeOfTile, firstTailPositionY + j * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    }
                    else if (tiles[i][j] == Tile.Door3)
                    {
                        spriteBatch.Draw(doorBlueTex, new Vector2(firstTailPositionX + i * sizeOfTile, firstTailPositionY + j * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    }
                    else if (tiles[i][j] == Tile.Empty)
                    {
                        spriteBatch.Draw(wallTex, new Vector2(firstTailPositionX + i * sizeOfTile, firstTailPositionY + j * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    }
                    else if (tiles[i][j] == Tile.StairsNoWall)
                    {
                        spriteBatch.Draw(stairsWithDoorsTex, new Vector2(firstTailPositionX + i * sizeOfTile, firstTailPositionY + j * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    }
                    else if (tiles[i][j] == Tile.StairsWall)
                    {
                        spriteBatch.Draw(stairsWithWallTex, new Vector2(firstTailPositionX + i * sizeOfTile, firstTailPositionY + j * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    }
                }

                if (dog.IsShitting())
                {
                    shittingTime += 1.0f;
                    if (shittingTime <= 15.0f)
                        spriteBatch.Draw(dogSta0Tex, new Vector2(dog.PosX + sizeOfTile, dog.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    else if (shittingTime <= 30.0f)
                        spriteBatch.Draw(dogSta1Tex, new Vector2(dog.PosX + sizeOfTile, dog.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    else if (shittingTime <= 45.0f)
                        spriteBatch.Draw(dogSta2Tex, new Vector2(dog.PosX + sizeOfTile, dog.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    else if (shittingTime <= 60.0f)
                        spriteBatch.Draw(dogSta3Tex, new Vector2(dog.PosX + sizeOfTile, dog.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    else
                        shittingTime = 0.0f;
                }
                  
                foreach (var shit in shits)
                {
                    shitTime += 1f;
                    if (shitTime < 60f*shits.Count)
                    {                
                        spriteBatch.Draw(shitTex1, new Vector2(shit.positionX*sizeOfTile + playerStartingPositionX, shit.positionY * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(shitTex2, new Vector2(shit.positionX * sizeOfTile + playerStartingPositionX, shit.positionY * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                        if (shitTime == 120f*shits.Count) shitTime = 0.0f;
                    }
                }
            
                
            }
            spriteBatch.End();
        }

        private void LoadMap()
        {
            using (TextReader reader = File.OpenText("Map.txt"))
            {
                string dataString;
                sizeOfBoardX = int.Parse(reader.ReadLine());
                sizeOfBoardY = int.Parse(reader.ReadLine());
                for (int i = 0; i < sizeOfBoardX; i++)
                {
                    tiles.Add(new List<Tile>());
                }
                foreach (var elem in tiles)
                {
                    for (int j = 0; j < sizeOfBoardY; j++)
                    {
                        elem.Add(Tile.Empty);
                    }
                }
                for (int j = 0; j < sizeOfBoardY; j++)
                {
                    dataString = reader.ReadLine();
                    for (int i = 0; i < sizeOfBoardX; i++)
                    {
                        if (dataString[i] == 'e')
                        {
                            tiles[i][j]=Tile.Empty;
                        }
                        else if (dataString[i] == 's')
                        {
                            tiles[i][j] = Tile.StairsWall;
                        }
                        else if (dataString[i] == 'n')
                        {
                            tiles[i][j] = Tile.StairsNoWall;
                        }
                        else if (dataString[i] == '1')
                        {
                            tiles[i][j] = Tile.Door1;
                        }
                        else if (dataString[i] == '2')
                        {
                            tiles[i][j] = Tile.Door2;
                        }
                        else if (dataString[i] == '3')
                        {
                            tiles[i][j] = Tile.Door3;
                        }
                    }
                }
            }
        }



        public override int FinishedScreen()
        {
            return -1;
        }
    }
}
