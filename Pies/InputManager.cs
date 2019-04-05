using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Pies
{
    class InputManager
    {
        private KeyboardState keyboardState;

        public bool isKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();
        }
    }
}
