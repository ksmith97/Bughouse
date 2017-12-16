using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bughouse.Utility;

namespace Bughouse.ChessGame
{
    public partial class Chessboard : Panel
    {
        public static int Area = 320;
        private Dictionary<Coord, Piece> myPieces;

        private Coord myKing;
        
        public static int Debug = 1;

        bool turn;

        public bool Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        bool player;  //True = white

        //Piece that user has currently selected
        private Piece selected = null;

        public Chessboard(bool initTurn)
        {
            turn = initTurn;
            player = initTurn;

            InitializeComponent();
            myPieces = new Dictionary<Coord, Piece>();

            Populate();
            Invalidate();
        }


        /**
         * Called by Constructor
         * Populates the chess board
         * Uses standard deployment at this time
         * Maybe a subclass could inherite and override
         *  for a bughouse version
         * note: bughouse version would need a different 2-D array, override
         */
        public void Populate()
        {
            for (int x = 0; x < 8; x++)
            {
                myPieces.Add(new Coord(x, 1), new Pawn(x, 1, false));
                myPieces.Add(new Coord(x, 6), new Pawn(x, 6, true));
            }
            myPieces.Add(new Coord(0, 0), new Rook(0, 0, false));
            myPieces.Add(new Coord(7, 0), new Rook(7, 0, false));
            myPieces.Add(new Coord(0, 7), new Rook(0, 7, true));
            myPieces.Add(new Coord(7, 7), new Rook(7, 7, true));

            myPieces.Add(new Coord(1, 0), new Knight(1, 0, false));
            myPieces.Add(new Coord(6, 0), new Knight(6, 0, false));
            myPieces.Add(new Coord(1, 7), new Knight(1, 7, true));
            myPieces.Add(new Coord(6, 7), new Knight(6, 7, true));

            myPieces.Add(new Coord(2, 0), new Bishop(2, 0, false));
            myPieces.Add(new Coord(5, 0), new Bishop(5, 0, false));
            myPieces.Add(new Coord(2, 7), new Bishop(2, 7, true));
            myPieces.Add(new Coord(5, 7), new Bishop(5, 7, true));

            myPieces.Add(new Coord(4, 0), new Queen(4, 0, false));
            myPieces.Add(new Coord(4, 7), new Queen(4, 7, true));

            myPieces.Add(new Coord(3, 0), new King(3, 0, false));
            myPieces.Add(new Coord(3, 7), new King(3, 7, true));

            if (player) myKing = new Coord(3, 7);
                else myKing = new Coord(3, 0);
        }

        private void Chessboard_MouseClick(object sender, MouseEventArgs e)
        {
            Piece temp;
            if(selected==null)
				{
                    //if (turn)
                        if(myPieces.ContainsKey(ToCoord(e.X, e.Y)))
                            selected = myPieces[ToCoord(e.X, e.Y)];
                            //if (myPieces[ToCoord(e.X, e.Y)].Color == player) selected = myPieces[ToCoord(e.X, e.Y)];  Rule to enforce only moving your pieces
                            //Believe was removed for demonstration purposes.
                            
					if(Chessboard.Debug>0)Console.WriteLine(selected);
				}
				else
				{
					Coord newMove = ToCoord(e.X,e.Y);
                    if (LegalMove(selected, newMove))
                    {
                        myPieces.Remove(selected.Coord);

                        if (selected is Rook) selected.Castle = false;
                        if (selected is King)
                        { 
                            selected.Castle = false;
                            if (Math.Abs(newMove.X - selected.Coord.X) > 1)
                            {
                                if (newMove.X > selected.Coord.X)
                                {
                                    temp = myPieces[new Coord(7, selected.Coord.Y)];
                                    temp.Castle = false;
                                    myPieces.Remove(new Coord(7, selected.Coord.Y));
                                    myPieces.Add(new Coord(4, selected.Coord.Y), temp);
                                    temp.ChangePos(new Coord(4, selected.Coord.Y));
                                }
                                else
                                {
                                    temp = myPieces[new Coord(0, selected.Coord.Y)];
                                    temp.Castle = false;
                                    myPieces.Remove(new Coord(0, selected.Coord.Y));
                                    myPieces.Add(new Coord(2, selected.Coord.Y), temp);
                                    temp.ChangePos(new Coord(2, selected.Coord.Y));
                                }
                            }
                        }

                        if (!myPieces.ContainsKey(newMove))
                        {
                            myPieces.Add(newMove, selected);
                            selected.ChangePos(newMove);
                        }
                        else
                        {
                            myPieces.Remove(newMove);
                            myPieces.Add(newMove, selected);
                            selected.ChangePos(newMove);
                        }
                      
                        
                        selected = null;
                        
                        
                        Invalidate();
                    }
                    else
                    {
                        selected.ResetDraw();
                        selected = null;
                        
                        Invalidate();
                    }
				}
        }

