using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.blocks
{
    public class Block : IRotate, IMove
    {
        
        public Tuple<int, int> position_block;
        public Tile[,] block;

        public Block()
        {
           
        }
        public void LeftRotate()
        {
            int width = block.GetLength(0);
            int height = block.GetLength(1);
            Tile[,] new_block =new Tile[height, width];
            var tempx = block[0, 0].coords.Item1;
            var tempy = block[0, 0].coords.Item2;
            for(int i=0; i<width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    new_block[height - 1 - j, i] = block[i, j];
                    new_block[height - 1 - j, i].coords = Tuple.Create(tempx+height-1-j,tempy+i);
                    if (!Enums.fitBoardSize(new_block[height - 1 - j, i].coords))
                    {
                        return;
                    }
                }
            }
            block = new_block;
        }

        public void RightRotate()
        {
            int width = block.GetLength(0);
            int height = block.GetLength(1);
            Tile[,] new_block = new Tile[height, width];
            var tempx = block[0, 0].coords.Item1;
            var tempy = block[0, 0].coords.Item2;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    new_block[j, width-1-i] = block[i, j];
                    new_block[j, width - 1 - i].coords = Tuple.Create(tempx+j, tempy+width - 1 - i);
                    if (!Enums.fitBoardSize(new_block[j, width - 1 - i].coords))
                    {
                        return;
                    }
                }
            }
            block = new_block;
        }

        public string blockToString()
        {
            int width = block.GetLength(0);
            int height = block.GetLength(1);
            string str = "";
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    str+=block[i,j].type.ToString()+" ";
                }
                str += "\n";
            }
            return str;
        }

        public void moveDown()
        {
            int width = block.GetLength(0);
            int height = block.GetLength(1);
            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    
                        block[i, j].coords = Tuple.Create(block[i, j].coords.Item1 + 1, block[i, j].coords.Item2);
                    
                }
            }
            position_block = Tuple.Create(position_block.Item1 + 1, position_block.Item2);
        }

        public bool collideBlock()
        {
            int width = block.GetLength(0);
            int height = block.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (block[i, j].isCollidedBottom())
                    {
                        
                        return true;
                    }
                }
            }
            return false;
        }
        
        public bool collideBlockWithBoardStates(Tile[,] board)
        {
            int width = block.GetLength(0);
            int height = block.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (block[i, j].coords.Item1 + 1 < Enums.NUMBER_OF_ROWS &&
                        board[block[i, j].coords.Item1 + 1, block[i, j].coords.Item2] != null)
                    {
                        if (block[i, j].type == Enums.Type_of_block.BLOCK &&
                            board[block[i, j].coords.Item1 + 1, block[i, j].coords.Item2].type == 
                                Enums.Type_of_block.BLOCK)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            return false;
        }

        public void moveLeft()
        {
            int width = block.GetLength(0);
            int height = block.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (block[i, j].coords.Item2 - 1 >= 0)
                    {
                        block[i, j].coords = Tuple.Create(block[i, j].coords.Item1, block[i, j].coords.Item2 - 1);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            position_block = Tuple.Create(position_block.Item1, position_block.Item2-1);
        }

        public void moveRight()
        {
            int width = block.GetLength(0);
            int height = block.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (block[i, height - 1].coords.Item2 + 1 >= Convert.ToInt32(Enums.NUMBER_OF_COLS))
                    {
                        return;
                    }
                    else
                    {
                        block[i, j].coords = Tuple.Create(block[i, j].coords.Item1, block[i, j].coords.Item2 + 1);
                    }
                    
                }
            }
            position_block = Tuple.Create(position_block.Item1, position_block.Item2+1);
        }
    }
}
