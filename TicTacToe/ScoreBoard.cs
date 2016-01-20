using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    class Score
    {
        private int xScore = 0;
        private int oScore = 0;

        public void xWins()
        {
            xScore++;
        }

        public void oWins()
        {
            oScore++;
        }
        public string displayScore()
        {
            return 
                "SCORE"
                +"\nX : " + xScore
                +"\nO : " + oScore;
        }
        public void reset()
        {
            xScore = oScore = 0;
        }
    }

}
