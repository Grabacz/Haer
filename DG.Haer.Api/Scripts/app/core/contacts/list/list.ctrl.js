/// <reference path="../../../vendors/angular.js" />

angular.module("contactsMdl")

.controller("listCtrl", function ($scope, contactTypesService, contactsService) {

    $scope.vm = {
        models: []
    }

    $scope.vm.options = {
        contactTypes: contactTypesService.getEnumes()
    }

    $scope.vm.searchSet = {
        selectedPage: null,
        filter: [],
        reset: function () {
            $scope.vm.searchSet.selectedPage = null;
            $scope.vm.searchSet.filter = [];
        },
        resetSelectedPage: function () {
            $scope.vm.searchSet.selectedPage = null;
        },
        resetFilter: function () {
            $scope.vm.searchSet.filter = [];
        }
    }

    $scope.pagination = {
        currentPage: 2,
        totalPages: 5,
        itemsPerPage: 10,
        pages: [],
        initPages: function () {
            $scope.pagination.pages = [];
            for (var i = 0; i < $scope.pagination.totalPages; i++) {
                $scope.pagination.pages.push(i + 1);
            }
        },
        nextPage: function () {
            if ($scope.pagination.currentPage != $scope.pagination.totalPages) {
                $scope.pagination.currentPage++;
                $scope.action.getModels($scope.pagination.currentPage, $scope.filter.model);
            }
        },
        previousPage: function () {
            if ($scope.pagination.currentPage != 1) {
                $scope.pagination.currentPage--;
                $scope.action.getModels($scope.pagination.currentPage, $scope.filter.model);
            }
        },
        setCurrentPage: function (currentPage) {
            $scope.pagination.currentPage = currentPage;
            $scope.action.getModels($scope.pagination.currentPage, $scope.filter.model);
        }
    }

    $scope.filter = {
        model: {},
        isActive: false,
        type: {
            current: 0,
            options: [
                { icon: "filter", value: "" },
                { icon: "code", value: "1"},
                { icon: "bug", value: "2"}
            ],
            next: function(){
                if ($scope.filter.type.current >= 2) {
                    $scope.filter.type.current = 0;
                }
                else
                    $scope.filter.type.current++;
                $scope.filter.model.contactType = $scope.filter.type.getValue();
            },
            getIcon: function () {
                return $scope.filter.type.options[$scope.filter.type.current].icon;
            },
            getValue: function () {
                return $scope.filter.type.options[$scope.filter.type.current].value;
            }
        },
        clear: function (property) {
            if (angular.isUndefined(property)) {
                $scope.filter.model = {};
            }
            else {
                $scope.filter.model[property] = undefined;
                
            }
            $scope.action.getModels();
        }
    }

    $scope.action = {
        getModels: function (page, filter) {
            contactsService.searchContacts({
                selectedPage: page,
                filter: filter
            })
                .success(function (response) {
                    $scope.pagination.currentPage = response.currentPage;
                    $scope.pagination.totalPages = response.totalPages;
                    $scope.pagination.itemsPerPage = response.itemsPerPage
                    $scope.pagination.initPages();

                    $scope.vm.models = response.items;
                })
                .error(function (response) {
                    console.log("error");
                    console.log(response);
                })
        }
    }

    $scope.$watchCollection("filter.model", function (model) {
        $scope.action.getModels(null, $scope.filter.model);
        var isActive = false;
        angular.forEach(model, function (value, key) {
            if (value) {
                isActive = true;
            }
        })
        $scope.filter.isActive = isActive;
    })

    $scope.action.getModels();
})