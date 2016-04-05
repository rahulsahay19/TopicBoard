//app.js

(function () {

    "use strict";


    //Declared Angular Module
    angular.module("topicsApp", ["ngRoute", "angularUtils.directives.dirPagination"])
        .config(function($routeProvider) {
            $routeProvider.when("/", {
                controller: "homeIndexController",
                templateUrl: "templates/topicsView.html"
            });
            $routeProvider.when("/newTopic", {
                controller: "newTopicController",
                templateUrl: "templates/newTopicView.html"
            });
            $routeProvider.when("/topic/:id", {
                controller: "singleTopicController",
                templateUrl: "templates/singleTopicView.html"
            });
            $routeProvider.when("/editTopic/:id", {
                controller: "editTopicController",
                templateUrl: "templates/editTopicView.html"
            });
            $routeProvider.when("/fileUploadTopic/:id", {
                controller: "fileUploadTopicController",
                templateUrl: "templates/fileUploadTopicView.html"
            });
            $routeProvider.when("/fileDownloadTopic/:id", {
                controller: "fileDownloadTopicController",
                templateUrl: "templates/fileDownloadTopicView.html"
            });
            $routeProvider.when("/downloadFile/:id", {
                controller: "fileDownloadIndividualController"
            });
            /*  $routeProvider.when("/upload", {
                  controller: "imageUploadController",
                  templateUrl: "/templates/uploadFilesView.html"
              });*/
            $routeProvider.otherwise({ redirectTo: "/" });
        });
    
})();


