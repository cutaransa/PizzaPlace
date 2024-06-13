
"use strict";

app.controller('SignInController', SignInController);

SignInController.$inject = ['$scope', '$rootScope', '$http', '$cookies', '$location', '$routeParams']

function SignInController($scope, $rootScope, $http, $cookies, $location, $routeParams) {

	var vm = this;

	vm.message = $routeParams.message;
	vm.newSignIn = {};
	$rootScope.loggedIn = false;

	vm.signIn = function () {

		vm.showMessage = false;

		var params = "grant_type=password&username=" + vm.newSignIn.username + "&password=" + vm.newSignIn.password;
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


			window.location = '#!/dashboard';

		},
		function (response) {
			vm.message = response.data.error_description.replace(/["']{1}/gi, "");
			vm.showMessage = true;
		});
	}

}