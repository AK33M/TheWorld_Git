// app-trips.js
(function () {
    "use strict";

    //creating the module
    angular.module("app-trips", ["simpleControls", "ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "tripsController",
                controllerAs: "model",
                templateUrl: "/views/tripsView.html"
            });

            $routeProvider.when("/editor/:tripName", {
                controller: "tripEditorController",
                controllerAs: "model",
                templateUrl: "/views/tripEditorView.html"
            });

            $routeProvider.otherwise({
                redirectTo: "/"
            });
        });
})();