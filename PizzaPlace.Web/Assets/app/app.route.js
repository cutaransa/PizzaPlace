 
app.config(['$provide', '$routeProvider', '$httpProvider', function ($provide, $routeProvider, $httpProvider) {
    $routeProvider.when('/signin/:message?', {
        title: 'Sign In',
        templateUrl: 'App/SignIn',
        controller: 'SignInController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/reset-password', {
        title: 'Reset Password',
        templateUrl: 'App/ResetPassword',
        controller: 'ResetPasswordController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/forgot-password', {
        title: 'Forgot Password',
        templateUrl: 'App/ForgotPassword',
        controller: 'ForgotPasswordController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/dashboard', {
        title: 'Dashboard',
        templateUrl: 'App/Dashboard',
        controller: 'DashboardController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/categories', {
        title: 'Categories',
        templateUrl: 'App/Categories',
        controller: 'CategoriesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/modules', {
        title: 'Modules',
        templateUrl: 'App/Modules',
        controller: 'ModulesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/pizzas', {
        title: 'Pizzas',
        templateUrl: 'App/Pizzas',
        controller: 'PizzasController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/types', {
        title: 'Types',
        templateUrl: 'App/PizzaTypes',
        controller: 'PizzaTypesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/roles' , {
        title: 'Roles',
        templateUrl: 'App/Roles',
        controller: 'RolesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/users', {
        title: 'Users',
        templateUrl: 'App/Users',
        controller: 'UsersController',
        controllerAs: 'vm'
    });
    $routeProvider.otherwise({
        redirectTo: '/signin'
    });

    //=============================================================================================
    // Ignore Template Request errors if a page that was requested was not found or unauthorized.  The GET operation could still show up in the browser debugger, but it shouldn't show a $compile:tpload error.
    //=============================================================================================
    $provide.decorator('$templateRequest', ['$delegate', function ($delegate) {
        var mySilentProvider = function (tpl, ignoreRequestError) {
            return $delegate(tpl, true);
        }
        return mySilentProvider;
    }]);

    //=============================================================================================
    // Add an interceptor for AJAX errors
    //=============================================================================================
    $httpProvider.interceptors.push(['$q', '$location', '$injector', function ($q, $location, $injector) {
        return {
            'responseError': function (response) {
                if (response.status === 401) {
                    $location.url('/signin?expired');
                }
                //else if (response.status === 400) {
                //    if (!$location.$$path === '/forgot-password' || !$location.$$path === '/reset-password' || !$location.$$path === '/verify-account:id' || !$location.$$path === '/raffle-entry' || !$location.$$path === '/attendance:id') {
                //        $location.url('/signin');
                //    }
                //}
                else if (response.status === 403) {
                    var toaster = $injector.get('toaster');
                    toaster.pop('error', 'Unauthorized Access', 'You are unauthorized to access this resource.', 5000);
                }

                return $q.reject(response);
            }
        };
    }]);
}]);
