using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pies

{
    class Player
    {
        List<int> tailList = new List<int>();
        protected int playerPositionX;
        protected int playerPositionY;

        public Player() {}

        public Player(int x, int y)
        {
            playerPositionX = x;
            playerPositionY = y;
        }
        
    }
}
