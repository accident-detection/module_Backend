(function () {

    function mainController($scope, $http) {
        $scope.formData = {};

        $http.get('api/events').success(function (data) {
            $scope.events = data;
        })
        .error(function (data) {
            console.log("Error loading data from API.");
        });
    };

    angular.module("events", [])
        .controller("mainController", ["$scope", "$http", mainController]);
})();