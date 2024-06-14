
"use strict";

app.factory('PizzaTypesFactory', PizzaTypesFactory);

PizzaTypesFactory.$inject = ['$filter', '$http'];

function PizzaTypesFactory($filter, $http) {

    var factory = {};
    var serviceUrl = '/api/PizzaTypes/';

    factory.getPizzaTypes = function () {
        return $http.post(serviceUrl);
    };

    factory.getPizzaType = function (id) {
        var param = {
            id: id
        };
        return $http({ url: serviceUrl + 'detail/', method: 'POST', params: param });
    };

    factory.addPizzaType = function (pizzaType) {
        return $http({ url: serviceUrl + 'add/', method: 'POST', data: pizzaType });
    };

    factory.updatePizzaType = function (pizzaType) {
        return $http({ url: serviceUrl + 'update/', method: 'POST', data: pizzaType });
    };
    
    factory.getCategories = function () {
        return $http.post(serviceUrl + 'categories/');
    };

    return factory;
}