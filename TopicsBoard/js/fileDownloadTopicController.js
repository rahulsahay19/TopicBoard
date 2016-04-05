// fileDownloadTopicController.js

(function () {
    "strict";

    angular.module("topicsApp").controller("fileDownloadTopicController", fileDownloadTopicController);

    function fileDownloadTopicController($scope, dataService, $http, $routeParams) {
        /*toastr.success("Download File's View loaded");*/

        $scope.files = null;
        var data = null;
        dataService.getFiles($routeParams.id)
                   .then(function (files) {
                       //Success
                       debugger;
                       $scope.files = files;
                       if (files[0].fileData) {
                           data = files[0].fileData;
                           toastr.success("Files Fetched Successfully");
                       }
                   }, function () {
                       //Error
                       toastr.error("Error in File Fetching");
                   });


    }
}());