        private void Chessboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (selected != null)
            {
                selected.FreeDraw(e.X, e.Y);//cannot move piece around graph just needs to move draw point....
                Invalidate();
            }
        }

     /**
	 * Converts a position
	 * into a Chess Coord
	 * @param x
	 * @param y
	 * @return the Coord corresponding to the location
	 */
        private Coord ToCoord(int x, int y)
        {
            x = x / (this.Size.Width / 8);
            y = y / (this.Size.Height / 8);
            return new Coord(x, y);
        }

        private void Chessboard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawChessboard(g);
            DrawPieces(g);
        }

        private void DrawChessboard(Graphics g)
        {
            int size = Area / 8;
            using (SolidBrush b = new SolidBrush(Color.Brown))
            {
                using (Pen p = new Pen(Color.Brown, 10))
                {
                    for (int x = 0; x < 8; x++)
                        for (int y = 0; y < 8; y++)
                        {
                            if (y % 2 == 0 && x % 2 == 0) g.FillRectangle(b, (x * size), (y * size), size, size);
                            else if (y % 2 == 1 && x % 2 == 1) g.FillRectangle(b, (x * size), (y * size), size, size);

                        }
                    /*g.DrawLine(p, 0, 0, 319, 0);
                    g.DrawLine(p, 0, 319, 319, 319);
                    g.DrawLine(p, 319, 319, 319, 0);
                    g.DrawLine(p, 0, 0, 0, 319);*/
                }
            }
        }

        private void DrawPieces(Graphics g)
        {
            foreach (KeyValuePair<Coord, Piece> o in myPieces)
            {
                (o.Value).DrawImage(g);
            }
        }

        /**
	     * Called after a piece is placed into a new spot
	     * Checks to see if the move is legal
	     * Deals with capturing pieces and collisions on move
	     * Deals with special moves
	     * @param The piece that is being moved
	     * @param The possible movement for the piece
	     * @return true if the move is legal else false
	     */
        private bool LegalMove(Piece a, Coord move)
	    {
		    List <Coord> posMoves = a.move();
            List<Coord> castMoves = new List<Coord>();
            Coord temp;

            if (Chessboard.Debug > 0)
            {
                Console.Write("[");
                foreach (Coord pos in posMoves)
                {
                    Console.Write(pos + ",");
                }
                Console.WriteLine("]");
            }
		    if(Chessboard.Debug>0)Console.WriteLine(move);

            if (a is Pawn)
            {
                try
                {
                    if (myPieces.ContainsKey(move) && move.X == a.Coord.X)
                        posMoves.Remove(move);
                }
                catch (IndexOutOfRangeException) { }
            }

            if (a is King)
            {
                if (a.Castle)
                {
                    temp = new Coord(0,a.Coord.Y);
                    if (myPieces.ContainsKey(temp) && myPieces[temp] is Rook && myPieces[temp].Castle)
                        castMoves.Add(new Coord(a.Coord.X-2, a.Coord.Y));
                    
                    temp = new Coord(7, a.Coord.Y);
                    if (myPieces.ContainsKey(temp) && myPieces[temp] is Rook && myPieces[temp].Castle)
                        castMoves.Add(new Coord(a.Coord.X+2, a.Coord.Y));
                }

                for (int x = 0; x < posMoves.Count; x++)
                {
                    if (CheckForCheck(posMoves[x])) posMoves.Remove(posMoves[x]);
                }

                for (int x = 0; x < castMoves.Count; x++)
                {
                    if (CheckForCheck(castMoves[x])) castMoves.Remove(castMoves[x]);
                }
            }

            if (posMoves.Contains(move))
            {
                if (Collision(a.Coord, move))
                {
                    //Handle Collision on the move point
                    //Friendly Pieces may not be taken
                    if (!myPieces.ContainsKey(move) || myPieces[move].Equals(selected)) return true;
                    else if (myPieces[move].Color != selected.Color) return true;
                }
                else if (Chessboard.Debug > 0) Console.WriteLine("Collision Occured between : [" + a.Coord + ", " + move + "]");
            }
            else if (castMoves.Contains(move))
            {
                if (Collision(a.Coord, move))
                {
                    if (move.Y < a.Coord.Y)
                    {
                        if (Collision(new Coord(a.Coord.X, 0), a.Coord))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (Collision(new Coord(a.Coord.X, 7), a.Coord))
                        {
                            return true;
                        }
                    }
                }
            }
            
            return false;
	    }


        /**
	     * Used by legal Move
	     * Determines if there are any pieces in the way
	     * from Coord a to Cord b
	     * 
	     * note: Does not check Coord a or b
	     * 	     only handles spaces in between
	     * @param Starting Coord for check
	     * @param End Coord for check
	     * @return If move passes through any piece returns false 
	     * else move is clear and true is returned
	     */
        private bool Collision(Coord a, Coord b)
	    {
    		
		    if(a.X==b.X)
		    {
			    if(b.Y>a.Y)
				    for(int x = a.Y+1; x < b.Y; x++)
				    {
					    if(myPieces.ContainsKey(new Coord(a.X, x)))return false;
				    }
			    else if(b.Y<a.Y)
				    for(int x = a.Y-1; x > b.Y; x--)
				    {
					    if(myPieces.ContainsKey(new Coord(a.X, x)))return false;
				    }
		    }
		    if(a.Y==b.Y)
		    {
			    if(b.X>a.X)
				    for(int x = a.X+1; x < b.X; x++)
				    {
					    if(myPieces.ContainsKey(new Coord(x, a.Y)))return false;
				    }
			    else if(b.X<a.X)
				    for(int x = a.X-1; x > b.X; x--)
				    {
					    if(myPieces.ContainsKey(new Coord(x, a.Y)))return false;
				    }
		    }
		    if(Math.Abs(b.X-a.X)==Math.Abs(b.Y-a.Y)&&Math.Abs(b.X-a.X)!=0)
		    {
			    if(Chessboard.Debug>0)Console.WriteLine("Checking for Diagonal Collision");
    			
			    for(int x = 1; x < Math.Abs(b.Y-a.Y); x++)
			    {
				    if(b.X>a.X)
				    {	
					    if(b.Y>a.Y)
					    {
						    if(Chessboard.Debug>0)Console.WriteLine("Checking for Diagonal Collision: "+new Coord(a.X+x, a.Y+x));
    						
						    if(myPieces.ContainsKey(new Coord(a.X+x, a.Y+x)))return false;
					    }
					    else if(b.Y<a.Y)
					    {
						    if(Chessboard.Debug>0)Console.WriteLine("Checking for Diagonal Collision: "+new Coord(a.X+x, a.Y-x));
    						
						    if(myPieces.ContainsKey(new Coord(a.X+x, a.Y-x)))return false;
					    }
				    }
				    else if(b.X<a.X)
				    {
					    if(b.Y>a.Y)
					    {
						    if(Chessboard.Debug>0)Console.WriteLine("Checking for Diagonal Collision: "+new Coord(a.X-x, a.Y+x));
    						
						    if(myPieces.ContainsKey(new Coord(a.X-x, a.Y+x)))return false;
					    }
					    else if(b.Y<a.Y)
					    {
						    if(Chessboard.Debug>0)Console.WriteLine("Checking for Diagonal Collision: "+new Coord(a.X-x, a.Y-x));
    						
						    if(myPieces.ContainsKey(new Coord(a.X-x, a.Y-x)))return false;
					    }
				    }
			    }
		    }
		    return true;
	    }

        //false means no check
        //true == check
        //Generates a list of all the squares being attacked this turn.
        //If the king is ont he list check is declared.
        private bool CheckForCheck(Coord c)
        {
           /* ArrayList inDanger = new ArrayList();

            foreach (Piece p in myPieces)
            {
                foreach(Coord c in p.move())
                {
                    inDanger.Add(c);
                }
            }

            myPieces.(new King(player));*/

            return false;

        }
        /* Depreceated/unfinished in favor of an alternate method.
        //false means no check
        //true == check
        private bool CheckForCheck(Coord c)
        {
            List<Coord> templist;
            Knight cknight = new Knight(c,true);
            Piece temp;
            Coord first, second, third, fourth;

            if (c.X > c.Y)
            {
                third = new Coord(c.X - c.Y, 0);
            }
            else
            {
                third = new Coord(0, c.Y - c.X);
            }

            first = new Coord(7 - third.Y, 7 - third.X);
            if (c.X + c.Y > 7)
            {
                second = new Coord(c.X + c.Y - 7, 7);
            }
            else
            {
                second = new Coord(0, c.X + c.Y);
            }

            fourth = new Coord(second.Y, second.X);

            //checking for knights covering the coord
            templist = cknight.move();
            foreach (Coord x in templist)
            {
                if (myPieces.ContainsKey(x))
                {
                    if (myPieces[x] is Knight)
                    {
                        return true;
                    }
                }
            }

            temp = Prox(c, new Coord(c.X, 7));
            if (temp != null)
            {
                templist = temp.move();
                if (templist.Contains(c))
                {
                    return true;
                }

            }

            temp = Prox(c, new Coord(c.X, 0));
            if (temp != null)
            {
                templist = temp.move();
                if (templist.Contains(c))
                {
                    return true;
                }
            }

            temp = Prox(c, new Coord(7, c.Y));
            if (temp != null)
            {
                templist = temp.move();
                if (templist.Contains(c))
                {
                    return true;
                }
            }

            temp = Prox(c, new Coord(0, c.Y));
            if (temp != null)
            {
                templist = temp.move();
                if (templist.Contains(c))
                {
                    return true;
                }
            }

            temp = Prox(c, first);
            if (temp != null)
            {
                templist = temp.move();
                if (templist.Contains(c))
                {
                    return true;
                }

            }

            temp = Prox(c, second);
            if (temp != null)
            {
                templist = temp.move();
                if (templist.Contains(c))
                {
                    return true;
                }

            }
            temp = Prox(c, third);
            if (temp != null)
            {
                templist = temp.move();
                if (templist.Contains(c))
                {
                    return true;
                }

            }
            temp = Prox(c, fourth);
            if (temp != null)
            {
                templist = temp.move();
                if (templist.Contains(c))
                {
                    return true;
                }

            }
            return false;
        }

        private Piece Prox(Coord a, Coord b)
        {

            if (a.X == b.X)
            {
                if (b.Y > a.Y)
                    for (int x = a.Y + 1; x <= b.Y; x++)
                    {
                        if (myPieces.ContainsKey(new Coord(a.X, x))) return myPieces[new Coord(a.X, x)];

                    }
                else if (b.Y < a.Y)
                    for (int x = a.Y - 1; x >= b.Y; x--)
                    {
                        if (myPieces.ContainsKey(new Coord(a.X, x))) return myPieces[new Coord(a.X, x)];

                    }
            }
            if (a.Y == b.Y)
            {
                if (b.X > a.X)
                    for (int x = a.X + 1; x <= b.X; x++)
                    {
                        if (myPieces.ContainsKey(new Coord(x, a.Y))) return myPieces[new Coord(x, a.Y)];

                    }
                else if (b.X < a.X)
                    for (int x = a.X - 1; x >= b.X; x--)
                    {
                        if (myPieces.ContainsKey(new Coord(x, a.Y))) return myPieces[new Coord(x, a.Y)];

                    }
            }
            if (Math.Abs(b.X - a.X) == Math.Abs(b.Y - a.Y) && Math.Abs(b.X - a.X) != 0)
            {
                if (Chessboard.Debug > 0) Console.WriteLine("Checking for Diagonal Collision");

                for (int x = 1; x < Math.Abs(b.Y - a.Y); x++)
                {
                    if (b.X > a.X)
                    {
                        if (b.Y > a.Y)
                        {
                            if (Chessboard.Debug > 0) Console.WriteLine("Checking for Diagonal Collision: " + new Coord(a.X + x, a.Y + x));

                            if (myPieces.ContainsKey(new Coord(a.X + x, a.Y + x))) return myPieces[new Coord(a.X + x, a.Y + x)];

                        }
                        else if (b.Y < a.Y)
                        {
                            if (Chessboard.Debug > 0) Console.WriteLine("Checking for Diagonal Collision: " + new Coord(a.X + x, a.Y - x));

                            if (myPieces.ContainsKey(new Coord(a.X + x, a.Y - x))) return myPieces[new Coord(a.X + x, a.Y - x)];

                        }
                    }
                    else if (b.X < a.X)
                    {
                        if (b.Y > a.Y)
                        {
                            if (Chessboard.Debug > 0) Console.WriteLine("Checking for Diagonal Collision: " + new Coord(a.X - x, a.Y + x));

                            if (myPieces.ContainsKey(new Coord(a.X - x, a.Y + x))) return myPieces[new Coord(a.X - x, a.Y + x)];

                        }
                        else if (b.Y < a.Y)
                        {
                            if (Chessboard.Debug > 0) Console.WriteLine("Checking for Diagonal Collision: " + new Coord(a.X - x, a.Y - x));

                            if (myPieces.ContainsKey(new Coord(a.X - x, a.Y - x))) return myPieces[new Coord(a.X - x, a.Y - x)];

                        }
                    }
                }
            }
            return null;
        }*/
    }
}
