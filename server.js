/**
*	Configuration
*/

var express = require('express');
var app = express();
var bodyParser = require('body-parser');
var loggly = require('loggly');
var router = express.Router();
var mongoose = require('mongoose');

var Event = require('./app/models/event.js');
var Token = require('./app/models/token.js');

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
	var databaseURL = "mongodb://localhost:27017/";
}

mongoose.connect(databaseURL);
logger("Database is located at: " + databaseURL);

/**
*	Routes
*/

app.use(express.static(__dirname + '/app/view'));
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use('/api', router);

router.use(function(request, response, next) {
	next();
});

router.route('/events').get(function(request, response) {
	Event.find({ }, function(error, events) {
		if (error) {
			logger("Error fetching data.");
			response.status(500).json({ error: "Error fetching data." });
		}

		response.json(events);
	})
});

router.route('/events/:eventId').get(function(request, response) {
	Event.findById(request.params.eventId, function(error, event) {
		if (error) {
			logger("Error fetching data.");
			response.status(500).json({ error: "Error fetching data." });
		}

		response.json(event);
	})
});

router.route("/events").post(function(request, response) {
	checkAuth(request.body.token, function(success, authedDevice) {
		if (success) {
			logger("Device " + authedDevice.device + " atempted auth with a token and sucedded.");

			if (
				typeof request.body.GPSlat == "undefined"
				|| typeof request.body.GPSlog == "undefined"
				|| typeof request.body.GPSalt == "undefined"
				|| typeof request.body.temp == "undefined"
				|| typeof request.body.errorCode == "undefined"
			) {
				logger("Wrong event format.");
				response.status(500).json({ error: "Wrong event format." });
			}
			else {
				var newEvent = new Event();

				newEvent.time = new Date();
				newEvent.device = authedDevice._id;
				newEvent.GPSlog = request.body.GPSlog;
				newEvent.GPSlat = request.body.GPSlat;
				newEvent.GPSalt = request.body.GPSalt;
				newEvent.temp = request.body.temp;
				newEvent.errorCode = request.body.errorCode;

				newEvent.save(function(error) {
					if (error) {
						logger("Error saving event");
						response.status(500).json({ error: "Error saving event." });
					}

					logger("Event " + newEvent._id + " saved.");
					response.json(newEvent);
				});
			}
		}
		else {
			logger("Device attempted auth with token " + request.body.token + " and failed.");
			response.status(401).json({ error: "Auth error."});
		}
	});
});

app.get('/', function(request, response) {
	response.sendFile(__dirname + "/app/view/index.html");
});

app.get('*', function(request, response) {
	response.sendFile(__dirname + "/app/view/404.html");
});

/**
*	Functions
*/

function checkAuth(clientToken, callback) {
	Token.findOne({ token: clientToken }, function(error, authedDevice) {
		if (error) {
			callback(false, null);
		}

		if (isEmpty(authedDevice))
			callback(false, null);
		else if (authedDevice.active) // If the token is set to active
			callback(true, authedDevice);
		else
			callback(false, null);
	});
}

function logger(message) {
	if (process.env.enviroment == 'prod') {
		client.log(message);
	}

	console.log(new Date() + " " + message);
}

function isEmpty(obj) {
	if (obj == null)
		return true;

	if (obj.length && obj.length > 0)
		return false;

	if (obj.length === 0)
		return true;


	for (var key in obj) {
		if (hasOwnProperty.call(obj, key)) return false;
	}

	return true;
}
