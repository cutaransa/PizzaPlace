
"use strict";

app.controller('ModulesController', ModulesController)
   .controller('ModulesModalController', ModulesModalController);

ModulesController.$inject = ['$scope', '$compile', 'ModulesFactory', 'toaster', '$uibModal', 'uiGridConstants'];
ModulesModalController.$inject = ['$scope', '$uibModalInstance', 'module', 'ModulesFactory', 'toaster'];

function ModulesController($scope, $compile, ModulesFactory, toaster, $uibModal, uiGridConstants) {

    var vm = this;

    vm.gridModules = {
        enableSorting: true,
        enableFiltering: true,
        paginationPageSizes: [25, 50, 75, 100],
        paginationPageSize: 25,
        columnDefs: [
            { name: 'Controller', field: 'controller' },
            { name: 'Action', field: 'action' },
            {
                field: 'moduleId',
                enableSorting: false,
                enableFiltering: false,
                enableColumnMenu: false,
                displayName: "Options",
                width: '90',
                cellTemplate: '<center><div class="ui-grid-cell-contents">' +
                        '<a href="" ng-click="grid.appScope.vm.editModule(row.entity.moduleId)" class="grid-action" tooltip-placement="bottom" uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
                    '</div></center>'
            }

        ]
    }

    vm.loadList = function () {
        ModulesFactory.getModules().then(
            function (response) {
                vm.gridModules.data = response.data;
            }
        );
    }

    vm.loadList();

    vm.module = {};

    vm.addModule = function () {
        vm.module = {};
        vm.open()
    };

    vm.editModule = function (id) {
        return ModulesFactory.getModule(id).then(
            function (response) {
                vm.module = response.data
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
            templateUrl: 'App/ModulesEditor',
            controller: 'ModulesModalController',
            controllerAs: 'vm',
            size: size,
            resolve: {
                module: function () {
                    return vm.module;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.loadList();
        });
    }

};

function ModulesModalController($scope, $uibModalInstance, module, ModulesFactory, toaster) {

    var vm = this;

    vm.module = module;

    if (vm.module.moduleId == null)
        vm.headerTitle = 'Add Module';
    else
        vm.headerTitle = 'Edit Module';

    vm.save = function () {
        if (vm.module.moduleId == null)
            return ModulesFactory.addModule(vm.module).then(
                function (response) {
                    toaster.pop('success', "Edit Module", "Successfully added new module.", 5000);
                    $uibModalInstance.close();
                },
                function (error) {
                    vm.errorMessages = error.data;
                    vm.showMessage = true;
                }
            );
        else
            return ModulesFactory.updateModule(vm.module).then(
                function (response) {
                    toaster.pop('success', "Edit Module", "Successfully updated module.", 5000);
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
