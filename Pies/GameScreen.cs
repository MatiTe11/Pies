using System;
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
        private Tile[,] tiles = new Tile[sizeOfBoardX,sizeOfBoardY];
        InputManager inputManager;
        int sizeOfTile;

        public GameScreen()
        {

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
                player.Move(Direction.Left);
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                if (!player.isMoving() && player.GetTileX())
                {
                    player.Move(Direction.Right);
                }
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                player.Move(Direction.Up);
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                player.Move(Direction.Down);
            }
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
