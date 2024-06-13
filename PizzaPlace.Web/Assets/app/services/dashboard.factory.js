
"use strict";

app.factory('DashboardFactory', DashboardFactory);

DashboardFactory.$inject = ['$filter', '$http'];

function DashboardFactory($filter, $http) {

    var factory = {};
    var serviceUrl = '/api/Dashboard/';

    factory.getCardSummary = function () {
        return $http({ url: serviceUrl + 'get-card-summary/', method: 'POST' });
    };
    factory.extractCardSummary = function () {
        return $http({ url: serviceUrl + 'extract-card-summary/', method: 'POST', responseType: 'arraybuffer'  });
    };

    factory.getServiceRequest = function (data) {
        return $http({ url: serviceUrl + 'get-service-request/', method: 'POST', data: data });
    };

    factory.extractServiceRequest = function (data) {
        return $http({ url: serviceUrl + 'extract-service-request/', method: 'POST', data: data, responseType: 'arraybuffer'  });
    };

    factory.getMerchants = function () {
        return $http({ url: serviceUrl + 'get-merchants/', method: 'POST' });
    };

    factory.getBranches = function (id) {
        return $http({ url: serviceUrl + 'get-branches/', method: 'POST', params: { id: id } });
    };

    factory.getMerchantSales = function (id) {
        return $http({ url: serviceUrl + 'get-merchant-sales/', method: 'POST', params: { id: id } });
    };

    factory.extractMerchantSales = function (id) {
        return $http({ url: serviceUrl + 'extract-merchant-sales/', method: 'POST', params: { id: id }, responseType: 'arraybuffer'  });
    };

    factory.getRegisteredCard = function (data) {
        return $http({ url: serviceUrl + 'get-registered-card/', method: 'POST', data: data });
    };

    factory.extractRegisteredCard = function (data) {
        return $http({ url: serviceUrl + 'extract-registered-card/', method: 'POST', data: data, responseType: 'arraybuffer'  });
    };

    factory.getMonthlySales = function (data) {
        return $http({ url: serviceUrl + 'get-monthly-sales/', method: 'POST', data: data });
    };

    factory.extractMonthlySales = function (data) {
        return $http({ url: serviceUrl + 'extract-monthly-sales/', method: 'POST', data: data, responseType: 'arraybuffer' });
    };

    factory.getBillings = function () {
        return $http({ url: serviceUrl + 'get-billings/', method: 'POST' });
    };

    factory.getBillingsReport = function (id) {
        return $http({ url: serviceUrl + 'get-billings-report/', method: 'POST', params: { id: id } });
    };

    factory.extractBillingsReport = function (id) {
        return $http({ url: serviceUrl + 'extract-billings-report/', method: 'POST', params: { id: id }, responseType: 'arraybuffer' });
    };

    factory.getDeliveryReceipt = function (id) {
        return $http({ url: serviceUrl + 'get-delivery-receipt/', method: 'POST', params: { id: id } });
    };

    factory.extractDeliveryReceipt = function (id) {
        return $http({ url: serviceUrl + 'extract-delivery-receipt/', method: 'POST', params: { id: id }, responseType: 'arraybuffer' });
    };



    return factory;
}