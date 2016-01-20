using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class formMain : Form
    {
        gfxSettings engine;
        Board gameBoard;
        Point startPoint;
        int menuWidth = 135;

        public formMain()
        {
            InitializeComponent();
            gameBoard = new Board();
            
            engine = new gfxSettings(panelBoard);
            gameBoard.initBoard(engine, panelBoard, labelScore);
            gameBoard.resetScore();
        }


        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            engine = new gfxSettings(panelBoard); 
            gameBoard.initBoard(engine, panelBoard, labelScore);

        }
        
        private void panelMain_Click(object sender, EventArgs e)
        {
            Point mousePosition = Cursor.Position;

            //adjust from default reference to monitor
            mousePosition = panelBoard.PointToClient(mousePosition);

            gameBoard.detectHit(mousePosition);
        }

        private void FormMain_ResizeBegin(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            startPoint = new Point(control.Size.Width, control.Size.Height);
        }
        
        private void formMain_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            //ensure main form stays proportional
            if (startPoint.Y != control.Size.Height)
                control.Size =
                    new Size(control.Size.Height + menuWidth, control.Size.Height);
            else if (startPoint.X != control.Size.Width)
                control.Size =
                    new Size(control.Size.Width, control.Size.Width - menuWidth);

        }

        // Ensure the panelBoard remains square
        // and PanelMenu will fill out the rest
        private void formMain_ResizeEnd(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            panelBoard.Width = control.Size.Height - 30;
            panelBoard.Height = panelBoard.Width;
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            gameBoard.startNewGame();
            engine.drawBoard();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gameBoard.resetScore();
        }
    }
}
