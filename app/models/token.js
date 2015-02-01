var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var TokenSchema = new Schema({
    token: String,
    device: String,
    active: Boolean
});

module.exports = mongoose.model('Token', TokenSchema);
