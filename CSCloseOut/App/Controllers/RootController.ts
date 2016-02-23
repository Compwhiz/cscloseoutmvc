module csCloseOut {
    export class RootController {
        public static $inject = ['$rootScope', '$modal'];

        constructor(private $rootScope, private $modal) {
            $rootScope.settings = {
                showCodeType: 'SHOWCODE',
                inquiryTypeButtons:'RADIO'
            };
        }

        public openSettingsModal() {
            let modalInstance = this.$modal.open({
                templateUrl: '/App/Views/SettingsModal.html',
                controller: 'SettingsModalController',
                controllerAs: 'ctrl'
            });

            modalInstance.result.then(settings => {
                this.$rootScope.$broadcast('SETTINGS_CHANGED');
            });
        }
    }
}

(() => {
    angular.module('csCloseOut').controller('RootController', csCloseOut.RootController);
})();