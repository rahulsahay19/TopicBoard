//singleTopicController.js

(function () {
    "use strict";

    angular.module("topicsApp").controller("singleTopicController", singleTopicController);

    function singleTopicController($scope, $window, $routeParams, $log, dataService, canEdit) {

        toastr.success("Detailed view loaded successfully!");
        $scope.topic = null;
        $scope.newTag = {};

        $scope.canEdit = canEdit;

      //  console.log($scope.canEdit);

        dataService.getTopicById($routeParams.id)
            .then(function (topic) {
                //Success
                $scope.topic = topic;
            }, function () {
                //Error
                $window.location = "#/";
            });

        $scope.addTag = function () {
            dataService.saveTag($scope.topic, $scope.newTag)
                .then(function () {
                    //Success
                    $scope.newTag.tagDescription = "";
                    toastr.success("New Keyword added");
                }, function () {
                    //Error
                    toastr.error("Couldn't save the new keyword!");
                });
        };

        $scope.deleteTopic = function (id) {
            bootbox.confirm({
                size: 'small',
                message: "Are you sure?",
                callback: function (response) {
                    if (response) {
                        dataService.removeTopic(id)
                            .then(function () {
                                //Success
                                toastr.success("Article Deleted Successfully!");
                                $window.location = "#/";
                            }, function (err) {
                                //Error
                                toastr.error("Error Deleting Article, Please delete all dependencies with Article First!");
                            });
                    }
                }
            });
        };
        $scope.deleteTag = function (id, topicId) {
            bootbox.dialog({
                message: "Are you Sure?",
                title: "Delete Keyword",
                buttons: {
                    success: {
                        label: "Delete",
                        className: "btn-danger",
                        callback: function (response) {
                            dataService.removeTag(id, topicId)
                                .then(function() {
                                    //Success
                                    location.reload(true);
                                    toastr.success("Keyword Deleted Successfully!");
                                }, function() {
                                    //Error
                                    toastr.error("Error Deleting Keyword!");
                                });
                        }
                    },
                    danger: {
                        label: "Cancel",
                        className: "btn-info",
                        callback: function () {

                        }
                    }
                }
            });
        }
    }
}());

// fileDownloadTopicController.js

(function () {
    "strict";

    angular.module("topicsApp").controller("fileDownloadTopicController", fileDownloadTopicController);

    function fileDownloadTopicController($scope, dataService, $http, $routeParams) {
        //toastr.success("Download File's View loaded");

        $scope.files = null;
        var data = null;
        dataService.getFiles($routeParams.id)
                   .then(function (files) {
                       //Success
                       $scope.files = files;
                       data = files[0].fileData;
                       toastr.success("Files Fetched Successfully");
                   }, function () {
                       //Error
                       toastr.error("Error in File Fetching");
                   });


    }
}());