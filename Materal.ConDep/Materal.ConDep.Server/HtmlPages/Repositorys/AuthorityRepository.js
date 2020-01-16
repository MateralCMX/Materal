"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Repositories;
        (function (Repositories) {
            var AuthorityRepository = /** @class */ (function () {
                function AuthorityRepository() {
                }
                AuthorityRepository.prototype.Login = function (data, success) {
                    ConDep.Scripts.Common.sendPost("Authority/Login", data, success);
                };
                AuthorityRepository.prototype.Logout = function (token, success) {
                    ConDep.Scripts.Common.sendGet("Authority/Logout?token=" + token, null, success);
                };
                return AuthorityRepository;
            }());
            Repositories.AuthorityRepository = AuthorityRepository;
        })(Repositories = ConDep.Repositories || (ConDep.Repositories = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
