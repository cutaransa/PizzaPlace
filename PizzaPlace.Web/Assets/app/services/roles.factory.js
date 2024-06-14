"use strict";

app.factory('RolesFactory', RolesFactory);

RolesFactory.$inject = ['$filter', '$http'];

function RolesFactory($filter, $http) {

    var factory = {};
    var serviceUrl = '/api/Roles/';

    factory.getRoles = function () {
        return $http.post(serviceUrl);
    };

    factory.getRole = function (id) {
        var param = {
            id: id
        };
        return $http({ url: serviceUrl + 'detail/', method: 'POST', params: param });
    };

    factory.addRole = function (role) {
        return $http({ url: serviceUrl + 'add/', method: 'POST', data: role });
    };

    factory.updateRole = function (role) {
        return $http({ url: serviceUrl + 'update/', method: 'POST', data: role });
    };

    return factory;
}