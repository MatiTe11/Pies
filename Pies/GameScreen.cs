﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pies
{
    class GameScreen : IScreen
    {
        private Player player;
        private Player dog;
        private const int sizeOfBoardX = 10;
        private const int sizeOfBoardY = 10;
        private List<List<Tile>> tiles;
        InputManager inputManager;
        int sizeOfTile;

        public GameScreen()
        {
            tiles = new List<List<Tile>>();
        }

        public void LoadContent()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            inputManager.Update();
            player.Update(gameTime);

            if(inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                if (!player.isMoving() && GetTileNumber(player.PosX) > 1)
                {
                    player.Move(Direction.Left);
                }
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                if (!player.isMoving() && GetTileNumber(player.PosX) < sizeOfBoardX - 1)
                {
                    player.Move(Direction.Right);
                }
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                if (!player.isMoving() && tiles[GetTileNumber(player.PosX), GetTileNumber(player.PosY)] is TileStairs)
                {
                    player.Move(Direction.Up);
                }
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                if (!player.isMoving() && tiles[GetTileNumber(player.PosX), GetTileNumber(player.PosY)] is TileStairs)
                {
                    player.Move(Direction.Up);
                }
            }
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        private int GetTileNumber(int px)
        {
            return px / sizeOfTile;
        }
    }
}
