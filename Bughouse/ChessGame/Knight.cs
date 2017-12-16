using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Bughouse.Utility;
using System.IO;
using System.Windows.Forms;

namespace Bughouse.ChessGame
{
    class Knight : Piece
    {
        const String white = "WKnight.png";
        const String black = "BKnight.png";

        public Knight(int x, int y, bool color)
            : base(x, y, color)
        {
            try
            {
                if (color) myImg = new Bitmap(new Bitmap(white), Chessboard.Area / 8, Chessboard.Area / 8);
                else myImg = new Bitmap(new Bitmap(black), Chessboard.Area / 8, Chessboard.Area / 8);
            }
            catch (IOException)
            {
                MessageBox.Show("Image Not Found: " + white.Substring(1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.Read();
            }
        }

        public Knight(Coord coo, bool color)
            : base(coo, color)
        {
            try
            {
                if (color) myImg = new Bitmap(new Bitmap(white), Chessboard.Area / 8, Chessboard.Area / 8);
                else myImg = new Bitmap(new Bitmap(black), Chessboard.Area / 8, Chessboard.Area / 8);
            }
            catch (IOException)
            {
                MessageBox.Show("Image Not Found: " + white.Substring(1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*
         * Output:  returns a list of potetntial moves from its location
         */

        //Does Not WORK!!!!
        public override List<Coord> move()
        {
            List<Coord> moveList = new List<Coord>();

            moveList.Add(myCoord);

            for (int x = -1; x <= 1; x = x + 2)
            {
                try
                {
                    moveList.Add(new Coord(myCoord.X + 2 * x, myCoord.Y + 1));
                }
                catch (IndexOutOfRangeException) { }
                try
                {
                    moveList.Add(new Coord(myCoord.X + 2 * x, myCoord.Y - 1));
                }
                catch (IndexOutOfRangeException) { }
                try
                {
                    moveList.Add(new Coord(myCoord.X + 1, myCoord.Y + 2 * x));
                }
                catch (IndexOutOfRangeException) { }
                try
                {
                    moveList.Add(new Coord(myCoord.X - 1, myCoord.Y + 2 * x));
                }
                catch (IndexOutOfRangeException) { }

            }
            return moveList;
        }

        public override String ToString()
        {
            return "Knight " + base.ToString();
        }
    }
}
