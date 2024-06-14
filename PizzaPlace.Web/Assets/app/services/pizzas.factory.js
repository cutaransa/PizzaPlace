
"use strict";

app.factory('PizzasFactory', PizzasFactory);

PizzasFactory.$inject = ['$filter', '$http'];

function PizzasFactory($filter, $http) {

    var factory = {};
    var serviceUrl = '/api/Pizzas/';

    factory.getPizzas = function () {
        return $http.post(serviceUrl);
    };

    factory.getPizza = function (id) {
        var param = {
            id: id
        };
        return $http({ url: serviceUrl + 'detail/', method: 'POST', params: param });
    };

    factory.addPizza = function (pizza) {
        return $http({ url: serviceUrl + 'add/', method: 'POST', data: pizza });
    };

    factory.updatePizza = function (pizza) {
        return $http({ url: serviceUrl + 'update/', method: 'POST', data: pizza });
    };
    
    factory.getTypes = function () {
        return $http.post(serviceUrl + 'types/');
    };

    return factory;
}