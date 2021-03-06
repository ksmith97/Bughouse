﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Bughouse.Utility;
using System.IO;
using System.Windows.Forms;

namespace Bughouse.ChessGame
{
    class Rook : Piece
    {
        const String white = "WRook.png";
        const String black = "BRook.png";

        public Rook(int x, int y, bool color)
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

        public Rook(Coord coo, bool color)
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
            //int z = myCoord.X*-1;
            //int y = 7-myCoord.X;

            for (int x = -7; x < 8; x++)
            {
                if (x != 0)
                {
                    try
                    {
                        moveList.Add(new Coord(myCoord.X + x, myCoord.Y));
                    }
                    catch (IndexOutOfRangeException) { }
                }
                try
                {
                    moveList.Add(new Coord(myCoord.X, myCoord.Y + x));
                }
                catch (IndexOutOfRangeException) { }
            }
            return moveList;
        }

        public override String ToString()
        {
            return "Rook " + base.ToString();
        }
    }
}
