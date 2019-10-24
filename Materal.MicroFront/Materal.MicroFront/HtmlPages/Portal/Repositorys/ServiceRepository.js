"use strict";
var Materal;
(function (Materal) {
    var MicroFront;
    (function (MicroFront) {
        var Repositories;
        (function (Repositories) {
            var ServiceRepository = /** @class */ (function () {
                function ServiceRepository() {
                }
                ServiceRepository.prototype.GetAppList = function (success) {
                    MicroFront.Scripts.Common.sendPost("Service/GetServiceModel", null, success);
                };
                return ServiceRepository;
            }());
            Repositories.ServiceRepository = ServiceRepository;
        })(Repositories = MicroFront.Repositories || (MicroFront.Repositories = {}));
    })(MicroFront = Materal.MicroFront || (Materal.MicroFront = {}));
})(Materal || (Materal = {}));
