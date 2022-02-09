using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TetrisGame.blocks;

namespace TetrisGame
{
    public class Game
    {
        private int number_of_cols;
        private int number_of_rows;
        public Block current_block;
        public Tile[,] Board_states;
        public GameBoard board;
        DispatcherTimer dispatcher;
        public Game(GameBoard b)
        {
            number_of_cols = Convert.ToInt32(Enums.WIDTH / Enums.TILE);
            number_of_rows = Convert.ToInt32(Enums.HEIGHT / Enums.TILE);
            Board_states = new Tile[number_of_rows, number_of_cols];
            board = b;
            dispatcher = new DispatcherTimer();
            dispatcher.Tick += updateGame;
            dispatcher.Interval = new TimeSpan(0, 0, 0, 0, 100);

        }
        public void restartGame(object sender, RoutedEventArgs args)
        {
            board.removeAll();
            current_block = null;
            Board_states = new Tile[number_of_rows, number_of_cols];
            dispatcher.Start();
        }
        public void startGame(object sender, RoutedEventArgs args)
        {
            dispatcher.Start();
        }
        public void stopGame(object sender, RoutedEventArgs args)
        {
            dispatcher.Stop();
        }
        public void setDirection(KeyEventArgs e)
        {
            if (current_block!=null) 
            {
                if (e.Key == Key.Up)
                {
                    current_block.RightRotate();

                }
                if (e.Key == Key.Down)
                {
                    current_block.LeftRotate();
                }

                if (e.Key == Key.Left)
                {
                    current_block.moveLeft();
                }
                if (e.Key == Key.Right)
                {
                    current_block.moveRight();

                }
        
            }
        }
        private int checkIfDeletionPossible()
        {
            
            for (int i = 0; i < number_of_rows; i++)
            {
                int count = 0;
                for (int j = 0; j < number_of_cols; j++)
                {
                    if (Board_states[i,j]!=null )
                    {
                        count++;
                    }
                    
                }
                if (count == number_of_cols)
                {
                    return i;
                }
            }
            return -1;
        }
        private Block createRandomBlock()
        {
            Random r = new Random();
            Block[] blocks ={new BlockBreak(Tuple.Create(0, 8)),
                new BlockLine(Tuple.Create(0, 8)),
                new BlockSquare(Tuple.Create(0, 8)),
                new BlockBreakR(Tuple.Create(0,8)),
                new BlockT(Tuple.Create(0,8))};
            int c=r.Next(blocks.Length);
            return blocks[c];
        }
        private void updateGame(object sender, EventArgs args)
        {
            if (current_block == null)
            {
                current_block = createRandomBlock();
            }
            if (!current_block.collideBlock() && !current_block.collideBlockWithBoardStates(Board_states))
            {
                current_block.moveDown();
                board.displayBoard(current_block);
            }
            else
            {
                addBlockToBoardStates();
                current_block = createRandomBlock();
            }
            int row = checkIfDeletionPossible();
            if ( row  != -1)
            {
               
                deleteEntireRow(row);
                moveAllElementsAboveRowDown(row);
            }
            if (isGameOver())
            {
                dispatcher.Stop();
                MessageBox.Show("Game Over");
            }
            
            
        }
        private bool isGameOver()
        {
            if (Board_states[2,8]!=null)
            {
                return true;
            }
            return false;
        }
        private void moveAllElementsAboveRowDown(int row)
        {
            for (int i = row-1; i > 0; i--)
            {
                for (int j = 0; j < number_of_cols; j++)
                {
                    if (Board_states[i,j]!=null && board.current_state.ContainsKey(Board_states[i,j]))
                    {
                        Board_states[i, j].coords = Tuple.Create(Board_states[i, j].coords.Item1 + 1,
                            Board_states[i, j].coords.Item2);
                        board.moveTailOnBoard(Board_states[i,j]);

                    }
                }
            }
        }
        private void deleteEntireRow(int row)
        {
            for (int i = 0; i < number_of_cols; i++)
            {
                if (board.current_state.ContainsKey(Board_states[row, i]))
                {
                    board.Children.Remove(board.current_state[Board_states[row, i]]);
                    board.current_state.Remove(Board_states[row, i]);
                    Board_states[row, i] = null;
                }
            }
        }
        private void addBlockToBoardStates()
        {
            int width = current_block.block.GetLength(0);
            int height = current_block.block.GetLength(1);
            for(int i=0; i<width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    if (current_block.block[i, j].type == Enums.Type_of_block.BLOCK)
                    {
                        Board_states[current_block.block[i, j].coords.Item1,
                        current_block.block[i, j].coords.Item2] = current_block.block[i, j];
                    }
                }
            }
        }
    }
}
