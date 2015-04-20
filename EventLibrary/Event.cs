using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibrary
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID
        {
            get;
            set;
        }

        private double _gpsLocLat;
        public double GpsLocLat
        {
            get { return _gpsLocLat; }
            set { _gpsLocLat = value; }
        }

        private double _gpsLocLog;
        public double GpsLocLog
        {
            get { return _gpsLocLog; }
            set { _gpsLocLog = value; }
        }

        private string _userToken;
        public string UserToken
        {
            get { return _userToken; }
            set { _userToken = value; }
        }

        private DateTime _time;
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private int _adCode;
        public int AdCode
        {
            get { return _adCode; }
            set { _adCode = value; }
        }

        public Event(double lat, double log, int adCode, string token)
        {
            _gpsLocLat = lat;
            _gpsLocLog = log;
            _userToken = token;
            _adCode = adCode;
            _time = DateTime.UtcNow;
        }
    }
}
