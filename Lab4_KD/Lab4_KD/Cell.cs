using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab4_KD
{
    public class Cell
    {
        // Row and Column values for each Cell
        public int row;
        public int col;

        // Cell Color
        public Brush color;

        // Flag to see if the cell is safe or not (default true)
        public bool isSafe = true;

        // Managing Queen Stuff
        public bool hasQueen = false;
        public Brush queenColor;

        // Rect to represent each cell (x,y,width,height)
        public Rectangle rect;

        //Default Constructor
        public Cell(Rectangle currRect, int currRow, int currCol, int currColor)
        {
            // Set cell's rect
            this.rect = currRect;

            // Set cell color
            if (currColor == 0)
            {
                this.color = Brushes.White;
                queenColor = Brushes.Black;
            }
            else
            {
                this.color = Brushes.Black;
                queenColor = Brushes.White;
            }

            // Set row and column
            this.row = currRow;
            this.col = currCol;
            
        }
    }
}
