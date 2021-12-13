using System;
using System.Collections.Generic;
using System.Text;

namespace Covid
{
    class Person
    {
        public Coordinates Home { get; set; }
        public Coordinates Work { get; set; }
        public bool HaveCovid { get; set; }
        public bool AtWork { get; set; }

        public override string ToString()
        {
            return Home + ", " + Work + ", " + HaveCovid + ", " + AtWork;
        }
    }
}
