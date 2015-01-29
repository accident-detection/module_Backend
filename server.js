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

if (process.env.enviroment == 'prod') {
	var client = loggly.createClient({
		token: process.env.logglyToken,
		subdomain: process.env.logglySubdomain,
		tags: ['accident-detection'],
		json: true
	});
}

/**
* App start
*/

app.set('port', (process.env.PORT || 5000));

app.listen(app.get("port"), function() {
	logger("API is running at localhost:" + app.get('port'));
});


/**
*	MongoDB
*/

if (process.env.enviroment == 'prod') {
	logger("App is started in production. Good luck!");
	var databaseURL = process.env.MONGODB_URL;
}
else {
	var databaseURL = "test";
}

logger("Database is located at: " + databaseURL);
var db = mongojs(databaseURL, ['logDB', 'tokens']);

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
			throw error;

		response.json(logEvents);
	});
});

app.get("/api/events/:logEvent_id", function(request, response) {
	db.logDB.find({
		"_id": ObjectId(request.params.logEvent_id)
	}, function(error, logEvent) {
		if (error) {
			throw(error);
		}

		response.json(logEvent[0]);
	});
});

app.post("/api/events", function(request, response) {
	var authToken = request.body.auth;

	checkAuth(request.body.token, function(success, authedDevice) {
		if (success) {
			logger("Device " + authedDevice.device + " atempted auth with a token and succedded.");

			var logEvent = {
				time: new Date(),
				GPSlat: request.body.GPSlat,
				GPSlog: request.body.GPSlog,
				GPSalt: request.body.GPSalt,
				temp: request.body.temp,
				errorCode: request.body.errorCode
			};

			saveDevice(logEvent, response);
		}
		else {
			logger("Device atempted auth with a token and failed.");
			response.json({ error: "Auth token check failed." });
		}
	});
});

app.get('*', function(request, response) {
	response.sendFile(__dirname + "/public/index.html");
});

/**
*	Functions
*/

function checkAuth(clientToken, callback) {
	db.tokens.find( { token: clientToken }, function(error, foundTokens) {
		if (error) {
			logger(error);
			callback(false);
		}


		if (foundTokens.length == 0)
			callback(false);
		else if (foundTokens[0].token == clientToken && foundTokens[0].active)
			callback(true, foundTokens[0]);
		else
			callback(false);
	});
}

function saveDevice(logEvent, response) {
	db.logDB.save(logEvent, function(error) {
		if (error) {
			logger("There was an error saving an event to the database.");
			throw(error);
		}

		logger("New event added to the database.");
		response.json(logEvent);
	});
};

function logger(message) {
	if (process.env.enviroment == 'prod') {
		client.log(message);
	}

	console.log(new Date() + " " + message);
}
