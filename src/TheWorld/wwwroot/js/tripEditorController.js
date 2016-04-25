// tripEditorController.js

(function () {
    "use strict";

    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var model = this;

        model.tripName = $routeParams.tripName;
        model.stops = [];
        model.errorMessage = "";
        model.isBusy = true;
        model.newStop = {};

        var url = "/api/trips/" + model.tripName + "/stops";

        $http.get(url)
            .then(function (response) {
                //success
                angular.copy(response.data, model.stops);
                _showMap(model.stops);
            }, function(error){
                model.errorMessage = "Failed to load stops";
            })
            .finally(function () {
                model.isBusy = false;
            });

        model.addStop = function () {
            model.isBusy = true;
            model.errorMessage = "";

            $http.post(url, model.newStop)
                .then(function (response) {
                    //success
                    model.stops.push(response.data);
                    _showMap(model.stops);
                    model.newStop = {};
                }, function (error) {
                    //failure
                    model.errorMessage = error;
                })
                .finally(function () {
                    model.isBusy = false;
                });
        };
    }

    function _showMap(stops) {
        if (stops && stops.length > 0) {

            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                }
            })

            //show Map
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom: 3
            })
        }
    }
})();