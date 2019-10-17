"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Repositories;
        (function (Repositories) {
            var SystemRepository = /** @class */ (function () {
                function SystemRepository() {
                }
                SystemRepository.prototype.GetSystemName = function (success) {
                    ConDep.Scripts.Common.sendGet("System/GetSystemName", null, success);
                };
                SystemRepository.prototype.GetSystemVersion = function (success) {
                    ConDep.Scripts.Common.sendGet("System/GetSystemVersion", null, success);
                };
                return SystemRepository;
            }());
            Repositories.SystemRepository = SystemRepository;
        })(Repositories = ConDep.Repositories || (ConDep.Repositories = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
