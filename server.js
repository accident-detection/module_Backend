/**
*	Configuration
*/

var express = require("express");
var app = express();
var mongojs = require("mongojs");
var ObjectId = mongojs.ObjectId;
var bodyParser = require('body-parser');
var methodOverride = require('method-override');
var loggly = require('loggly');

/**
*	Loggly logging
*/

var client = loggly.createClient({
	token: process.env.logglyToken,
	subdomain: process.env.logglySubdomain,
	tags: ['accident-detection'],
	json:true
});

/**
* App start
*/

app.set('port', (process.env.PORT || 5000));

app.listen(app.get("port"), function() {
	client.log("Node app is running at localhost:" + app.get('port'));
});


/**
*	MongoDB
*/

if (process.env.enviroment == 'prod') {
	client.log(new Date() + " App is started in production. Good luck!");
	var databaseURL = process.env.MONGODB_URL;
}
else {
	var databaseURL = "test";
}

client.log(new Date() + " Database is located at: " + databaseURL);
var db = mongojs(databaseURL, ['logDB']);

/**
*	Routes
*/

app.use(express.static(__dirname + '/public'));
app.use(bodyParser.urlencoded({'extended':'true'}));
app.use(bodyParser.json());
app.use(bodyParser.json({ type: 'application/vnd.api+json' }));
app.use(methodOverride());

app.get("/api/events", function(request, response) {
	db.logDB.find(function(error, logEvents) {
		if (error)
			response.send(error);

		response.json(logEvents);
	});
});

app.get("/api/events/:logEvent_id", function(request, response) {
	db.logDB.find({
		"_id": ObjectId(request.params.logEvent_id)
	}, function(error, logEvent) {
		if (error) {
			response.send(error);
		}

		response.json(logEvent[0]);
	});
});

app.post("/api/events", function(request, response) {
	var logEvent = {
		time: new Date(),
		logmsg: request.body.logmsg
	};

	saveDevice(logEvent, response);
});

app.get('*', function(request, response) {
	response.sendFile(__dirname + "/public/index.html");
});

/**
*	Functions
*/

function saveDevice(logEvent, response) {
	db.logDB.save(logEvent, function(error) {
		if (error) {
			client.log("There was an error saving an event to the database.");
			response.send(error);
		}

		client.log("New event added to the database.");
	});

	response.json(logEvent);
};
