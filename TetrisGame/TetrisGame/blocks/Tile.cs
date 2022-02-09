using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.blocks
{
    public class Tile
    {
        public Tuple<int, int> coords;
        public Enums.Type_of_block type = new Enums.Type_of_block();
        
        public Tile(Tuple<int,int> c, Enums.Type_of_block t)
        {
            coords = c;
            type = t;
            
        }

        public bool isCollidedBottom()
        {
            if (type==Enums.Type_of_block.BLOCK && coords.Item1+1 >= Convert.ToInt32( Enums.HEIGHT/Enums.TILE))
            {
               
                return true;
            }
            return false;
        }
        
    }
}
