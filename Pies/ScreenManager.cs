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
    enum ActiveScreen
    {
        start, game
    };

    class ScreenManager
    {
        //List<Screen> screens;
        GameScreen gameScreen;
        StartScreen startScreen;
        int screenWidth, screenHeight;
        ActiveScreen activeScreen;

        public ScreenManager(int screenWidth, int screenHeight)
        {
            //screens = new List<Screen>();
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            activeScreen = ActiveScreen.start;
            gameScreen = new GameScreen(screenWidth, screenHeight);
            startScreen = new StartScreen(screenWidth, screenHeight);
        }

        public void LoadContent(ContentManager content)
        {
            gameScreen.LoadContent(content);
            startScreen.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            //screens[activeScreen].Update(gameTime);

            if(activeScreen == ActiveScreen.game)
            {
                gameScreen.Update(gameTime);
            }
            else if(activeScreen == ActiveScreen.start)
            {
                startScreen.Update(gameTime);
                if(startScreen.FinishedScreen() == 0)
                {
                    activeScreen = ActiveScreen.game;
                    startScreen.Reset();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (activeScreen == ActiveScreen.game)
            {
                gameScreen.Draw(spriteBatch);
            }
            else if (activeScreen == ActiveScreen.start)
            {
                startScreen.Draw(spriteBatch);
            }
        }
    }
}
