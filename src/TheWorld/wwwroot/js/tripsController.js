//tripsController.js
(function () {
    "use strict";

    //getting the existing module
    angular.module("app-trips")
    .controller("tripsController", tripsController);

    function tripsController($http) {
        var model = this;

        model.trips = [];

        model.newTrip = {};

        model.errorMessage = "";

        model.isBusy = true;

        $http.get("/api/trips")
            .then(function (response) {
                //Success
                angular.copy(response.data, model.trips);
            }, function (error) {
                //Failure
                model.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                model.isBusy = true;
            });

        model.addTrip = function () {
            model.isBusy = true;
            model.errorMessage = "";

            $http.post("/api/trips", model.newTrip)
            .then(function (response) {
                //Success
                model.trips.push(response.data);
                model.newTrip = {};
            }, function (error) {
                //Failure
                model.errorMessage= "Failed to save new trip"
            })
            .finally(function () {
                model.isBusy = false;
            });
        };
    }
})();