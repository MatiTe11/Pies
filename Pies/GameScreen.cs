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
        private Dog dog;
        private int sizeOfBoardX = 3;
        private int sizeOfBoardY = 3;

        private float textureScale;

        private int playerStartingPositionX;
        private int playerStartingPositionY;

        private int firstTailPositionX;
        private int firstTailPositionY;

        private List<List<Tile>> tiles;
        
        private List<Shit> shits;
        int sizeOfTile; //size in pixels
        Texture2D doorWhiteTex, doorRedTex, doorBlueTex, shitTex1, shitTex2, dogL0Tex, dogL2Tex, dogL1Tex, dogP0Tex, dogP1Tex, dogP2Tex, dogSta0Tex, dogSta1Tex, dogSta2Tex, dogSta3Tex, player0Tex, playerLTex, playerPTex, stairsWithDoorsTex, stairsWithWallTex, wallTex;

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
            stairsWithDoorsTex = Content.Load<Texture2D>("SchodyZDrzwiami");
            stairsWithWallTex = Content.Load<Texture2D>("SchodyZeSciana");
            wallTex = Content.Load<Texture2D>("Sciana");
            List<Texture2D> playerFrames = new List<Texture2D>(){ player0Tex, playerLTex, player0Tex, playerPTex };

            this.textureScale =  (float)this.sizeOfTile / (float)doorWhiteTex.Width;
            dog = new Dog(1,1,0.5f,sizeOfTile, tiles, shits);
            player = new Player(playerStartingPositionX, playerStartingPositionY, 1.5f, sizeOfTile);
            player.LoadContent(playerFrames);

        }

        override public void Reset()
        {
            throw new NotImplementedException();
        }

        override public void Update(GameTime gameTime)
        {
            inputManager.Update();
            player.Update(gameTime);
            dog.Update(gameTime, shits);
            dog.IsShitting();
            shits = dog.Shit;
            Tile currentTile = tiles[GetTileNumberX(player.PosX)][GetTileNumberY(player.PosY)];

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
                if (!player.isMoving && ((currentTile == Tile.StairsWall || currentTile == Tile.StairsNoWall)) && GetTileNumberY(player.PosY) < (sizeOfBoardY-1))
                {
                    player.Move(Direction.Down);
                }
            }
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            DrawBoard(spriteBatch);
            //DrawPlayer(spriteBatch);
            player.Draw(spriteBatch, textureScale);
            DrawDog(spriteBatch);
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
            spriteBatch.Draw(dogL0Tex, new Vector2(dog.PosX, dog.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
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
