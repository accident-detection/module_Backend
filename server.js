/**
*	Configuration
*/

var express = require("express");
var app = express();
var mongojs = require("mongojs");
var ObjectId = mongojs.ObjectId;
var bodyParser = require('body-parser');
var methodOverride = require('method-override');

app.set('port', (process.env.PORT || 5000));

app.listen(app.get("port"), function() {
	console.log("Node app is running at localhost:" + app.get('port'));
});


/**
*	MongoDB
*/

if (process.env.enviroment == 'prod') {
	console.log(new Date() + " App is started in production. Good luck!");
	var databaseURL = process.env.MONGODB_URL;
}
else {
	var databaseURL = "test";
}

console.log(new Date() + " Database is located at: " + databaseURL);
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
			response.send(error);
		}
	});
	response.json(logEvent);
};
