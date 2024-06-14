"use strict";

app.controller('PizzasController', PizzasController)
    .controller('PizzasModalController', PizzasModalController);

PizzasController.$inject = ['$scope', '$http', '$location', 'PizzasFactory', '$routeParams', 'toaster', '$uibModal', 'uiGridConstants'];
PizzasModalController.$inject = ['$scope', '$uibModalInstance', 'pizza', 'PizzasFactory', 'toaster'];


function PizzasController($scope, $http, $location, PizzasFactory, $routeParams, toaster, $uibModal, uiGridConstants) {

    var vm = this;

    vm.pizza = {};

    vm.gridPizzas = {
        enableSorting: true,
        enableFiltering: true,
        paginationPageSizes: [25, 50, 75, 100],
        paginationPageSize: 25,
        columnDefs: [
            { name: 'Code', field: 'code' },
            { name: 'Type', field: 'pizzaType.name' },
            { name: 'Size', field: 'size' },
            { name: 'Price', field: 'price' },
            {
                field: 'pizzaId',
                enableSorting: false,
                enableFiltering: false,
                enableColumnMenu: false,
                displayName: "Options",
                width: '125',
                cellTemplate: '<center><div class="ui-grid-cell-contents">' +
                    '<a href="" ng-click="grid.appScope.vm.editPizza(row.entity.pizzaId)" class="grid-action" tooltip-placement="bottom" uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
                    '</div></center>'
            }
        ]
    }

    vm.loadList = function () {
        PizzasFactory.getPizzas().then(
            function (response) {
                vm.gridPizzas.data = response.data;
            }
        );
    }

    vm.loadList();

    vm.addPizza = function () {
        return PizzasFactory.getTypes().then(
            function (response) {
                vm.pizza = {};
                vm.pizza.pizzaTypes = response.data
                vm.open();
            },
            function (error) {
                vm.errorMessages = error.data;
                vm.showMessage = true;
            }
        );
    }

    vm.editPizza = function (id) {
        return PizzasFactory.getPizza(id).then(
            function (response) {
                vm.pizza = response.data
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
            templateUrl: 'App/PizzasEditor',
            controller: 'PizzasModalController',
            controllerAs: 'vm',
            size: 'lg',
            resolve: {
                pizza: function () {
                    return vm.pizza;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.loadList();
        });
    }
    
}

function PizzasModalController($scope, $uibModalInstance, pizza, PizzasFactory, toaster) {

    var vm = this;

    vm.pizza = pizza;

    if (vm.pizza.pizzaId == null)
        vm.headerTitle = 'Add Pizza';
    else
        vm.headerTitle = 'Edit Pizza';

    vm.save = function () {
        if (vm.pizza.pizzaId == null)
            return PizzasFactory.addPizza(vm.pizza).then(
                function (response) {
                    toaster.pop('success', "Edit Pizza ", "Successfully added new pizza.", 5000);
                    $uibModalInstance.close();
                },
                function (error) {
                    vm.errorMessages = error.data;
                    vm.showMessage = true;
                }
            );
        else
            return PizzasFactory.updatePizza(vm.pizza).then(
                function (response) {
                    toaster.pop('success', "Edit Pizza ", "Successfully updated pizza.", 5000);
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