using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.blocks
{
    class BlockBreak : Block
    {
        public BlockBreak(Tuple<int,int> p)
        {
            position_block = p;
            block = new Tile[2, 3]
            {
                {new Tile(Tuple.Create(p.Item1,p.Item2),Enums.Type_of_block.NONE),
                    new Tile(Tuple.Create(p.Item1,p.Item2+1),Enums.Type_of_block.BLOCK),
                    new Tile(Tuple.Create(p.Item1,p.Item2+2),Enums.Type_of_block.BLOCK)},

                {new Tile(Tuple.Create(p.Item1+1,p.Item2),Enums.Type_of_block.BLOCK),
                    new Tile(Tuple.Create(p.Item1+1,p.Item2+1),Enums.Type_of_block.BLOCK),
                    new Tile(Tuple.Create(p.Item1,p.Item2+2),Enums.Type_of_block.NONE) }
            };
        }
    }
}
