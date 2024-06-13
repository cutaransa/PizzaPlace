
"use strict";

app.factory('DataServiceFactory', DataServiceFactory);

DataServiceFactory.$inject = ['$filter', '$http'];

function DataServiceFactory($filter, $http) {

    var _data = {};

    return {
        data: _data
    }
}