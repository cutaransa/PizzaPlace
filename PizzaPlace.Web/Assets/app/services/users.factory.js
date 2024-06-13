
"use strict";

app.factory('UsersFactory', UsersFactory);

UsersFactory.$inject = ['$filter', '$http'];

function UsersFactory($filter, $http) {

    var factory = {};
    var serviceUrl = '/api/Users/';


    factory.getUsers = function () {
        return $http.post(serviceUrl);
    };

    factory.setStatus = function (id) {
        var param = {
            id: id
        };
        return $http({ url: serviceUrl + 'set-status/', method: 'POST', params: param });
    };

    factory.getUser = function (id) {
        var param = {
            id: id
        };
        return $http({ url: serviceUrl + 'detail/', method: 'POST', params: param });
    };


    factory.addUser = function (user) {
        return $http({ url: serviceUrl + 'add/', method: 'POST', data: user });
    };

    factory.updateUser = function (user) {
        return $http({ url: serviceUrl + 'update/', method: 'POST', data: user });
    };

    factory.getRoles = function () {
        return $http({ url: serviceUrl + 'roles/', method: 'POST' });
    };

    return factory;
}