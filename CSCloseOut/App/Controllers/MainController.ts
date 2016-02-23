module csCloseOut {

    export class MainController {
        static $inject = ['$rootScope', 'CloseOutFactory'];

        codeStack = [];
        closeOutOptions = [];
        currentOptions = [];
        formShowInfo: ng.IFormController;
        settings: any = {};

        constructor(private $rootScope, private CloseOutFactory: csCloseOut.CloseOutFactory) {
            this.settings = angular.copy($rootScope.settings);

            $rootScope.$on('SETTINGS_CHANGED', () => {
                this.settings = angular.copy(this.$rootScope.settings);
            });
        }

        private getCloseOutOptions() {
            this.CloseOutFactory.getCloseOutOptions().then(options => {
                this.closeOutOptions = options;
                this.currentOptions = this.closeOutOptions;
            }).catch(error => {
                console.error(error);
            });
        }

        private getPreviousOptions(previousId, options = this.closeOutOptions) {
            let i = 0, prevOptions;

            for (; i < options.length; i++) {
                if (options[i].id === previousId) {
                    return options;
                } else if (Array.isArray(options[i].children) && options[i].children.length) {
                    prevOptions = this.getPreviousOptions(previousId, options[i].children);
                    if (prevOptions) {
                        return prevOptions;
                    }
                }
            }
        }

        public submitFormInfo() {
            if (!this.formShowInfo.$valid) {
                return;
            }

            this.getCloseOutOptions();
        }

        public selectOption(option: server.CloseOutOption) {
            this.codeStack.push(option);
            if (Array.isArray(option.children) && option.children.length) {
                // show sub codes
                this.currentOptions = option.children;
            } else {
                // submit code
                let code, codes = [];
                this.codeStack.reverse();
                while (code = this.codeStack.pop()) {
                    codes.push(code.code);
                }
                this.currentOptions = angular.copy(this.closeOutOptions);
                console.log('SUBMIT', codes.join(':'));
            }
        }

        public goBack(id?) {
            let previous;
            if (id) {
                for (var i in this.codeStack) {
                    if (this.codeStack[i].id === id) {
                        previous = this.codeStack.splice(i);
                        break;
                    }
                }
            } else {
                previous = this.codeStack.pop();
            }
            if (previous) {
                let prevOptions = this.getPreviousOptions(previous.id);
                if (prevOptions) {
                    this.currentOptions = angular.copy(prevOptions);
                } else {
                    this.currentOptions = angular.copy(this.closeOutOptions);
                }
            }
        }
    }
}

(() => {
    angular.module('csCloseOut').controller('MainController', csCloseOut.MainController);
})(); 