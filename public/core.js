var logEvents = angular.module('logEvents', []);

function mainController($scope, $http) {
	$scope.formData = {};

	// when landing on the page, get all events and show them
	$http.get('/api/events')
		.success(function(data) {
			$scope.events = data;
		})
		.error(function(data) {
			console.log("Error: " + data);
		});
};
