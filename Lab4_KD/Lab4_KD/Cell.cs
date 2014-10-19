using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab4_KD
{
    class Cell
    {
        // Row and Column values for each Cell
        public int row;
        public int col;

        // Cell Color
        public Brush color;

        // Flag for wheather the cell is safe or not (default true)
        public bool safe = true;

        // Rect to represent each cell (x,y,width,height)
        public Rectangle rect;

        //Default Constructor
        public Cell(Rectangle currRect, int currRow, int currCol, Brush currColor)
        {
            // Set cell's rect
            this.rect = currRect;

            // Set cell color
            this.color = currColor;

            // Set row and column
            this.row = currRow;
            this.col = currCol;
            
        }
    }
}
