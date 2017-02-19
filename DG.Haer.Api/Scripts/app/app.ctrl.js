/// <reference path="../vendors/angular.js" />

angular.module("haerApp")

.controller("appCtrl", function ($scope) {

    $scope.vm = {
    }

    $scope.nav = {
        currentTab: "list",
        currentViewCaption: "lista kontaktów",
        setCurrentTab: setCurrentTab,
        getTabActivityClass: getTabActivityClass,
        setCurrentViewCaption: setCurrentViewCaption
    }

    $scope.$on("viewChanged", function (event, args) {
        $scope.nav.setCurrentTab(args.currentTab);
    })

    function setCurrentTab(tabName) {
        $scope.nav.currentTab = tabName;
        $scope.nav.setCurrentViewCaption(tabName);
    }
    function getTabActivityClass(tabName) {
        return $scope.nav.currentTab === tabName ? "active" : "";
    }

    function setCurrentViewCaption(tabName) {
        switch (tabName) {
            case "list":
                $scope.nav.currentViewCaption = "lista kontaktów";
                break;
            case "add":
                $scope.nav.currentViewCaption = "dodawanie kontaktu";
                break;
        }
    }
})