
"use strict";

app.factory('DashboardFactory', DashboardFactory);

DashboardFactory.$inject = ['$filter', '$http'];

function DashboardFactory($filter, $http) {

    var factory = {};
    var serviceUrl = '/api/Dashboard/';

    factory.getAllTransactions = function () {
        return $http({ url: serviceUrl + 'get-all-transactions/', method: 'POST' });
    }
    
    factory.getTransaction = function (id) {
        var param = {
            id: id
        };
        return $http({ url: serviceUrl + 'get-transaction/', method: 'POST', params: param });
    };

    return factory;
}