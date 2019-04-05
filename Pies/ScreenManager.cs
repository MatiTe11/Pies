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
    class ScreenManager
    {
        List<Screen> screens;
        int activeScreen;
        int screenWidth, screenHeight;

        public ScreenManager(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            activeScreen = 0;
            screens.Add(new GameScreen(screenWidth, screenHeight));
        }

        public void LoadContent(ContentManager content)
        {
            foreach(Screen s in screens)
            {
                s.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            screens[activeScreen].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            screens[activeScreen].Draw(spriteBatch);
        }
    }
}
