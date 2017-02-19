/// <reference path="viws/ng-dropdown.html" />
/// <reference path="viws/ng-dropdown.html" />
/// <reference path="../vendors/angular.js" />

angular.module("commonMdl", [])

.directive("ngAccordion", function () {
    return {
        link: function (scope, element, attrs) {
            element.accordion({ selector: { trigger: "#accordion-trigger" } });
        }
    }
})

.directive("cnPopup", function () {
    return {
        link: function (scope, element, attrs) {
            element.popup({ variation: "inverted" });
        }
    }
})

.directive('updateOnEnter', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ctrl) {
            element.on("keyup", function (ev) {
                if (ev.keyCode == 13) {
                    ctrl.$commitViewValue();
                    scope.$apply(ctrl.$setTouched);
                }
            });
        }
    }
})

.service("iconKind", function () {
    this.none = "";
    this.icon = "icon";
    this.flag = "flag";
})

.service("fn", function () {
    this.getElement = function (array, propertyValue, propertyName) {

        for (var i = 0; i < array.length; i++) {
            if (array[i][propertyName] == propertyValue) {
                return array[i];
            }
        }
    }
    this.getElementById = function (array, id) {
        return this.getElement(array, id, "id");
    }
})

.filter("to", function (fn) {
    return function (value, array, inputPropertyName, outputPropertyName) {
        if (angular.isUndefined(array) || !angular.isArray(array) || angular.isUndefined(inputPropertyName) || angular.isUndefined(outputPropertyName) || angular.isUndefined(value)) {
            return value;
        }

        var selectedElement = fn.getElement(array, value, inputPropertyName);
        if (angular.isUndefined(selectedElement))
            return value;
        return selectedElement[outputPropertyName];
    }
})

.directive("ngDropdown", function (iconKind, $filter, $log, $timeout) {
    var toJson = $filter("json")

    return {
        restrict: "E",
        require: "ngModel",
        replace: true,
        scope: {
            options: "=",
            defaultText: "@",
            model: "=ngModel",
            isList: "@",
            isEnumeration: "@",
            isFilter: "@",
            isDisabled: "=ngDisabled"
        },
        templateUrl: "Scripts/common/views/ng-dropdown.html",
        link: function (scope, elem, attrs) {
            elem.dropdown({
                onChange: function (value, text, $selectedItem) {
                    scope.model = (typeof scope.model == "number") ? Number(value) : value;
                    scope.$apply();
                }
            });

            scope.$watch("model", function (newVal, oldVal) {
                $timeout(function () {
                    if (angular.isUndefined(newVal) || newVal == null) {
                        elem.dropdown("clear");
                    }
                    elem.dropdown("set selected", newVal);
                })
            })
        },
        controller: ["$scope", "$timeout", "iconKind", function ($scope, $timeout, iconKind) {
            $scope.iconKind = iconKind;
        }]
    }
})