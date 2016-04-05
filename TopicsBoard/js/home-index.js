//home-index.js

(function () {
    "use strict";

    angular.module("topicsApp").controller("homeIndexController", homeIndexController);

    function homeIndexController($scope, $http, dataService, canEdit) {

        $scope.dataCount = 0;
        $scope.data = dataService;
        $scope.isBusy = false;
        $scope.canEdit = canEdit;

        if (dataService.isReady() == false) {
            $scope.isBusy = true;
            dataService.getTopics().
                then(function () {
                    //Success
                    toastr.success("Article view loaded");
                }, function () {
                    //Error
                   toastr.error("Error occured while retrieving data!");
                }).then(function () {
                    $scope.isBusy = false;
                });
        }

    }

})();

