using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Bughouse.Utility;
using System.IO;

namespace Bughouse.ChessGame
{
    class King : Piece
    {
        const String white = "Wking.png";
        const String black = "BKing.png";


        public King(int x, int y, bool color)
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
            castle = true;
        }

        public King(Coord coo, bool color)
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
            castle = true;
        }

        public override List<Coord> move()
        {
            List<Coord> moveList = new List<Coord>();
            for (int x = -1; x < 2; x++)
                for (int y = -1; y < 2; y++)
                {
                    try
                    {
                        if(x!=0 && y!=0)moveList.Add(new Coord(myCoord.X + x, myCoord.Y + y));
                    }
                    catch (IndexOutOfRangeException) { }
                }
            return moveList;
        }

        public override String ToString()
        {
            return "King " + base.ToString();
        }
    }
}
