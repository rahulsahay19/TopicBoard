//fileUploadTopicController.js

(function () {

    "use strict";

    angular.module("topicsApp").controller("fileUploadTopicController", fileUploadTopicController);

    function fileUploadTopicController($scope, $routeParams, dataService, $window) {
        $scope.fileList = [];
        $scope.curFile;
        $scope.ImageProperty = {
            file: ''
        }

        var topicId = $routeParams.id;
        $scope.setFile = function (element) {
            $scope.fileList = [];
            // get the files
            var files = element.files;
            for (var i = 0; i < files.length; i++) {
                $scope.ImageProperty.file = files[i];

                $scope.fileList.push($scope.ImageProperty);
                $scope.ImageProperty = {};
                $scope.$apply();

            }
        }

        $scope.UploadFile = function () {

            for (var i = 0; i < $scope.fileList.length; i++) {

                $scope.UploadFileIndividual($scope.fileList[i].file,
                                            $scope.fileList[i].file.name,
                                            $scope.fileList[i].file.type,
                                            $scope.fileList[i].file.size,
                                            i);
            }

        }

        $scope.UploadFileIndividual = function (fileToUpload, name, type, size, index) {
            //Create XMLHttpRequest Object
            var reqObj = new XMLHttpRequest();

            //event Handler
            reqObj.upload.addEventListener("progress", uploadProgress, false)
            reqObj.addEventListener("load", uploadComplete, false)
            reqObj.addEventListener("error", uploadFailed, false)
            reqObj.addEventListener("abort", uploadCanceled, false)


            //open the object and set method of call(get/post), url to call, isasynchronous(true/False)
            console.log($routeParams.id);
            reqObj.open("POST", "ImageUpload/UploadFiles/?id=" + $routeParams.id, true);

            //set Content-Type at request header.For file upload it's value must be multipart/form-data
            reqObj.setRequestHeader("Content-Type", "multipart/form-data");

            //Set Other header like file name,size and type
            reqObj.setRequestHeader('X-File-Name', name);
            reqObj.setRequestHeader('X-File-Type', type);
            reqObj.setRequestHeader('X-File-Size', size);


            // send the file
            reqObj.send(fileToUpload);

            function uploadProgress(evt) {
                if (evt.lengthComputable) {

                    var uploadProgressCount = Math.round(evt.loaded * 100 / evt.total);

                    document.getElementById('P' + index).innerHTML = uploadProgressCount;

                    if (uploadProgressCount == 100) {
                        document.getElementById('P' + index).innerHTML =
                       '<i class="icon-refresh icon-spin" style="color:maroon;"></i>';
                    }

                }
            }

            function uploadComplete(evt) {
                /* This event is raised when the server  back a response */

                document.getElementById('P' + index).innerHTML = 'Saved';
                toastr.info("File Uploaded Successfully");
                $window.location = "#/topic/" + $routeParams.id;
                $scope.NoOfFileSaved++;
                $scope.$apply();
            }

            function uploadFailed(evt) {
                document.getElementById('P' + index).innerHTML = 'Upload Failed..';
            }

            function uploadCanceled(evt) {

                document.getElementById('P' + index).innerHTML = 'Cancelled....';
            }

        }

        //Get the Topic Name 

        dataService.getTopicById($routeParams.id)
            .then(function (topic) {
                //Success
                $scope.topic = topic;
            }, function () {
                //Error
                toastr.error("Error Fetching Topic with Id:- " + $routeParams.id);
            });
    };


})();