
"use strict";

app.controller('RolesController', RolesController)
   .controller('RolesModalController', RolesModalController)
   .controller('RoleModulesViewModalController', RoleModulesViewModalController);

RolesController.$inject = ['$scope', '$compile', 'RolesFactory', 'toaster', '$uibModal', 'uiGridConstants'];
RolesModalController.$inject = ['$scope', '$uibModalInstance', 'role', 'RolesFactory', 'toaster'];
RoleModulesViewModalController.$inject = ['$scope', '$uibModalInstance', 'role', 'RoleModulesFactory', 'toaster'];

function RolesController($scope, $compile, RolesFactory, toaster, $uibModal, uiGridConstants) {

    var vm = this;

    vm.gridRoles = {
        enableSorting: true,
        enableFiltering: true,
        paginationPageSizes: [25, 50, 75, 100],
        paginationPageSize: 25,
        columnDefs: [
            { name: 'Name', field: 'name' },
            {
                field: 'id',
                enableSorting: false,
                enableFiltering: false,
                enableColumnMenu: false,
                displayName: "Options",
                width: '90',
                cellTemplate: '<center><div class="ui-grid-cell-contents">' +
                        '<a href="" ng-click="grid.appScope.vm.editRole(row.entity.id)" class="grid-action" tooltip-placement="bottom" uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
                        '<a href="" ng-click="grid.appScope.vm.viewModules(row.entity)" class="grid-action" tooltip-placement="bottom" uib-tooltip="Show"><i class="fa fa-list"></i></a>' +
                    '</div><center>'
            }

        ]
    }

    vm.loadList = function () {
        RolesFactory.getRoles().then(
            function (response) {
                vm.gridRoles.data = response.data;
            }
        );
    }

    vm.loadList();

    vm.role = {};

    vm.addRole = function () {
        vm.role = {};
        vm.open();
    }

    vm.editRole = function (id) {
        return RolesFactory.getRole(id).then(
            function (response) {
                vm.role = response.data
                vm.open();
            },
            function (error) {
                vm.errorMessages = error.data;
                vm.showMessage = true;
            }
        );
    }

    vm.viewModules = function (role) {
        vm.openModule('lg', role);
    }

    vm.open = function (size) {
        vm.showMessage = false;
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            templateUrl: 'App/RolesEditor',
            controller: 'RolesModalController',
            controllerAs: 'vm',
            size: size,
            resolve: {
                role: function () {
                    return vm.role;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.loadList();
        });
    }

    vm.openModule = function (size, role) {
        vm.showMessage = false;
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            templateUrl: 'App/RoleModulesViewEditor',
            controller: 'RoleModulesViewModalController',
            controllerAs: 'vm',
            size: size,
            resolve: {
                role: function () {
                    return role;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.loadList();
        });
    }
};

function RolesModalController($scope, $uibModalInstance, role, RolesFactory, toaster) {

    var vm = this;

    vm.role = role;

    if (vm.role.id == null)
        vm.headerTitle = 'Add Role';
    else
        vm.headerTitle = 'Edit Role';

    vm.save = function () {
        if (vm.role.id == null)
            return RolesFactory.addRole(vm.role).then(
                function (response) {
                    toaster.pop('success', "Edit Role", "Successfully added new role.", 5000);
                    $uibModalInstance.close();
                },
                function (error) {
                    vm.errorMessages = error.data;
                    vm.showMessage = true;
                }
            );
        else
            return RolesFactory.updateRole(vm.role).then(
                function (response) {
                    toaster.pop('success', "Edit Role", "Successfully updated role.", 5000);
                    $uibModalInstance.close();
                },
                function (error) {
                    vm.errorMessages = error.data;
                    vm.showMessage = true;
                }
            );
    }

    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function RoleModulesViewModalController($scope, $uibModalInstance, role, RoleModulesFactory, toaster) {

    var vm = this;

    vm.role = role;

    vm.loadList = function () {
        RoleModulesFactory.getRolesByRoleId(role.id).then(
            function (response) {
                vm.role.roleModules = response.data;

                // set initial value for vm.selectAll checkbox
                vm.selectAll = true;
                vm.role.roleModules.some(function (value, index, _ary) {
                    if (value.isEnabled == false)
                        vm.selectAll = false;
                });
            }
        );
    }

    vm.loadList();

    vm.updateModuleAccess = function (roleModule) {
        return RoleModulesFactory.updateModuleAccess(roleModule).then(
            function (response) {
                toaster.pop('success', "Update Access", "Successfully updated access to " + roleModule.module.controller + "/" + roleModule.module.action + ".", 5000);
                vm.loadList();
            },
            function (error) {
                vm.errorMessages = error.data;
                vm.showMessage = true;
            }
        );
    }

    vm.toggleAllModuleAccess = function () {
        angular.forEach(vm.role.roleModules, function (roleModule) {
            roleModule.isEnabled = vm.selectAll;
        });

        return RoleModulesFactory.updateModuleAccessAll(vm.role.id, vm.selectAll).then(
            function (response) {
                toaster.pop('success', "Update Access", "Successfully updated all access.", 5000);
                vm.loadList();
            },
            function (error) {
                vm.errorMessages = error.data;
                vm.showMessage = true;
            }
        );
    }

    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}