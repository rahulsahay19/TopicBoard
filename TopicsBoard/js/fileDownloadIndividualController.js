//fileDownloadIndividualController.js

(function () {
    "strict";

    angular.module("topicsApp").controller("fileDownloadIndividualController", fileDownloadIndividualController);

    function fileDownloadIndividualController($scope, $routeParams, $http) {

        $http.get('FileDownload/DownloadFiles').then(function () {
            //Success
        }, function () {
            //Error
        });

    }
}());