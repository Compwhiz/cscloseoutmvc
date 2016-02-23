module csCloseOut {
    export class NotesModalController {
        public static $inject = ['$modalInstance','code'];

        notes: string;

        constructor(private $modalInstance, private code) {

        }

        public submit() {
            this.$modalInstance.close(this.notes);
        }

        public cancel() {
            this.$modalInstance.dismiss('cancel');
        }
    }
}

(() => {
    angular.module('csCloseOut').controller('NotesModalController', csCloseOut.NotesModalController);
})(); 