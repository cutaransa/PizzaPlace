
"use strict";

app.directive('dateFormat', function () {
    return {
        restrict: 'A',
        scope: {
            ngModel: '='
        },
        link: function (scope) {
            if (scope.ngModel) scope.ngModel = new Date(scope.ngModel);
        }
    }
});

app.directive('exportTable', function () {
    var link = function ($scope, elm, attr) {
        $scope.$on('export-pdf', function (e, d) {
            elm.tableExport({ type: 'pdf', escape: 'false' });
        });
        $scope.$on('export-excel', function (e, d) {
            elm.tableExport({ type: 'excel', escape: false });
        });
        $scope.$on('export-doc', function (e, d) {
            elm.tableExport({ type: 'doc', escape: false });
        });
    }
    return {
        restrict: 'C',
        link: link
    }
});

app.directive('myDirectory', ['$parse', function ($parse) {
    function link(scope, element, attrs) {
        var model = $parse(attrs.myDirectory);
        element.on('change', function (event) {
            scope.data = [];    //Clear shared scope in case user reqret on the selection
            model(scope, { file: event.target.files });

        });
    };

    return {
        link: link
    }
}]);
