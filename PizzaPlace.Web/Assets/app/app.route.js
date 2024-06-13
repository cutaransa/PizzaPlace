 
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
    $routeProvider.when('/billings', {
        title: 'Billings',
        templateUrl: 'App/Billings',
        controller: 'BillingsController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/cards', {
        title: 'Cards',
        templateUrl: 'App/Cards',
        controller: 'CardsController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/card-batches', {
        title: 'CardBatches',
        templateUrl: 'App/CardBatches',
        controller: 'CardBatchesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/card-types', {
        title: 'CardTypes',
        templateUrl: 'App/CardTypes',
        controller: 'CardTypesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/customer-details/:id', {
        title: 'Customer Details',
        templateUrl: 'App/CustomerDetails',
        controller: 'CustomerDetailsController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/customers', {
        title: 'Customers',
        templateUrl: 'App/Customers',
        controller: 'CustomersController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/dashboard', {
        title: 'Dashboard',
        templateUrl: 'App/Dashboard',
        controller: 'DashboardController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/email-campaigns', {
        title: 'Email Campaigns',
        templateUrl: 'App/EmailCampaigns',
        controller: 'EmailCampaignsController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/email-campaigns/editor', {
        title: 'Email Campaigns - Editor',
        templateUrl: '../Assets/app/templates/EmailCampaignsEditor.html',
        controller: 'EmailCampaignsModalController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/machines', {
        title: 'Machines',
        templateUrl: 'App/Machines',
        controller: 'MachinesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/merchants', {
        title: 'Merchants',
        templateUrl: 'App/Merchants',
        controller: 'MerchantsController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/modules', {
        title: 'Modules',
        templateUrl: 'App/Modules',
        controller: 'ModulesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/push-notification-campaigns', {
        title: 'Push Notification Campaigns',
        templateUrl: 'App/PushNotificationCampaigns',
        controller: 'PushNotificationCampaignsController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/push-notification-campaigns/editor', {
        title: 'Push Notification Campaigns - Editor',
        templateUrl: '../Assets/app/templates/PushNotificationCampaignsEditor.html',
        controller: 'PushNotificationCampaignsModalController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/reports', {
        title: 'Reports',
        templateUrl: '../Assets/app/templates/Reports.html',
        controller: 'ReportsController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/requests', {
        title: 'Requests',
        templateUrl: 'App/Requests',
        controller: 'RequestsController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/roles' , {
        title: 'Roles',
        templateUrl: 'App/Roles',
        controller: 'RolesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/sms-campaigns', {
        title: 'Sms Campaigns',
        templateUrl: 'App/SmsCampaigns',
        controller: 'SmsCampaignsController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/sms-campaigns/editor', {
        title: 'Sms Campaigns - Editor',
        templateUrl: '../Assets/app/templates/SmsCampaignsEditor.html',
        controller: 'SmsCampaignsModalController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/sms-inboxes', {
        title: 'Sms Inboxes',
        templateUrl: 'App/SmsInboxes',
        controller: 'SmsInboxesController',
        controllerAs: 'vm'
    });
    $routeProvider.when('/tags', {
        title: 'Tags',
        templateUrl: 'App/Tags',
        controller: 'TagsController',
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
