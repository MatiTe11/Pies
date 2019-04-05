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
        private Tile[,] tiles = new Tile[10,10];
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
            player.Update(gameTime);
            if(inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                player.Move(Direction.Left);
            }
            else if (inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                player.Move(Direction.Right);
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
