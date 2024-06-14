
"use strict";

app.factory('CategoriesFactory', CategoriesFactory);

CategoriesFactory.$inject = ['$filter', '$http'];

function CategoriesFactory($filter, $http) {

    var factory = {};
    var serviceUrl = '/api/Categories/';

    factory.getCategories = function () {
        return $http.post(serviceUrl);
    };

    factory.getCategory = function (id) {
        var param = {
            id: id
        };
        return $http({ url: serviceUrl + 'detail/', method: 'POST', params: param });
    };

    factory.addCategory = function (category) {
        return $http({ url: serviceUrl + 'add/', method: 'POST', data: category });
    };

    factory.updateCategory = function (category) {
        return $http({ url: serviceUrl + 'update/', method: 'POST', data: category });
    };

    return factory;
}