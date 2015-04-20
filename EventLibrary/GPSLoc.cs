using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventLibrary
{
    class GPSLoc
    {
        private double _log;
        private double _lat;

        public GPSLoc(double log, double lat)
        {
            _log = log;
            _lat = lat;
        }
    }
}
