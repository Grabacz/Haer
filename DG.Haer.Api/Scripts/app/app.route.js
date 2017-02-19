/// <reference path="../vendors/angular.js" />
/// <reference path="../vendors/angular-route.js" />

angular.module("haerApp")

.config(function ($routeProvider) {

    $routeProvider.when("/contacts/list", {
        controller: "listCtrl",
        templateUrl: "Scripts/app/core/contacts/list/list.html"
    });

    $routeProvider.when("/contacts/add", {
        controller: "addCtrl",
        templateUrl: "Scripts/app/core/contacts/add/add.html"
    });

    $routeProvider.otherwise({
        redirectTo: "/contacts/list"
    });
})