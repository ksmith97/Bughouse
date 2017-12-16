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
    class Pawn : Piece
    {
        const String white = "WPawn.png";
        const String black = "BPawn.png";

        public Pawn(int x, int y, bool color)
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

        public Pawn(Coord coo, bool color)
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

            moveList.Add(new Coord(myCoord.X, myCoord.Y));

            if (myColor)
            {
                if (myCoord.Y == 6)
                {
                    try
                    {
                        moveList.Add(new Coord(myCoord.X, myCoord.Y - 2));
                    }
                    catch (IndexOutOfRangeException) { }
                }
                try
                {
                    moveList.Add(new Coord(myCoord.X, myCoord.Y - 1));
                }
                catch (IndexOutOfRangeException) { }

                try
                {
                    moveList.Add(new Coord(myCoord.X - 1, myCoord.Y - 1));
                }
                catch (IndexOutOfRangeException) { }

                try
                {
                    moveList.Add(new Coord(myCoord.X + 1, myCoord.Y - 1));
                }
                catch (IndexOutOfRangeException) { }
            }
            else
            {
                if (myCoord.Y == 1)
                {
                    try
                    {
                        moveList.Add(new Coord(myCoord.X, myCoord.Y + 2));
                    }
                    catch (IndexOutOfRangeException) { }
                }
                try
                {
                    moveList.Add(new Coord(myCoord.X, myCoord.Y + 1));
                }
                catch (IndexOutOfRangeException) { }

                try
                {
                    moveList.Add(new Coord(myCoord.X - 1, myCoord.Y + 1));
                }
                catch (IndexOutOfRangeException) { }

                try
                {
                    moveList.Add(new Coord(myCoord.X + 1, myCoord.Y + 1));
                }
                catch (IndexOutOfRangeException) { }
            }

            return moveList;
        }

        public override String ToString()
        {
            return "Pawn " + base.ToString();
        }
    }
}
