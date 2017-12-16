using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bughouse.Utility
{
    class Location
    {
        protected int x_loc;    //Determines row starting from top of the board.
        protected int y_loc;    //Determines column starting from the left of the board.

        public Location() { }

        public Location(int x, int y)
        {
            x_loc = x;
            y_loc = y;
        }


        /*
        * x_loc properties
        */
        public int x
        {
            get { return this.x_loc; }
            set { this.x_loc = value; }
        }

        /*
         * y_loc properties
         */
        public int y
        {
            get { return this.x_loc; }
            set { this.x_loc = value; }
        }
    }
}
