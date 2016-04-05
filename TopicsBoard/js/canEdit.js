//canEdit.js

(function () {

    "use strict";

    angular.module("topicsApp").factory("canEdit", canEdit);

    function canEdit($scope) {
        $scope.canEdit = canEdit;
        alert("in here");
    }
}());