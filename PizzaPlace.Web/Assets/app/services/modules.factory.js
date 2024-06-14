
"use strict";

app.factory('ModulesFactory', ModulesFactory);

ModulesFactory.$inject = ['$filter', '$http'];

function ModulesFactory($filter, $http) {

    var factory = {};
    var serviceUrl = '/api/Modules/';

    factory.getModules = function () {
        return $http.post(serviceUrl);
    };

    factory.getModule = function (id) {
        var param = {
            id: id
        };
        return $http({ url: serviceUrl + 'detail/', method: 'POST', params: param });
    };

    factory.addModule = function (module) {
        return $http({ url: serviceUrl + 'add/', method: 'POST', data: module });
    };

    factory.updateModule = function (module) {
        return $http({ url: serviceUrl + 'update/', method: 'POST', data: module });
    };

    return factory;
}