
"use strict";

app.factory('UserProfileFactory', UserProfileFactory);

UserProfileFactory.$inject = ['$filter', '$http'];

function UserProfileFactory($filter, $http) {

    var factory = {};
    var serviceUrl = '/api/UserProfile/';

    factory.getUserProfile = function () {
        return $http({ url: serviceUrl + 'detail/', method: 'POST' });
    };

    factory.changePassword = function (user) {
        return $http({ url: '/api/Account/ChangePassword/', method: 'POST', data: user });
    };

    factory.updateUser = function (user) {
        return $http({ url: serviceUrl + 'update/', method: 'POST', data: user });
    };

    return factory;
}