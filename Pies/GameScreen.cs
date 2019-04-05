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
        private Player dog;
        private const int sizeOfBoardX = 10;
        private const int sizeOfBoardY = 10;

        private int firstTailPositionX;
        private int firstTailPositionY;

        private List<List<Tile>> tiles;
        InputManager inputManager;
        int sizeOfTile;
        Texture2D playerTex, doorTex, elevatorTex;

        public GameScreen(int screenWidth, int screenHeight) : base(screenWidth,screenHeight)
        {
            inputManager = new InputManager();
            tiles = new List<List<Tile>>();


        }

        override public void LoadContent(ContentManager Content) 
        {
            playerTex = Content.Load<Texture2D>("player");
            elevatorTex = Content.Load<Texture2D>("elevator");
            doorTex = Content.Load<Texture2D>("door");
        }

        override public void Reset()
        {
            throw new NotImplementedException();
        }

        override public void Update(GameTime gameTime)
        {
            inputManager.Update();
            player.Update(gameTime);

            if(inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                if (!player.isMoving && GetTileNumber(player.PosX) > 1)
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
                if (!player.isMoving && tiles[GetTileNumber(player.PosX)][ GetTileNumber(player.PosY)] is TileStairs)
                {
                    player.Move(Direction.Up);
                }
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                if (!player.isMoving && tiles[GetTileNumber(player.PosX)][ GetTileNumber(player.PosY)] is TileStairs)
                {
                    player.Move(Direction.Up);
                }
            }
            throw new NotImplementedException();
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        private int GetTileNumber(int px)
        {
            return px / sizeOfTile;
        }

        private void DrawPlayer(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playerTex, position, null, Color.White, angle, new Vector2(bathTex2.Width / 2, bathTex2.Height / 2), scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
