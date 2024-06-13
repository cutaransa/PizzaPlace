
var app = angular.module('PizzaPlace', [
    'ngRoute',
    'ngCookies',
    'ngAnimate',
    'ngTouch',
    'angular-loading-bar',
    'toaster',
    'bootstrap.angular.validation',
    'ui.bootstrap',
    'textAngular',
    'ui.grid',
    'ui.grid.autoResize',
    'ui.grid.pagination',
    'ui.grid.resizeColumns',
    'ui.grid.moveColumns',
    'ui.grid.selection',
    'ui.grid.pinning',   
    'ui.grid.exporter',
    'angular.filter',
    'checklist-model',
    'chart.js',
    'ngMask'
]);


angular.element(document).ready(function () {
    loading_screen.finish();
});
