using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Pies
{
    abstract class  Screen
    {
        protected int screenWidth;
        protected int screenHeight;

        protected InputManager inputManager;

        public Screen(int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            inputManager = new InputManager();
        }

        public abstract void LoadContent(ContentManager Content);
        public abstract void Update(GameTime gameTime);
        public abstract void Reset();
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract int FinishedScreen();
    }
}
