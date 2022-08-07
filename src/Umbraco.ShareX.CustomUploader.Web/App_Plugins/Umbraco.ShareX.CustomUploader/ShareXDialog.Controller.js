(function () {
    "use strict";

    function ShareXDialog($scope, $http, mediaResource, notificationsService, navigationService) {

        var vm = this;
        var node = $scope.currentNode;
        $scope.nav = navigationService;
        vm.loaded = false;

        mediaResource.getById(node.id)
            .then(function (data) {
                vm.node = data;
            });

        $http.get("/umbraco/backoffice/api/CustomUploaderResponse/GetCustomUploader/?nodeId=" + node.id)
            .then(function (response) {
                vm.uploaderModel = response.data;
                vm.loaded = true;
            });

        $scope.copyToClipboard = function () {
            navigator.clipboard.writeText(JSON.stringify(vm.uploaderModel));
            notificationsService.info("ShareX Custom Uploader", "The code has been copied to your clipboard.");
            navigationService.hideDialog();
        }
    }

    angular.module("umbraco").controller("ShareXDialog.Controller", ShareXDialog);
})();