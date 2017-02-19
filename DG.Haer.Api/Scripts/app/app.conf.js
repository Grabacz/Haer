/// <reference path="../vendors/angular-route.js" />

angular.module("haerApp")

.config(function ($locationProvider, $logProvider, $interpolateProvider) {

    if (window.history && window.history.pushState)
        $locationProvider.html5Mode(true);

    $logProvider.debugEnabled(true);
    $interpolateProvider.startSymbol("{{").endSymbol("}}");
})