
app.run(['$http', '$cookies', '$rootScope', '$location', function ($http, $cookies, $rootScope, $location) {
    //If a token exists in the cookie, load it after the app is loaded, so that the application can maintain the authenticated state.
    $http.defaults.headers.common.Authorization = 'Bearer ' + $cookies.get('_Token');
    $http.defaults.headers.common.RefreshToken = $cookies.get('_RefreshToken');
    $rootScope.location = $location;
}]);

//==========================================================================================
//GLOBAL FUNCTIONS - a root/global controller.
//==========================================================================================
app.run(['$rootScope', '$http', '$cookies', function ($rootScope, $http, $cookies) {

    $rootScope.logout = function () {
        $http.post('/api/Account/Logout')
            .then(function (response) {
                $http.defaults.headers.common.Authorization = null;
                $http.defaults.headers.common.RefreshToken = null;
                $cookies.remove('_Token');
                $cookies.remove('_RefreshToken');

                $rootScope.username = '';
                $rootScope.loggedIn = false;

                window.location = '#!/signin';
            });
    }

    $rootScope.$on('$locationChangeSuccess', function (event) {
        if ($http.defaults.headers.common.RefreshToken != null) {
            var params = "grant_type=refresh_token&refresh_token=" + $http.defaults.headers.common.RefreshToken;
            $http({
                url: '/Token',
                method: "POST",
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: params
            })
                .then(function (response) {
                    $http.defaults.headers.common.Authorization = "Bearer " + response.data.access_token;
                    $http.defaults.headers.common.RefreshToken = response.data.refresh_token;

                    $cookies.put('_Token', response.data.access_token);
                    $cookies.put('_RefreshToken', response.data.refresh_token);
                    
                    $http.get('/api/Account/GetCurrentUserName')
                        .then(function (responseData) {
                            if (responseData.data != "null") {
                                $rootScope.username = responseData.data.replace(/["']{1}/gi, "");
                                $rootScope.loggedIn = true;
                            }
                            else {
                                window.location = '/signin?expired';
                                $rootScope.loggedIn = false;
                            }    
                        });
                },
                function (error) {
                    console.log($rootScope.location.$$path);
                    if (!$rootScope.location.$$path === '/signin') {
                        $rootScope.username = '';
                        $rootScope.loggedIn = false;
                        window.location = '#!/signin?expired';
                    }
                }
            );
        }
        else {
            if (!$rootScope.location.$$path === '#!/signin') {
                $rootScope.username = '';
                $rootScope.loggedIn = false;
                window.location = '#!/signin?expired';
            }
        }
    });

    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
        $rootScope.title = current.title;
    });

}]);

app.config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {

    //==========================================================================================
    // Disable spinner on angular-loading-bar
    //==========================================================================================
    cfpLoadingBarProvider.includeSpinner = false;
}]);

//==========================================================================================
// Configure form validation
//==========================================================================================
app.config(['bsValidationConfigProvider', function (bsValidationConfigProvider) {
    bsValidationConfigProvider.global.setValidateFieldsOn('submit');
    bsValidationConfigProvider.global.setDisplayErrorsAs('tooltip');
    bsValidationConfigProvider.global.errorMessagePrefix = '<span class="glyphicon glyphicon-warning-sign"></span> &nbsp;';
    bsValidationConfigProvider.global.tooltipPlacement = 'bottom-right';
}])

app.config(['$qProvider', function ($qProvider) {
    $qProvider.errorOnUnhandledRejections(false);
}]);
