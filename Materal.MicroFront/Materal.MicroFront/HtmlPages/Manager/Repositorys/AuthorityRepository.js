"use strict";
var Materal;
(function (Materal) {
    var MicroFront;
    (function (MicroFront) {
        var Repositories;
        (function (Repositories) {
            var AuthorityRepository = /** @class */ (function () {
                function AuthorityRepository() {
                }
                AuthorityRepository.prototype.Login = function (data, success) {
                    MicroFront.Scripts.Common.sendPost("Authority/Login", data, success);
                };
                AuthorityRepository.prototype.Logout = function (token, success) {
                    MicroFront.Scripts.Common.sendGet("Authority/Logout?token=" + token, null, success);
                };
                return AuthorityRepository;
            }());
            Repositories.AuthorityRepository = AuthorityRepository;
        })(Repositories = MicroFront.Repositories || (MicroFront.Repositories = {}));
    })(MicroFront = Materal.MicroFront || (Materal.MicroFront = {}));
})(Materal || (Materal = {}));
