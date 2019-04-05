using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pies
{
    class ScreenManager
    {
        List<Screen> screens;
        int activeScreen;
        int screenWidth, screenHeight;

        public ScreenManager(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public void LoadContent()
        {
            foreach(Screen s in screens)
            {
                s.LoadContent();
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
