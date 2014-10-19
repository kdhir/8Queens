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
            foreach (Cell c in boardCells)
            {
                // Fill each cell's rectangle first
                g.FillRectangle(c.color, c.rect);
                // Then create a boarder around the board
                g.DrawRectangle(Pens.Black, c.rect);
                
                Font queenFont = new Font("Arial", 30, FontStyle.Bold);
                
                //Drawing Queens
                if (c.hasQueen == true)
                {
                    g.DrawString("Q", queenFont, c.queenColor, c.rect.Location);
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Click Point
                foreach (Cell c in this.boardCells){
                    int xRange = c.rect.X + 50;
                    int yRange = c.rect.Y + 50;
                    // if click is in this cell
                    if (((c.rect.X < e.X) && (e.X < xRange)) && ((c.rect.Y < e.Y) && (e.Y < yRange)))
                    {
                        if (c.isSafe == true)
                        {
                            c.hasQueen = true;
                            c.isSafe = false;

                            // update number of queens
                            numberOfQueens++;
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
        }

    }
}
