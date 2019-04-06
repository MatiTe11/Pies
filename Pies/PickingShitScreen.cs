using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pies
{

    struct Poo
    {
        public float position;
        public bool picked;

        public Poo(float pos)
        {
            position = pos;
            picked = false;
        }

        public Poo(float pos, bool p)
        {
            position = pos;
            picked = p;
        }
    }

    class PickingShitScreen
    {
        Random rand;
        private List<Poo> poos;
        private int shitSize;
        private int screenWidth, screenHeight;
        private Tile currentTile;
        private int currentDoorIndex;
        float speed;
        List<Texture2D> pooTex;
        List<Texture2D> doorsTex;
        Texture2D handTex;
        Texture2D backTex;
        int handPosition;
        int currentFrame;
        int texSize;
        int maxShits;
        InputManager inputManager;

        public PickingShitScreen(int screenW, int screenH, float speed)
        {
            rand = new Random();
            poos = new List<Poo>();
            this.speed = speed;
            screenHeight = screenH;
            screenWidth = screenW;
            shitSize = screenHeight / 7;
            maxShits = screenWidth / shitSize;
            inputManager = new InputManager();
            currentFrame = 0;
        }
        public void LoadContent(List<Texture2D> pooTex, List<Texture2D> doorsTex, Texture2D handTex, Texture2D backTex)
        {
            this.pooTex = pooTex;
            this.doorsTex = doorsTex;
            this.handTex = handTex;
            this.backTex = backTex;
            texSize = pooTex[0].Width;
        }

        public void Reset(Tile tile)
        {
            currentTile = tile;
            switch (tile)
            {
                case Tile.Door1: currentDoorIndex = 0;
                    break;
                case Tile.Door2: currentDoorIndex = 1;
                    break;
                case Tile.Door3: currentDoorIndex = 2;
                    break;

            }
            poos.Clear();
            int num = rand.Next(2, maxShits);
            poos.Add(new Poo((float)((float)num / (float)maxShits)));
            num = rand.Next(2, maxShits);
            poos.Add(new Poo((float)((float)num / (float)maxShits)));
            num = rand.Next(2, maxShits);
            poos.Add(new Poo((float)((float)num / (float)maxShits)));
            handPosition = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backTex, new Vector2(0), null, Color.White, 0f, new Vector2(0, 0), new Vector2((float)screenWidth/(float)backTex.Width, (float)screenHeight / (float)backTex.Height), SpriteEffects.None, 0f);
            spriteBatch.Draw(doorsTex[currentDoorIndex], new Vector2((screenWidth-screenHeight)/2, 0), null, Color.White, 0f, new Vector2(0, 0), new Vector2(screenHeight / texSize), SpriteEffects.None, 0f);
            for (int i = 0; i < poos.Count(); i++)
            {
                if (poos[i].picked)
                    currentFrame = 1;
                else
                    currentFrame = 0;
                spriteBatch.Draw(pooTex[currentFrame], new Vector2(poos[i].position * screenWidth, screenHeight / 2), null, Color.White, 0f, new Vector2(0, 0), new Vector2((float)shitSize / (float)texSize), SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(handTex, new Vector2(handPosition, screenHeight / 2), null, Color.White, 0f, new Vector2(0, 0), new Vector2((float)shitSize / (float) texSize), SpriteEffects.None, 0f);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            inputManager.Update();
            handPosition += (int)(speed * (float)screenWidth * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if(inputManager.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                for (int i = 0; i < poos.Count(); i++)
                {
                    if(CheckCollision(handPosition, (int)(poos[i].position * screenWidth)) && poos[i].picked != true)
                    {
                        poos[i] = new Poo(poos[i].position, true);
                    }
                }
            }
        }

        private bool CheckCollision(int r1, int r2)
        {
            if (r1 + shitSize/2 > r2 && r1 + shitSize / 2  < r2 + shitSize)
                return true;
            else
                return false;
        }

        public int isEnd()
        {
            if (handPosition + shitSize / 2 > screenWidth)
            {
                for (int i = 0; i < poos.Count; i++)
                {
                    if (poos[i].picked != true)
                        return 1; //failed
                }
                return 0; //passed
            }
            else
                return -1; //not end
        }
    }
}
