module csCloseOut {

    export class MainAltController {
        static $inject = ['$rootScope', 'CloseOutFactory', '$modal', 'toastr'];

        codeStack = [];
        closeOutOptions = [];
        currentOptions = [];
        formShowInfo: ng.IFormController;
        settings: any = {};
        notes: string = '';

        constructor(private $rootScope, private CloseOutFactory: csCloseOut.CloseOutFactory, private $modal, private toastr) {
            this.settings = angular.copy($rootScope.settings);

            $rootScope.$on('SETTINGS_CHANGED', () => {
                this.settings = angular.copy(this.$rootScope.settings);
            });

            this.getCloseOutOptions();

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

        private resetCloseOutCodes() {
            this.codeStack = [];
            this.currentOptions = angular.copy(this.closeOutOptions);
            this.notes = '';
        }

        public submitFormInfo() {
            if (!this.formShowInfo.$valid) {
                return;
            }

            this.getCloseOutOptions();
        }

        public submitCode() {
            let code, codes = [];
            this.codeStack.reverse();
            while (code = this.codeStack.pop()) {
                codes.push(code.code);
            }
            let content = 'SUBMIT: ' + codes.join(':');
            if (this.notes) {
                content += '\nNOTES: ' + this.notes;
            }
            this.resetCloseOutCodes();
            this.toastr.success(content);
        }

        public selectOption(option: server.CloseOutOption) {
            this.codeStack.push(option);
            if (Array.isArray(option.children) && option.children.length) {
                // show sub codes
                this.currentOptions = option.children;
            }
            else {
                // submit code
                let code, codes = [];
                this.codeStack.reverse();
                while (code = this.codeStack.pop()) {
                    codes.push(code.code);
                }
                let content = 'SUBMIT: ' + codes.join(':');

                let modalInstance = this.$modal.open({
                    templateUrl: '/App/Views/NotesModal.html',
                    controller: 'NotesModalController',
                    controllerAs: 'ctrl',
                    resolve: {
                        code: () => content
                    }
                });

                modalInstance.result.then(notes => {
                    if (notes) {
                        content += '\nNOTES: ' + notes;
                    }

                    this.resetCloseOutCodes();
                    this.toastr.success(content);
                });
            }
        }

        public goBack(id?) {
            let previous;
            if (id) {
                for (var i in this.codeStack) {
                    if (this.codeStack[i].id === id) {
                        previous = this.codeStack.splice(i);
                        let prevOptions = this.getPreviousOptions(id);
                        if (prevOptions) {
                            this.currentOptions = angular.copy(prevOptions);
                        } else {
                            this.currentOptions = angular.copy(this.closeOutOptions);
                        }
                        return;
                    }
                }
            } else {
                if (previous = this.codeStack.pop()) {
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
}

(() => {
    angular.module('csCloseOut').controller('MainAltController', csCloseOut.MainAltController);
})(); 