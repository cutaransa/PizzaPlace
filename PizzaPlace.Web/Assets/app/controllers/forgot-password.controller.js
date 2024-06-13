"use strict";

app.controller('ForgotPasswordController', ForgotPasswordController);

ForgotPasswordController.$inject = ['$scope', '$http', '$location', '$routeParams', 'toaster']

function ForgotPasswordController($scope, $http, $location, $routeParams, toaster) {

    var vm = this;

    vm.forgotPassword = {};

    vm.submit = function () {
        vm.showMessage = false;

        var params = {
            Username: vm.forgotPassword.username
        };
        $http.post('api/Account/ForgotPassword', params).then(
            function (response) {
                toaster.pop("success", "Forgot Password", "Successfully sent reset password email to your inbox.", 5000);
                window.location = '#!/signIn';
            },
            function (response) {
                vm.message = response.data.error_description.replace(/["']{1}/gi, "");
                vm.showMessage = true;
            }
        );

    }

}