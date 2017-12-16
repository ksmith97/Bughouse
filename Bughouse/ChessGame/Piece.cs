using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Bughouse.Utility;

namespace Bughouse.ChessGame
{

        abstract class Piece
        {
            protected Bitmap myImg = null;//<--hard code image into sub class
            protected Coord myCoord;

            public Coord Coord
            {
                get { return myCoord;  }
                set { myCoord = value; }
            }

            protected bool castle;

            public bool Castle
            {
                get { return castle; }
                set { castle = value; }
            }

            private int myDrawX, myDrawY;
            protected bool myColor;

            public bool Color
            {
                get { return myColor; }
            }
            public abstract List<Coord> move();

            //Constructs a white piece
            public Piece()
            {
                myColor = true;
            }

            /*
             * The Basic Full constructor 
             * Input: The color of the Piece
             * Output: 
             */
            public Piece(int x, int y, bool col)
            {
                myColor = col;
                myCoord = new Coord(x, y);
                CalcDraw(x, y);
            }

            public Piece(Coord coo, bool color)
            {
                myColor = color;
                myCoord = coo;
                CalcDraw(coo.X, coo.Y);
            }

            protected void CalcDraw(int x, int y)
            {
                myDrawX = (x * Chessboard.Area / 8);
                myDrawY = (y * Chessboard.Area / 8);
            }
            public void FreeDraw(int x, int y)
            {
                myDrawX = x-20;
                myDrawY = y-20;
            }

            public void DrawImage(Graphics g)
            {
                g.DrawImage(myImg, myDrawX, myDrawY);
            }

            public override String ToString()
            {
                return "Piece at loc: " + myCoord;
            }

            public override bool Equals(Object obj)
            {
                Piece comp = (Piece)obj;
                if (myColor != comp.Color) return false;
                if (myCoord != comp.Coord) return false;
                return true;
            }

            public void ChangePos(Coord coo)
            {
                myCoord = coo;
                CalcDraw(coo.X, coo.Y);
            }

            internal void ResetDraw()
            {
                CalcDraw(myCoord.X, myCoord.Y);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

}
