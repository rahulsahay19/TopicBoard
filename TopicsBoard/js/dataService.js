//dataService.js

(function () {
    "use strict";

    angular.module("topicsApp").factory("dataService", dataService);

    function dataService($http, $q) {

        var _topics = [];
        var _isInit = false;
        var _tags = [];
        var _files = [];
        var _isReady = function () {
            return _isInit;
        }

        var _getTopics = function () {

            var deferred = $q.defer();
            $http.get("api/v1/topics")
                .then(function (result) {
                    //Success
                    angular.copy(result.data, _topics);
                    //Below Piece is commented to avoid caching while deleting and redirecting to the home page
                    //It needs a hard refresh
                    // _isInit = true;
                    deferred.resolve();
                }, function () {
                    //Error
                    deferred.reject();
                });

            return deferred.promise;
        };
        var _addTopic = function (newTopic) {
            debugger;
            var deferred = $q.defer();

            $http.post("api/v1/topics", newTopic)
                .then(function (result) {
                    //Success
                    var newlyCreatedTopic = result.data;
                    //Added to the array collection.
                    _topics.splice(0, 0, newlyCreatedTopic);
                    //Now add the tag from topic form to the tags collection
                    deferred.resolve(newlyCreatedTopic);
                }, function () {
                    //Error
                    deferred.reject();
                });

            return deferred.promise;
        };

        //Private function
        function _findTopic(id) {
            var found = null;

            $.each(_topics, function (i, item) {
                if (item.id == id) {
                    found = item;
                    return false;
                }
            });

            return found;
        }

        var _getTopicById = function (id) {
            var deferred = $q.defer();

            if (_isReady()) {
                var topic = _findTopic(id);

                //if topic is not null
                if (topic) {
                    deferred.resolve(topic);
                } else {
                    deferred.reject();
                }
            } else {
                _getTopics()
                    .then(function () {
                        //Success
                        var topic = _findTopic(id);

                        //if topic is not null
                        if (topic) {
                            deferred.resolve(topic);
                        } else {
                            deferred.reject();
                        }
                    }, function () {
                        //Error
                        deferred.reject();
                    });
            }
            return deferred.promise;
        };
        var _saveTag = function (topic, newTag) {
            debugger;
            var deferred = $q.defer();
            $http.post("api/v1/topics/" + topic.id + "/tags", newTag)
                .then(function (result) {
                    //Success
                    if (topic.tags == null) topic.tags = [];
                    _tags = topic.tags.push(result.data);
                    deferred.resolve(result.data);
                }, function () {
                    //Error
                    deferred.reject();
                });
            return deferred.promise;
        };
        var _removeTopic = function (id) {
            var deferred = $q.defer();
            $http.delete("api/v1/topics/" + id)
                .then(function () {
                    //Success
                    deferred.resolve();
                }, function () {
                    deferred.reject();
                });
            return deferred.promise;
        };
        var _removeTag = function (id, topicId) {
            var deferred = $q.defer();
            $http.delete("api/v1/topics/" + topicId + "/tags/" + id)
            .then(function () {
                //Success
                //TODO: Need to think better tag splicing thing
                //_tags.splice()
                deferred.resolve();
            }, function () {
                //Error
                deferred.reject();
            })
            return deferred.promise;
        }
        var _saveTagWithTopic = function (topic, newTag) {
            debugger;
            var deferred = $q.defer();
            $http.post("api/v1/topics/" + topic + "/tags", newTag)
                .then(function (result) {
                    //Success
                    if (topic.tags == null) topic.tags = [];
                    _tags = topic.tags.push(result.data);
                    deferred.resolve(result.data);
                }, function () {
                    //Error
                    deferred.reject();
                });
            return deferred.promise;
        };
        var _editTopic = function (topic) {
            var deferred = $q.defer();
            $http.put("api/v1/topics/", topic)
                .then(function () {
                    //Success
                    deferred.resolve();
                }, function () {
                    //Error
                    deferred.reject();
                });
            return deferred.promise;
        };
        var _getFiles = function (id) {

            var deferred = $q.defer();
            $http.get("api/v1/files/" + id)
                .then(function (result) {
                    //Success
                    angular.copy(result.data, _files);
                    deferred.resolve(result.data);
                }, function () {
                    //Error
                    deferred.reject();
                });

            return deferred.promise;
        };

        var _saveTagsWithTopic = function (topic, newTag) {
        var deferred = $q.defer();
            $http.post("api/v1/topics/" + topic + "/tagscollection", newTag)
                  .then(function (result) {
                      //Success
                      if (topic.tags == null) topic.tags = [];
                      _tags = topic.tags.push(result.data);
                      console.log(newTag);
                      console.log(result);
                      deferred.resolve(result.data);
                  }, function () {
                      //Error
                      deferred.reject();
                  });
            return deferred.promise;
        };

  
        return {
            topics: _topics,
            getTopics: _getTopics,
            addTopic: _addTopic,
            isReady: _isReady,
            getTopicById: _getTopicById,
            saveTag: _saveTag,
            removeTopic: _removeTopic,
            removeTag: _removeTag,
            saveTagWithTopic: _saveTagWithTopic,
            editTopic: _editTopic,
            getFiles: _getFiles,
            saveTagsWithTopic: _saveTagsWithTopic
        };
    }
}());