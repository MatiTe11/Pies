using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pies
{
    class GameScreen : Screen
    {
        private Player player;
        private Dog dog;
        private const int sizeOfBoardX = 3;
        private const int sizeOfBoardY = 3;
        int graficzkaPsa = 0;

        private float textureScale;

        private int firstTailPositionX;
        private int firstTailPositionY;

        private List<List<Tile>> tiles;
        InputManager inputManager;
        int sizeOfTile; //size in pixels
        Texture2D playerTex, doorTex, elevatorTex, whiteDoor, emptyTex, dogTex, dogTex1, dogTex2;

        public GameScreen(int screenWidth, int screenHeight) : base(screenWidth,screenHeight)
        {
            
            inputManager = new InputManager();
            tiles = new List<List<Tile>>();

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

            LoadMap();
        }

        override public void LoadContent(ContentManager Content) 
        {
            //whiteDoor = Content.Load<Texture2D>("Graphic/DrzwiBiale");
            //texture scale counting
            

            playerTex = Content.Load<Texture2D>("player");
            elevatorTex = Content.Load<Texture2D>("elevator");
            doorTex = Content.Load<Texture2D>("doors");
            emptyTex = Content.Load<Texture2D>("empty");
            dogTex = Content.Load<Texture2D>("PiesP0");
            dogTex1 = Content.Load<Texture2D>("PiesL1");
            dogTex2 = Content.Load<Texture2D>("PiesP2");
            this.textureScale =  (float)this.sizeOfTile / (float)emptyTex.Width;
            player = new Player(10, 10, 0.5f, sizeOfTile);
            dog = new Dog(10, 10, 0.5f, sizeOfTile, tiles, new List<Shit>());
        }

        override public void Reset()
        {
            throw new NotImplementedException();
        }

        override public void Update(GameTime gameTime)
        {
            inputManager.Update();
            player.Update(gameTime);
            dog.Update(gameTime, new List<Shit>());

            if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                if (!player.isMoving && GetTileNumber(player.PosX) > 0)
                {
                    player.Move(Direction.Left);
                }
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                if (!player.isMoving && GetTileNumber(player.PosX) < sizeOfBoardX - 1)
                {
                    player.Move(Direction.Right);
                }
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                if (!player.isMoving && tiles[GetTileNumber(player.PosX)][GetTileNumber(player.PosY)] is TileStairs && GetTileNumber(player.PosY) > 0)
                {
                    player.Move(Direction.Up);
                }
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                if (!player.isMoving && tiles[GetTileNumber(player.PosX)][ GetTileNumber(player.PosY)] is TileStairs && GetTileNumber(player.PosY) < (sizeOfBoardY-1))
                {
                    player.Move(Direction.Down);
                }
            }
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            DrawBoard(spriteBatch);
            DrawPlayer(spriteBatch);
            DrawDog(spriteBatch);
        }

        private int GetTileNumber(int px)
        {
            return px / sizeOfTile;
        }

        private void DrawPlayer(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playerTex, new Vector2(player.PosX,player.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
            spriteBatch.End();
        }
        private void DrawDog(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if(graficzkaPsa % 3 == 0)
                spriteBatch.Draw(dogTex, new Vector2(dog.PosX, dog.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
            if (graficzkaPsa % 3 == 1)
                spriteBatch.Draw(dogTex1, new Vector2(dog.PosX, dog.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
            if (graficzkaPsa % 3 == 2)
                spriteBatch.Draw(dogTex2, new Vector2(dog.PosX, dog.PosY), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);

            graficzkaPsa++;


            spriteBatch.End();
        }

        private void DrawBoard(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int i = 0; i < sizeOfBoardX; i++)
            {
                for (int j = 0; j < sizeOfBoardY; j++)
                {
                    if (tiles[i][j] is TileDoors)
                        spriteBatch.Draw(doorTex, new Vector2(firstTailPositionX + i * sizeOfTile, firstTailPositionY + j * sizeOfTile), null, Color.White, 0f, new Vector2(0,0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    else if (tiles[i][j] is TileStairs)
                        spriteBatch.Draw(elevatorTex, new Vector2(firstTailPositionX + i * sizeOfTile, firstTailPositionY + j * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                    else if (tiles[i][j] is TileEmpty)
                        spriteBatch.Draw(emptyTex, new Vector2(firstTailPositionX + i * sizeOfTile, firstTailPositionY + j * sizeOfTile), null, Color.White, 0f, new Vector2(0, 0), new Vector2(textureScale), SpriteEffects.None, 0f);
                }
            }
            spriteBatch.End();
        }

        private void LoadMap()
        {
            for (int i = 0; i < 10; i++)
            {
                tiles.Add(new List<Tile>());
            }

            foreach (var elem in tiles)
            {
                for (int j = 0; j < 10; j++)
                {
                    elem.Add(new TileStairs(0, 0, 0));
                }
                
            }
            tiles[1][1] = new TileDoors(0, 0, 0);

        }
    }
}
