/// <reference path="../../../vendors/angular.js" />

angular.module("contactsMdl")

.controller("addCtrl", function ($scope, $rootScope, $location, contactTypesService, salaryService, contactsService) {

    $scope.vm = {
        model: {}
    }

    $scope.vm.options = {
        contactTypes: contactTypesService.getEnumes()
    }

    $scope.action = {
        save: function () {
            contactsService.addContact({
                name: $scope.vm.model.name,
                contactType: $scope.vm.model.contactType,
                experience: $scope.vm.model.experience,
                salary: $scope.vm.model.salary,
            })
                .success(function (response) {
                    $scope.$emit("viewChanged", { currentTab: "list" })
                    $location.path("/contacts/list");
                })
                .error(function(response){
                    console.log("błąd");
                    console.log(response);
                });
            },
        calculateSalary: function (contactType, experience) {
            contactType = Number(contactType);

            salaryService.calculateSalary({
                contactType: contactType,
                experience: experience
            })
                .success(function (response) {
                    $scope.vm.model.salary = response;
                })
                .error(function (response) {
                    console.log("error");
                    console.log(response);
                })
        }
    }

    $scope.$watchCollection("vm.model", function (model, oldModel) {

        if (model.name != oldModel.name
            || model.salary != oldModel.salary) {
            return;
        }
            
        if (angular.isDefined(model.contactType)
            && angular.isDefined(model.experience)
            && model.experience !== null) {
            $scope.action.calculateSalary(model.contactType, model.experience);
        }   
    })
})