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
        public string ID { get; set; }

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

        private int _gpsCode;
        public int GpsCode
        {
            get { return _gpsCode; }
            set { _gpsCode = value; }
        }

        private double _gpsLocLat;
        public double GpsLocLat
        {
            get { return _gpsLocLat; }
            set { _gpsLocLat = value; }
        }

        private double _gpsLocLng;
        public double GpsLocLng
        {
            get { return _gpsLocLng; }
            set { _gpsLocLng = value; }
        }

        private double _gpsLocSpeed;
        public double GpsLocSpeed
        {
            get { return _gpsLocSpeed; }
            set { _gpsLocSpeed = value; }
        }

        private string _userToken;
        public string UserToken
        {
            get { return _userToken; }
            set { _userToken = value; }
        }

        public Event(DateTime time, int adCode, int gpsCode, double lat, double lng, double speed, string token)
        {
            _time = time;
            _adCode = adCode;
            _gpsCode = gpsCode;
            _gpsLocLat = lat;
            _gpsLocLng = lng;
            _gpsLocSpeed = speed;
            _userToken = token;
        }
    }
}
