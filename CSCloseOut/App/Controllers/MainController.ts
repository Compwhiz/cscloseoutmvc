module csCloseOut {

    export class MainController {
        static $inject = ['CloseOutFactory'];

        codeStack = [];
        closeOutOptions = [];
        currentOptions = [];

        constructor(private CloseOutFactory: csCloseOut.CloseOutFactory) {
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

        public goBack() {
            let previous = this.codeStack.pop();
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