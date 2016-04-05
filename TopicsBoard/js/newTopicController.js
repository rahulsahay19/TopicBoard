// newTopicController.js

(function () {
    "use strict";

    angular.module("topicsApp").controller("newTopicController", newTopicController);

    function newTopicController($scope, $window, dataService) {

        $scope.newTopic = {};
        $scope.isBusy = true;
        $scope.newTag = {};
        var tagArray = new Array();
        var displayTag = new Array();
        var data = [];
        //Save function

        //Timeout function to show spinner
        setTimeout(function () {
            $scope.save = function () {
                //Making Spinner On
                $('#loader').show();
                dataService.addTopic($scope.newTopic)
                    .then(function (result) {
                        //Success
                        //This call is for saving the tag while topic creation
                        if (data.length > 1) {
                            //For Collection of Tags
                            dataService.saveTagsWithTopic(result.id, data);
                        } else {
                            //For topic with one tag or none
                            dataService.saveTagWithTopic(result.id, $scope.newTag);
                        }
                        toastr.success("New Article Created!");
                        $window.location = "#/";
                    }, function () {
                        //Error
                        toastr.error("Couldn't Save the New Article");
                    }).then(function () {
                        //Switch Off
                        $('#loader').hide();
                    });
            };
        }, 1000);

        $scope.addTag = function () {
            

            if ($scope.newTag.tagDescription) {
                tagArray.push($scope.newTag);
                //For displaying the collection on screen
                displayTag.push($scope.newTag.tagDescription);
                data = JSON.stringify(tagArray);
                console.log(tagArray);
                console.log(data);
                $("#tagsCollection").html(displayTag + ',');
                $("#tag").val('');
                $scope.newTag = '';
            } else {
                toastr.error("Please fill the keyword");
            }

        }

    }


})();