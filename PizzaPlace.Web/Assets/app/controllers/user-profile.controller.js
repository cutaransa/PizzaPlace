
"use strict";

app.controller("UserProfileController", UserProfileController)
    .controller("UserProfileModalController", UserProfileModalController)
    .controller("ChangePasswordModalController", ChangePasswordModalController);


UserProfileController.$inject = ['$scope', '$compile', 'UserProfileFactory', '$uibModal', 'toaster'];
UserProfileModalController.$inject = ['$scope', '$uibModalInstance', 'user', 'UserProfileFactory', 'toaster'];
ChangePasswordModalController.$inject = ['$scope', '$uibModalInstance', 'user', 'UserProfileFactory', 'toaster'];

function UserProfileController($scope, $compile, UserProfileFactory, $uibModal, toaster) {

    var vm = this

    $scope.user = {};

    $scope.profile = function () {
        return UserProfileFactory.getUserProfile().then(
            function (response) {
                vm.user = response.data
                vm.open();
            },
            function (error) {
                vm.errorMessages = error.data;
                vm.showMessage = true;
            }
        );
    }

    vm.open = function (size) {
        vm.showMessage = false;
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            templateUrl: 'App/UserProfile',
            controller: 'UserProfileModalController',
            controllerAs: 'vm',
            size: size,
            resolve: {
                user: function () {
                    return vm.user;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.reloadData();
        });
    }

    $scope.changePass = function () {
        vm.user = {};
        vm.openChangePass();
    }

    vm.openChangePass = function (size) {
        vm.showMessage = false;
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            templateUrl: 'App/ChangePassword',
            controller: 'ChangePasswordModalController',
            controllerAs: 'vm',
            size: size,
            resolve: {
                user: function () {
                    return vm.user;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.reloadData();
        });
    }
}

function UserProfileModalController($scope, $uibModalInstance, user, UserProfileFactory, toaster) {

    var vm = this;

    vm.user = user;

    vm.save = function () {
        return UserProfileFactory.updateUser(vm.user).then(
            function (response) {
                toaster.pop('success', "Edit User", "Successfully updated user.", 5000);
                $uibModalInstance.close();
            },
            function (error) {
                vm.errorMessages = error.data;
                vm.showMessage = true;
            }
        );
    };

    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function ChangePasswordModalController($scope, $uibModalInstance, user, UserProfileFactory, toaster) {

    var vm = this;

    vm.user = user;

    vm.changePassword = function () {
        var pass = {
            OldPassword: "" + vm.oldPassword,
            NewPassword: "" + vm.newPassword,
            ConfirmPassword: "" + vm.confirmPassword
        }
        return UserProfileFactory.changePassword(pass).then(
            function (response) {
                toaster.pop('success', "Change Password", "Successfully changed password.", 5000);
                $uibModalInstance.close();
            },
            function (error) {
                toaster.pop('warning', "Error", "" + error.data[0], 5000);
                vm.errorMessages = error.data;
                vm.showMessage = true;
            }
        );
    };

    vm.showAlert = false;
    vm.showSuccess = false;

    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}