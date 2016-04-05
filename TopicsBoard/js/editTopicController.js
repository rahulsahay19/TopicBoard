// editTopicController.js

(function () {
    "use strict";

    angular.module("topicsApp").controller("editTopicController", editTopicController);

    function editTopicController($scope, dataService, $window, $routeParams) {
        toastr.success("Edit Article View loaded");

        //Initialize the Topic and Topic Id
        $scope.topic = null;
        $scope.id = null;
        $scope.isBusy = true;
        //Fetch the Topic by Id
        dataService.getTopicById($routeParams.id)
            .then(function (topic) {
                //Success
                $scope.topic = topic;
            }, function () {
                //Error
                toastr.error("Error Fetching Article with Id:- " + $routeParams.id);
            });

        //Save function
        //TODO:- This also has to take care of individual image uploads
        $scope.save = function () {
            dataService.editTopic($scope.topic)
                .then(function () {
                    //Success

                    toastr.info("Article Modified Successfully");
                    $window.location = "#/";
                }, function () {
                    //Error

                    toastr.error("Error occured while modifying the article");
                });
        };
    }
})();