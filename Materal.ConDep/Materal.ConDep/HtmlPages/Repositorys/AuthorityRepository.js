"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Repositorys;
        (function (Repositorys) {
            var AuthorityRepository = /** @class */ (function () {
                function AuthorityRepository() {
                }
                AuthorityRepository.prototype.Login = function (data, success) {
                    ConDep.Scripts.Common.sendPost("Authority/Login", data, success);
                };
                return AuthorityRepository;
            }());
            Repositorys.AuthorityRepository = AuthorityRepository;
        })(Repositorys = ConDep.Repositorys || (ConDep.Repositorys = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
