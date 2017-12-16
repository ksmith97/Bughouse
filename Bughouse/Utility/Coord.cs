/* Just to make things confusing coordinates are stored as X being horizontal axis and Y vertical
 * [Column][Row]
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bughouse.Utility
{
    class Coord : IComparable
    {
        private int myX;

        public int X
        {
            get { return myX; }
            set { myX = value; }
        }

	    private int myY;

        public int Y
        {
            get { return myY; }
            set { myY = value; }
        }
	
	    public Coord(){}
    	
	    public Coord(int x, int y)
	    {
		    if(x<0||x>7||y<0||y>7)throw new IndexOutOfRangeException();
		    myX=x;
		    myY=y;
	    }
    	

	    public int CompareTo(Object arg0) {
		    Coord temp = (Coord)arg0;
		    if(myX == temp.X && myY == temp.Y)
			    return 0;
		    else return -1;
	    }
    	
	    /**
	     * Required for HashMap checks
	     */
	    public override bool Equals(Object obj)
	    {
		    if(obj is Coord)
		    {
			    Coord temp = (Coord)obj;
			    if(myX==temp.X&&myY==temp.Y)return true;
		    }
		    return false;
	    }
    	
	    /**
	     * Overload of Object method
	     * Returns a number unique to a specific Coord
	     * Two Coords with identical Numbers will have the same value
	     */
	    public override int GetHashCode()
	    {
            return ((10*myX)+myY);
	    }
    	
	    public override String ToString()
	    {
		    return "X:"+myX+" Y:"+myY;
	    }
    }
}
