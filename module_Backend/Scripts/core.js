(function () {

    function mainController($scope, $http) {
        $scope.formData = {};

        $http.get('http://iis.home.andreicek.eu/adEvents/api/events').success(function (data) {
            data.forEach(function (event) {
                if (event.AdCode == 205)
                    event.AdCodeMessage = "Hit from back";
                else if (event.AdCode == 206)
                    event.AdCodeMessage = "Hit in front";
                else if (event.AdCode == 207)
                    event.AdCodeMessage = "Hit while surrounded";
            });

            $scope.events = data;
        })
        .error(function (data) {
            console.log("Error loading data from API.");
        });
    };

    angular.module("events", [])
        .controller("mainController", ["$scope", "$http", mainController]);
})();