
"use strict";

app.controller('DashboardController', DashboardController)
    .controller('DashboardModalController', DashboardModalController);

DashboardController.$inject = ['$scope', '$compile', 'DashboardFactory', 'toaster', '$uibModal', 'uiGridConstants'];
DashboardModalController.$inject = ['$scope', '$uibModalInstance', 'dashboard', 'DashboardFactory', 'toaster'];

function DashboardController($scope, $compile, DashboardFactory, toaster, $uibModal, uiGridConstants) {

    var vm = this;

    vm.gridDashboards = {
        enableSorting: true,
        enableFiltering: true,
        paginationPageSizes: [25, 50, 75, 100],
        paginationPageSize: 25,
        columnDefs: [
            { displayName: 'order_details_id', field: 'fileDetailId' },
            { displayName: 'order_id', field: 'order.fileOrderId' },
            {
                displayName: 'order_date',
                field: 'order.createdDate',
                width: '160',
                cellFilter: 'date:"MMM dd, yyyy"',
                filterCellFiltered: true,
                cellTemplate: '<div class="ui-grid-cell-contents">' + '{{ row.entity.order.createdDate | date : \'MMM dd, yyyy h:mm a\' }}' + '</div>'
            },
            { displayName: 'pizza_id', field: 'pizza.code' },
            { displayName: 'pizza_type_id', field: 'pizza.pizzaType.code' },
            { displayName: 'size', field: 'pizza.size' },
            { displayName: 'price', field: 'pizza.price' },
            { displayName: 'name', field: 'pizza.pizzaType.name' },
            { displayName: 'category', field: 'pizza.pizzaType.category.name' },
            { displayName: 'ingredients', field: 'pizza.pizzaType.ingredients' },
            {
                field: 'detailId',
                enableSorting: false,
                enableFiltering: false,
                enableColumnMenu: false,
                displayName: "",
                width: '60',
                cellTemplate: '<center><div class="ui-grid-cell-contents">' +
                    '<a href="" ng-click="grid.appScope.vm.viewDashboard(row.entity.detailId)" class="grid-action" tooltip-placement="bottom" uib-tooltip="Edit"><i class="fa fa-eye"></i></a>' +
                    '</div></center>'
            }

        ]
    }

    vm.loadList = function () {
        DashboardFactory.getAllTransactions().then(
            function (response) {
                vm.gridDashboards.data = response.data;
            }
        );
    }

    vm.loadList();

    vm.dashboard = {};

    vm.viewDashboard = function (id) {
        return DashboardFactory.getTransaction(id).then(
            function (response) {
                vm.dashboard = response.data
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
            templateUrl: 'App/DashboardEditor',
            controller: 'DashboardModalController',
            controllerAs: 'vm',
            size: size,
            resolve: {
                dashboard: function () {
                    return vm.dashboard;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.loadList();
        });
    }

};


function DashboardModalController($scope, $uibModalInstance, dashboard, DashboardFactory, toaster) {

    var vm = this;

    vm.dashboard = dashboard;

    vm.headerTitle = 'View Transaction Detail';

    vm.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

