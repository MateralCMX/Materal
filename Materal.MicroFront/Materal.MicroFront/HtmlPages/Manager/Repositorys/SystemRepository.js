"use strict";
var Materal;
(function (Materal) {
    var MicroFront;
    (function (MicroFront) {
        var Repositories;
        (function (Repositories) {
            var SystemRepository = /** @class */ (function () {
                function SystemRepository() {
                }
                SystemRepository.prototype.GetSystemName = function (success) {
                    MicroFront.Scripts.Common.sendGet("System/GetSystemName", null, success);
                };
                SystemRepository.prototype.GetSystemVersion = function (success) {
                    MicroFront.Scripts.Common.sendGet("System/GetSystemVersion", null, success);
                };
                return SystemRepository;
            }());
            Repositories.SystemRepository = SystemRepository;
        })(Repositories = MicroFront.Repositories || (MicroFront.Repositories = {}));
    })(MicroFront = Materal.MicroFront || (Materal.MicroFront = {}));
})(Materal || (Materal = {}));
