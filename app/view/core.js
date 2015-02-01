var logEvents = angular.module('logEvents', []);

function mainController($scope, $http) {
	$scope.formData = {};

	$http.get('/api/events')
		.success(function(data) {
			$scope.events = data;

			var locations = [];

			angular.forEach(data, function(value, key) {
				var location = {
					lat: value.GPSlat,
					log: value.GPSlog,
					alt: value.GPSalt
				};

				this.push(location);
			}, locations);

			$scope.locations = locations;
		})
		.error(function(data) {
			// redirect to 500.html
			$location.url('/500.html');
		});
};

function deviceController($scope, $http) {
	$scope.formData = {};

	$http.get('/api/device/') //inject device id from URL??
		.success(function(data) {
			$scope.events = data;

			var locations = [];

			angular.forEach(data, function(value, key) {
				var location = {
					lat: value.GPSlat,
					log: value.GPSlog,
					alt: value.GPSalt
				};

				this.push(location);
			}, locations);

			$scope.locations = locations;
		})
		.error(function(data) {
			// redirect to 500.html
			$location.url('/500.html');
		});
};
