using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.blocks
{
    static public class Enums
    {
        static public int WIDTH = 450;
        static public int HEIGHT = 600;
        static public int TILE = 20;
        static public int NUMBER_OF_COLS = Convert.ToInt32( WIDTH / TILE);
        static public int NUMBER_OF_ROWS = Convert.ToInt32(HEIGHT / TILE);
        public enum Type_of_block
        {
            NONE,
            BLOCK
        }
        static public bool fitBoardSize(Tuple<int,int> pos)
        {
            if(pos.Item1>=0 && pos.Item1<NUMBER_OF_ROWS && pos.Item2>0 && pos.Item2<=NUMBER_OF_COLS)
            {
                return true;
            }
            return false;
        }
    }
}
