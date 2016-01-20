using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    class Board
    {
        Panel p;
        gfxSettings g;
        Label l;
        
        int tileWidth;

        Score scoreBoard = new Score();

        private int turnCounter = 0;
        
        private const bool playerX = true;
        private const bool playerO = false;

        private bool?[,] placements = new bool?[3,3];

        public void initBoard(gfxSettings mainGFX, Panel panelBoard, Label labelScore)
        {
            p = panelBoard;
            g = mainGFX;
            l = labelScore;

            tileWidth = p.Size.Width / 3;

            refill();
        }

        public void detectHit(Point location)
        {
            int row = toBoardPosition(location.X);
            int column = toBoardPosition(location.Y);

            //to prevent spots from being reused
            if (placements[row, column] != null
                || turnCounter > 9)
                return;

            turnCounter++;
            Action<Point> drawPlayer;

            if (turnCounter % 2 == 0)
            {
                drawPlayer = g.drawPlayerX;
                placements[row, column] = playerX;
            }
            else
            {
                drawPlayer = g.drawPlayerO; 
                placements[row, column] = playerO; 
            }

            assignPlayerMarkers(drawPlayer, new Point(row, column));

            if (checkWinner())
            {

                if (turnCounter % 2 == 0)
                    scoreBoard.xWins();
                else
                    scoreBoard.oWins();

                l.Text = scoreBoard.displayScore();

                turnCounter = 10; //to prevent further moves
            }

        }


        public void assignPlayerMarkers(Action<Point> drawPlayer , Point location)
        {
           drawPlayer(location);
        }

        private bool checkWinner()
        {
            bool? boolValue = placements[1, 1];

            //check for diagonals
            if (boolValue != null)
                //top to bottom
                if (boolValue == placements[0, 0]
                    && boolValue == placements[2, 2])
                {
                    g.drawWinRow( new Point(0, 0), new Point(2, 2));
                    return true;
                }
                //bottom to top
                else if (boolValue == placements[2, 0]
                    && boolValue == placements[0, 2])
                {
                    g.drawWinRow( new Point(0, 2), new Point(2, 0));
                    return true;
                }

            for (int x = 0; x < 3; x++)
            {
                //check rows
                boolValue = placements[x, 0];

                if (boolValue != null
                    && boolValue == placements[x, 1]
                    && boolValue == placements[x, 2])
                {
                    g.drawWinRow(new Point(x, 0), new Point(x, 2));
                    return true;
                }

                //check columns
                boolValue = placements[0, x];

                if (boolValue != null
                    && boolValue == placements[1, x]
                    && boolValue == placements[2, x])
                {
                    g.drawWinRow(new Point(0, x), new Point(2, x));
                    return true;
                }
            }
            
            if (turnCounter == 9)
            {
                MessageBox.Show("Game Tied");
                turnCounter++;
            }

            return false;
        }
        
        public void startNewGame()
        {
            placements = new bool?[3, 3];
            turnCounter = 0;
        }

        public void resetScore()
        {
            scoreBoard.reset();
            l.Text = scoreBoard.displayScore();
        }

        public int toBoardPosition(int mousePosition)
        {
            if (mousePosition < tileWidth)
                return 0;
            else if (mousePosition > tileWidth
                     && mousePosition < tileWidth * 2)
                return 1;
            else
                return 2;
        }


        public void refill()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bool? isPlayerX = placements[i, j];

                    if (isPlayerX != null)
                    {
                        Point currentPosition = new Point(i, j);
                        if ((bool)isPlayerX)
                            g.drawPlayerX(currentPosition);
                        else
                            g.drawPlayerO(currentPosition);

                        checkWinner();
                    }
                }
            }
        }
         
    }
}
