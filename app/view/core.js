var logEvents = angular.module('logEvents', []);

function mainController($scope, $http) {
	$scope.formData = {};

	// when landing on the page, get all events and show them
	$http.get('/api/events')
		.success(function(data) {
			$scope.events = data;

			var locations = [];

			angular.forEach(data, function(value, key) {
				var location = {
					lat: value.GPSlat,
					log: value.GPSlog,
					alt: value.GPSalt
				}
				
				this.push(location);
			}, locations);

			$scope.locations = locations;
		})
		.error(function(data) {
			console.log("Error: " + data);
		});
};

function extractPosition(data, callback) {
	var locations = [];

	data.every(function(element){
		locations += extractPosition(element);
	});
}
