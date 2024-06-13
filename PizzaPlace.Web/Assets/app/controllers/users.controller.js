

"use strict";

app.controller('UsersController', UsersController)
   .controller('UsersModalController', UsersModalController);

UsersController.$inject = ['$scope', '$compile', 'UsersFactory', 'toaster', '$uibModal', 'uiGridConstants'];
UsersModalController.$inject = ['$scope', '$uibModalInstance', 'user', 'UsersFactory', 'RolesFactory', 'toaster'];

function UsersController($scope, $compile, UsersFactory, toaster, $uibModal, uiGridConstants) {

    var vm = this;

    vm.gridUsers = {
        enableSorting: true,
        enableFiltering: true,
        paginationPageSizes: [25, 50, 75, 100],
        paginationPageSize: 25,
        columnDefs: [
			{ name: 'Name', field: 'name' },
			{ name: 'Email Address', field: 'login.email' },
			{
			    name: 'Status',
			    field: 'isActive',
			    width: '80',
			    cellTemplate: '<center><div class="ui-grid-cell-contents">' +
						'<span class="label label-{{ row.entity.isActive ? \'info\' : \'default\' }}" ng-click="grid.appScope.vm.toggleStatus(row.entity.administratorId)" style="cursor: pointer" tooltip-placement="bottom" uib-tooltip="{{ row.entity.isActive ? \'Click to set to Inactive\' : \'Click to set to Active\' }}">{{ row.entity.isActive ? \'Active\' : \'Inactive\' }}</span>&nbsp;&nbsp;' +
					'</div></center>'
			},
			{
			    field: 'administratorId',
			    enableSorting: false,
			    enableFiltering: false,
			    displayName: "Options",
			    width: '90',
			    cellTemplate: '<center><div class="ui-grid-cell-contents">' +
						'<a href="" ng-click="grid.appScope.vm.editUser(row.entity.administratorId)" class="grid-action" tooltip-placement="bottom" uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
					'</div></center>'
			}

        ]
    }

    vm.loadList = function () {
        UsersFactory.getUsers().then(
			function (response) {
			    vm.gridUsers.data = response.data;
			}
		);
    }

    vm.loadList();

    vm.toggleStatus = function (id) {
        return UsersFactory.setStatus(id).then(
			function (response) {
			    vm.loadList();
			    toaster.pop('success', "Update Status", "Successfully updated status of user.", 5000);
			},
			function (response) {
			    toaster.pop('error', "Update Status", "Failed to change status.<br/>" + response.data, 5000, 'trustedHtml');
			}
		);
    };

    vm.user = {};

    vm.addUser = function () {
        vm.user = {};
        vm.open();
    }

    vm.editUser = function (id) {
        return UsersFactory.getUser(id).then(
			function (response) {
			    vm.user = response.data;
			    vm.open();
			},
			function (error) {
			    toaster.pop('error', "View User", error.data, 5000);
			}
		);
    }

    vm.open = function (size) {
        vm.showMessage = false;
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            templateUrl: 'App/UsersEditor',
            controller: 'UsersModalController',
            controllerAs: 'vm',
            size: size,
            resolve: {
                user: function () {
                    return vm.user;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.loadList();
        });
    }
};

function UsersModalController($scope, $uibModalInstance, user, UsersFactory, RolesFactory, toaster) {

    var vm = this;

    vm.user = user;

    if (vm.user.administratorId == null) {
        vm.headerTitle = 'Add User';
        UsersFactory.getRoles().then(
		    function (response) {
		        vm.user.roles = response.data;
		    }
        );
    }
    else {
        vm.headerTitle = 'Edit User';
    }

    vm.save = function () {
        if (vm.user.administratorId == null) {
            return UsersFactory.addUser(vm.user).then(
                function (response) {
                    toaster.pop('success', "Add User", "Successfully added user.", 5000);
                    $uibModalInstance.close();
                },
                function (error) {
                    vm.errorMessages = error.data;
                    vm.showMessage = true;
                }
            );
        }
        else {
            return UsersFactory.updateUser(vm.user).then(
                function (response) {
                    toaster.pop('success', "Edit User", "Successfully updated user.", 5000);
                    $uibModalInstance.close();
                },
                function (error) {
                    vm.errorMessages = error.data;
                    vm.showMessage = true;
                }
            );
        }
    }

    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

