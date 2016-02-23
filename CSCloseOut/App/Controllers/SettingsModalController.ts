module csCloseOut {
    export class SettingsModalController {
        public static $inject = ['$rootScope', '$modalInstance','options'];

        settings: any = {};

        constructor(private $rootScope, private $modalInstance) {
            this.settings = $rootScope.settings || {};
        }

        public done() {
            this.$rootScope.settings = this.settings;
            this.$modalInstance.close();
        }

        public cancel() {
            this.$modalInstance.dismiss('cancel');
        }
    }
}

(() => {
    angular.module('csCloseOut').controller('SettingsModalController', csCloseOut.SettingsModalController);
})(); 