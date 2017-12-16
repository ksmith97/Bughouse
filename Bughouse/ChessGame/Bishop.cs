using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Bughouse.Utility;
using System.Windows.Forms;
using System.IO;

namespace Bughouse.ChessGame
{
    class Bishop : Piece
    {
        const String white = "WBishop.png";
        const String black = "BBishop.png";

        public Bishop(int x, int y, bool color)
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
            }
        }

        public Bishop(Coord coo, bool color)
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



        public override List<Coord> move()
        {
            List<Coord> moveList = new List<Coord>();

            for (int x = -7; x < 8; x++)
            {
                try
                {
                    moveList.Add(new Coord(myCoord.X + x, myCoord.Y + x));
                }
                catch (IndexOutOfRangeException) { }

                if (x != 0)
                {
                    try
                    {
                        moveList.Add(new Coord(myCoord.X + x, myCoord.Y - x));
                    }
                    catch (IndexOutOfRangeException) { }
                }
            }
            return moveList;
        }

        public override String ToString()
        {
            return "Bishop " + base.ToString();
        }
    }
}
