/// <reference path="../../vendors/angular.js" />

angular.module("servicesMdl", ["ngResource"])

.factory("contactTypesService", function (iconKind) {
    var enums = [
        { value: 1, translation: "Programista", iconKind: iconKind.icon, icon: "code icon" },
        { value: 2, translation: "Tester", iconKind: iconKind.icon, icon: "bug icon" }
    ];

    return {
        getEnumes: function () {
            return enums;
        }
    }
})

.provider("salaryService", function () {
    this.baseUrl = "";
    return {
        setUrl: function (url) {
            if (angular.isDefined(url) && angular.isString(url)) {
                this.baseUrl = url;
                return this;
            }
        },
        $get: function ($http) {
            var baseUrl = this.baseUrl;
            return {
                getUrl: function () {
                    return baseUrl;
                },
                calculateSalary: function (data) {
                    return $http({
                        method: "PUT",
                        url: baseUrl + "/calculate",
                        data: data,
                        headers: {
                            "Content-Type": "application/json"
                        }
                    });
                }
            }
        }
    }
})


.provider("contactsService", function () {
    this.baseUrl = "";
    return {
        setUrl: function (url) {
            if (angular.isDefined(url) && angular.isString(url)) {
                this.baseUrl = url;
                return this;
            }
        },
        $get: function ($http) {
            var baseUrl = this.baseUrl;
            return {
                getUrl: function () {
                    return baseUrl;
                },
                getContacts: function (searchSet) {
                    return $http({
                        method: "PUT",
                        url: baseUrl + "/list",
                        data: searchSet,
                        headers: {
                            "Content-Type": "application/json"
                        }
                    });
                },
                addContact: function (data) {
                    var config = {
                        method: "POST",
                        url: baseUrl + "/add",
                        data: data,
                        headers: {
                            "Content-Type": "application/json"
                        }
                    }
                    return $http(config);
                }
            }
        }
    }
})
