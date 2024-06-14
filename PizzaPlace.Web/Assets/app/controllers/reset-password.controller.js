"use strict";

app.controller('ResetPasswordController', ResetPasswordController);

ResetPasswordController.$inject = ['$scope', '$http', '$location', '$routeParams', 'toaster']

function ResetPasswordController($scope, $http, $location, $routeParams, toaster) {

    var vm = this;

    vm.resetPassword = {};

    vm.submit = function () {
        vm.showMessage = false;

        var params = {
            userId: $routeParams.userId,
            code: $routeParams.code,
            newPassword: vm.resetPassword.newPassword,
            confirmPassword: vm.resetPassword.confirmPassword
        };
        $http.post('api/Account/ResetPassword', params).then(
            function (response) {
                toaster.pop("success", "Reset Password", "Successfully reset your password.", 5000);
                window.location = '#!/signIn';
            },
            function (response) {
                vm.message = response.data.error_description.replace(/["']{1}/gi, "");
                vm.showMessage = true;
            }
        );

    }

}