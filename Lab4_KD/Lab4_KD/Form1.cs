using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_KD
{
    public partial class Form1 : Form
    {
        // constants for chess board
        const int BOARDSIZE = 400; // 8 cells each way
        const int BOARDPOSITION = 100;
        const int CELLSIZE = 50;

        // List of cell for board
        private List<Cell> boardCells = new List<Cell>();

        // List of queens on board
        
        // Flag for Hints (default false)
        private bool hintsEnabled = false;

        // Count Queens
        public int numberOfQueens;
       

        public Form1()
        {
            InitializeComponent();
            //Hint color 
            int colorNumb;
            // Title
            this.Text = @"Eight Queens by Kanav Dhir";

            // number of queens text
            label1.Text = String.Format("You have {0} queens on the board",numberOfQueens);

            // Create cells on board
            for (int col = 0; col < (BOARDSIZE / CELLSIZE); col++)
            {
                for (int row = 0; row < (BOARDSIZE / CELLSIZE); row++)
                {
                    // Create Rect
                    Rectangle rect = new Rectangle(BOARDPOSITION + col * CELLSIZE, BOARDPOSITION + row * CELLSIZE, CELLSIZE, CELLSIZE);

                    // Create Cell           (rect,row,col,color)
                    Cell boardCell = new Cell(rect,row,col,(row+col)%2); // Color is an 0 or 1 depending on even or odd cell, setting color is handeled in cell class

                    // Add cells to list
                    this.boardCells.Add(boardCell);
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // check to update all safe and unsafe cells after queen deletion
            foreach (Cell c in boardCells)
            {
                if(c.hasQueen) updateSafety(c, false);
            }
            foreach (Cell c in boardCells)
            {
                // Fill each cell's rectangle first
                if (hintsEnabled && !c.isSafe)
                {
                    g.FillRectangle(Brushes.Red, c.rect);
                }
                else
                {
                    g.FillRectangle(c.color, c.rect);
                }
                // Then create a boarder around the board
                g.DrawRectangle(Pens.Black, c.rect);

                Font queenFont = new Font("Arial", 30, FontStyle.Bold);
                
                //Drawing Queens
                if (c.hasQueen == true)
                {
                    if (hintsEnabled)
                    {
                        g.DrawString("Q", queenFont, Brushes.Black, c.rect.Location); // text black if hints
                    }
                    else
                    {
                        g.DrawString("Q", queenFont, c.queenColor, c.rect.Location); // otherwise 
                    }
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // Left Click
            if (e.Button == MouseButtons.Left)
            {
                // Click Point
                foreach (Cell c in this.boardCells){
                    int xRange = c.rect.X + 50;
                    int yRange = c.rect.Y + 50;
                    // if click is in this cell
                    if (((c.rect.X < e.X) && (e.X < xRange)) && ((c.rect.Y < e.Y) && (e.Y < yRange)))
                    {
                        // if the cell is safe then add queen and set as unsafe
                        if (c.isSafe == true)
                        {
                            c.hasQueen = true;
                            c.isSafe = false;

                            // update number of queens
                            numberOfQueens++;
                            label1.Text = String.Format("You have {0} queens on the board", numberOfQueens);
                        }
                        // otherwise make a sound
                        else
                        {
                            //Beep sound
                            System.Media.SystemSounds.Beep.Play();
                        }

                    }
                }
                this.Invalidate();
            }

            // Right Click
            if (e.Button == MouseButtons.Right)
            {
                // Click Point
                foreach (Cell c in this.boardCells)
                {
                    int xRange = c.rect.X + 50;
                    int yRange = c.rect.Y + 50;
                    // if click is in this cell
                    if (((c.rect.X < e.X) && (e.X < xRange)) && ((c.rect.Y < e.Y) && (e.Y < yRange)))
                    {
                        // if cell has a queen, then remove queen and set as safe
                        if (c.hasQueen == true)
                        {
                            c.hasQueen = false;
                            c.isSafe = true;

                            //update safety (cell, set to unsafe)
                            updateSafety(c, true);

                            // update number of queens
                            numberOfQueens--;
                            label1.Text = String.Format("You have {0} queens on the board", numberOfQueens);
                        }
                        else
                        {
                            //Beep sound
                            System.Media.SystemSounds.Beep.Play();
                        }

                    }
                }
                this.Invalidate();
            }
            if (numberOfQueens == 8)
            {
                // Show winning messages
                MessageBox.Show(@"You did it!");
            }
        }
        public void updateSafety(Cell currCell, bool setSafe)
        {
            foreach (Cell cell in this.boardCells)
            {
                // check rows and columns
                if ((currCell.rect.X == cell.rect.X) || (currCell.rect.Y == cell.rect.Y))
                {
                    if (setSafe == false)
                    {
                        cell.isSafe = false;
                    }
                    else
                    {
                        cell.isSafe = true;
                    }
                }
                // check diagonals
                int deltay = Math.Abs(cell.rect.Y - currCell.rect.Y);
                int deltax = Math.Abs(cell.rect.X - currCell.rect.X);
                if (deltax == deltay)
                {
                    if (setSafe == false)
                    {
                        cell.isSafe = false;
                    }
                    else
                    {
                        cell.isSafe = true;
                    }
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            foreach (Cell c in this.boardCells)
            {
                // set every cell as safe and make none of them have queens
                c.isSafe = true;
                c.hasQueen = false;
                // update number of queens 
                numberOfQueens = 0;
                label1.Text = String.Format("You have {0} queens on the board", numberOfQueens);
            }

            this.Invalidate();
        }

        private void hint_CheckedChanged(object sender, EventArgs e)
        {
            // if false, set hits to true (check)
            if (!hintsEnabled)
            {
                hintsEnabled = true;
            }

            // if true, set hits to flase (uncheck)
            else
            {
                hintsEnabled = false;
            }

            this.Invalidate();
        }

    }
}
