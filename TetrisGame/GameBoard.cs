using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TetrisGame.blocks;

namespace TetrisGame
{

    public class GameBoard : Canvas
    {

        int tile_size = 20;
        public Dictionary<Tile, Rectangle> current_state;
        public GameBoard()
        {
            current_state = new Dictionary<Tile, Rectangle>();

        }
        
        public void removeAll()
        {
            foreach(var elem in current_state.Keys)
            {
                base.Children.Remove(current_state[elem]);
            }
            current_state = new Dictionary<Tile, Rectangle>();
        }
        public void moveBlockOnBoard(Block block)
        {
            int width = block.block.GetLength(0);
            int height = block.block.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    moveTailOnBoard(block.block[i, j]);
                }
            }
        }
        public void moveTailOnBoard(Tile tile)
        {
            if (tile.type== Enums.Type_of_block.BLOCK && current_state.ContainsKey(tile))
            {
                Canvas.SetLeft(current_state[tile], tile.coords.Item2 * tile_size);
                Canvas.SetTop(current_state[tile], tile.coords.Item1 * tile_size);
            }
        }
        public void drawBlockOnBoard(Block block)
        {
            int width = block.block.GetLength(0);
            int height = block.block.GetLength(1);
            for(int i=0; i<width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    drawTileOnBoard(block.block[i, j]);
                }
            }
            
        }
        private void drawTileOnBoard(Tile tile)
        {
            if (tile.type == Enums.Type_of_block.BLOCK && !current_state.ContainsKey(tile))
            {
                Rectangle rect = new Rectangle();
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Color.FromArgb(255, 0, 0, 255);
                rect.Width = tile_size;
                rect.Height = tile_size;
                rect.Fill = brush;
                SolidColorBrush brush2 = new SolidColorBrush();
                brush2.Color = Color.FromArgb(255, 0, 0, 0);
                rect.Stroke = brush2;

                moveTailOnBoard(tile);
                base.Children.Add(rect);
                current_state.Add(tile, rect);

            }
            
        }
        
        public void displayBoard(Block block)
        {
            drawBlockOnBoard(block);
            moveBlockOnBoard(block);
        }
    }
}
