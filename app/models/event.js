var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var EventSchema = new Schema({
    time: Date,
    device: String,
    GPSlog: Number,
    GPSlat: Number,
    GPSalt: Number,
    temp: Number,
    errorCode: Number
});

module.exports = mongoose.model('Event', EventSchema);
