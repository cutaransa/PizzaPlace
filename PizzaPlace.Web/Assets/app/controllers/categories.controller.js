
"use strict";

app.controller('CategoriesController', CategoriesController)
   .controller('CategoriesModalController', CategoriesModalController);

CategoriesController.$inject = ['$scope', '$compile', 'CategoriesFactory', 'toaster', '$uibModal', 'uiGridConstants'];
CategoriesModalController.$inject = ['$scope', '$uibModalInstance', 'category', 'CategoriesFactory', 'toaster'];

function CategoriesController($scope, $compile, CategoriesFactory, toaster, $uibModal, uiGridConstants) {

    var vm = this;

    vm.gridCategories = {
        enableSorting: true,
        enableFiltering: true,
        paginationPageSizes: [25, 50, 75, 100],
        paginationPageSize: 25,
        columnDefs: [
            { name: 'Name', field: 'name' },
            {
                field: 'categoryId',
                enableSorting: false,
                enableFiltering: false,
                enableColumnMenu: false,
                displayName: "Options",
                width: '90',
                cellTemplate: '<center><div class="ui-grid-cell-contents">' +
                        '<a href="" ng-click="grid.appScope.vm.editCategory(row.entity.categoryId)" class="grid-action" tooltip-placement="bottom" uib-tooltip="Edit"><i class="fa fa-pencil"></i></a>' +
                    '</div></center>'
            }

        ]
    }

    vm.loadList = function () {
        CategoriesFactory.getCategories().then(
            function (response) {
                vm.gridCategories.data = response.data;
            }
        );
    }

    vm.loadList();

    vm.category = {};

    vm.addCategory = function () {
        vm.category = {};
        vm.open()
    };

    vm.editCategory = function (id) {
        return CategoriesFactory.getCategory(id).then(
            function (response) {
                vm.category = response.data
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
            templateUrl: 'App/CategoriesEditor',
            controller: 'CategoriesModalController',
            controllerAs: 'vm',
            size: size,
            resolve: {
                category: function () {
                    return vm.category;
                }
            }
        });
        modalInstance.result.then(function (response) {
            vm.loadList();
        });
    }

};

function CategoriesModalController($scope, $uibModalInstance, category, CategoriesFactory, toaster) {

    var vm = this;

    vm.category = category;

    if (vm.category.categoryId == null)
        vm.headerTitle = 'Add Category';
    else
        vm.headerTitle = 'Edit Category';

    vm.save = function () {
        if (vm.category.categoryId == null)
            return CategoriesFactory.addCategory(vm.category).then(
                function (response) {
                    toaster.pop('success', "Edit Category", "Successfully added new category.", 5000);
                    $uibModalInstance.close();
                },
                function (error) {
                    vm.errorMessages = error.data;
                    vm.showMessage = true;
                }
            );
        else
            return CategoriesFactory.updateCategory(vm.category).then(
                function (response) {
                    toaster.pop('success', "Edit Category", "Successfully updated category.", 5000);
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
