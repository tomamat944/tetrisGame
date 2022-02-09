using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.blocks
{
    class BlockSquare : Block
    {
        public BlockSquare(Tuple<int,int> p)
        {
            position_block = p;
            block = new Tile[2, 2]
            {
                {new Tile(Tuple.Create(p.Item1,p.Item2),Enums.Type_of_block.BLOCK), 
                    new Tile(Tuple.Create(p.Item1,p.Item2+1),Enums.Type_of_block.BLOCK) },
                {new Tile(Tuple.Create(p.Item1+1,p.Item2),Enums.Type_of_block.BLOCK), 
                    new Tile(Tuple.Create(p.Item1+1,p.Item2+1),Enums.Type_of_block.BLOCK) }
            };
        }
    }
}
