module csCloseOut {
    export class CloseOutFactory {
        public static $inject = ['$http', '$q'];
        apiBase = '/api/CloseOut/';

        constructor(private $http: ng.IHttpService, private $q: ng.IQService) { }

        public getCloseOutOptions() {
            let defer = this.$q.defer<server.CloseOutOption[]>();

            this.$http.get<server.CloseOutOption[]>(this.apiBase + 'CloseOutOptions').then(response => {
                defer.resolve(response.data);
            }).catch(error=> {
                defer.reject(error);
            });

            return defer.promise;
        }

        public static Factory() {
            let factory = (http, q) => new CloseOutFactory(http, q);
            factory.$inject = CloseOutFactory.$inject;
            return factory;
        }
    }
}

(() => {
    angular.module('csCloseOut').factory('CloseOutFactory', csCloseOut.CloseOutFactory.Factory());
})();