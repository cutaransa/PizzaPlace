"use strict";

app.controller('PizzaTypesController', PizzaTypesController)
    .controller('PizzaTypesModalController', PizzaTypesModalController);

PizzaTypesController.$inject = ['$scope', '$http', '$location', 'PizzaTypesFactory', '$routeParams', 'toaster', '$uibModal', 'uiGridConstants'];
PizzaTypesModalController.$inject = ['$scope', '$uibModalInstance', 'pizzaType', 'PizzaTypesFactory', 'toaster'];


function PizzaTypesController($scope, $http, $location, PizzaTypesFactory, $routeParams, toaster, $uibModal, uiGridConstants) {

    var vm = this;

    vm.pizzaType = {};

    vm.gridPizzaTypes = {
        enableSorting: true,
        enableFiltering: true,
        paginationPageSizes: [25, 50, 75, 100],
        paginationPageSize: 25,
        columnDefs: [
            { name: 'Code', field: 'code' },
            { name: 'Name', field: 'name' },
            { name: 'Category', field: 'category.name' },
            { name: 'Ingredients', field: 'ingredients' },
            {
                field: 'typeId',
                enableSorting: false,
                enableFiltering: false,
                enableColumnMenu: false,
                displayName: "Options",
                width: '125',
                cellTemplate: '<center><div class="ui-grid-cell-contents">' +
                    '<a href="" ng-click="grid.appScope.vm.editPizzaType(row.entity.typeId)" class="grid-action" tooltip-placement="bottom" uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
                    '</div></center>'
            }
        ]
    }

    vm.loadList = function () {
        PizzaTypesFactory.getPizzaTypes().then(
            function (response) {
                vm.gridPizzaTypes.data = response.data;
            }
        );
    }

    vm.loadList();

    vm.addPizzaType = function () {
        return PizzaTypesFactory.getCategories().then(
            function (response) {
                vm.pizzaType = {};
                vm.pizzaType.categories = response.data
                vm.open();
            },
            function (error) {
                vm.errorMessages = error.data;
                vm.showMessage = true;
            }
        );
    }

    vm.editPizzaType = function (id) {
        return PizzaTypesFactory.getPizzaType(id).then(
            function (response) {
                vm.pizzaType = response.data
                vm.open();
            },
            function (error) {
                vm.errorMessages = error.data;
                vm.showMessage = true;
            }
        );
    }

    vm.open = function () {
        vm.showMessage = false;
        var modalInstance = $uibModal.open({
            animation: true,
            backdrop: 'static',
            templateUrl: 'App/PizzaTypesEditor',
            controller: 'PizzaTypesModalController',
            controllerAs: 'vm',
            size: 'lg',
            resolve: {
                pizzaType: function () {
                    return vm.pizzaType;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.loadList();
        });
    }
    
}

function PizzaTypesModalController($scope, $uibModalInstance, pizzaType, PizzaTypesFactory, toaster) {

    var vm = this;

    vm.pizzaType = pizzaType;

    if (vm.pizzaType.pizzaTypeId == null)
        vm.headerTitle = 'Add Pizza Type';
    else
        vm.headerTitle = 'Edit Pizza Type';

    vm.save = function () {
        if (vm.pizzaType.pizzaTypeId == null)
            return PizzaTypesFactory.addPizzaType(vm.pizzaType).then(
                function (response) {
                    toaster.pop('success', "Edit Pizza Type", "Successfully added new pizza type.", 5000);
                    $uibModalInstance.close();
                },
                function (error) {
                    vm.errorMessages = error.data;
                    vm.showMessage = true;
                }
            );
        else
            return PizzaTypesFactory.updatePizzaType(vm.pizzaType).then(
                function (response) {
                    toaster.pop('success', "Edit Pizza Type", "Successfully updated pizza type.", 5000);
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