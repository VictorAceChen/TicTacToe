using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    class gfxSettings
    {
        private Graphics gfx;
        private Panel pnlBoard;
        private int playerPadding = 25;
        private int tileWidth;

        public gfxSettings(Panel p)
        {
            gfx = p.CreateGraphics();
            pnlBoard = p;
            drawBoard();
        }

        public void drawBoard()
        {
            Brush backgroundColor = new SolidBrush(Color.White);
            Pen lines = new Pen(Color.Black, 8);

            gfx.FillRectangle(
                backgroundColor
                , new Rectangle(0, 0, pnlBoard.Size.Width, pnlBoard.Size.Height)
                );

            tileWidth = pnlBoard.Size.Width / 3;

            for(int i = 1; i <= 2; i++)
            {
                //Vertical Lines
                gfx.DrawLine(
                    lines,
                    new Point(i * tileWidth, 0),
                    new Point(i * tileWidth, pnlBoard.Size.Height)        
                    );

                //Horizontal lines
                gfx.DrawLine(
                    lines,
                    new Point(0, i * tileWidth - 1),
                    new Point(pnlBoard.Size.Width, i * tileWidth - 1)
                    );
            }
        }

        public void drawPlayerX(Point startPosition)
        {
            Pen penX = new Pen(Color.Red, 8);
            int posX = startPosition.X * tileWidth;
            int posY = startPosition.Y * tileWidth;

            //Line: Top left to bottom right
            gfx.DrawLine(
                penX
                , posX + playerPadding
                , posY + playerPadding
                , posX + tileWidth - playerPadding
                , posY + tileWidth - playerPadding
                );

            //Line: bottom left to Top right
            gfx.DrawLine(
                penX
                , posX + tileWidth - playerPadding
                , posY + playerPadding
                , posX + playerPadding
                , posY + tileWidth - playerPadding
                );
        }

        public void drawPlayerO(Point startPosition)
        {
            Pen penO = new Pen(Color.Blue, 8);
            int posX = startPosition.X * tileWidth;
            int posY = startPosition.Y * tileWidth;

            gfx.DrawEllipse(
                penO
                , posX + playerPadding
                , posY + playerPadding
                , tileWidth - 2 * playerPadding
                , tileWidth - 2 * playerPadding
                );
        }

        public void drawWinRow(Point startPosition, Point endPosition)
        {
            Pen penWin = new Pen(Color.Purple, 4);
            int posX1, posY1, posX2, posY2;

            if (startPosition.X == endPosition.X) //for columns
            {
                posX1 = posX2 = Convert.ToInt32((startPosition.X + 0.5) * tileWidth);
                posY1 = startPosition.Y * tileWidth;
                posY2 = Convert.ToInt32(endPosition.Y * tileWidth * 1.5);
            }
            else if (startPosition.Y == endPosition.Y)  //for rows
            {
                posY1 = posY2 = Convert.ToInt32((startPosition.Y + 0.5) * tileWidth);
                posX1 = startPosition.X * tileWidth;
                posX2 = Convert.ToInt32(endPosition.X * tileWidth * 1.5);
            }
            else if (startPosition.X == endPosition.Y) //diagonal up
            {
                posX1 = posY2 = 0;
                posY1 = Convert.ToInt32(startPosition.Y * tileWidth * 1.5);
                posX2 = Convert.ToInt32(endPosition.X * tileWidth * 1.5);
            }
            else //diagonal down
            {
                posX1 = posY1 = 0;
                posX2 = Convert.ToInt32(endPosition.X * tileWidth * 1.5);
                posY2 = Convert.ToInt32(endPosition.Y * tileWidth * 1.5);
            }

            gfx.DrawLine(
                penWin
                , posX1
                , posY1
                , posX2 
                , posY2
                );
        }

    }
}
