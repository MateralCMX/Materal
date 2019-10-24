"use strict";
var Materal;
(function (Materal) {
    var MicroFront;
    (function (MicroFront) {
        var Repositories;
        (function (Repositories) {
            var AppRepository = /** @class */ (function () {
                function AppRepository() {
                }
                AppRepository.prototype.GetAppList = function (success) {
                    MicroFront.Scripts.Common.sendGet("App/GetAppList", null, success);
                };
                AppRepository.prototype.StartAllApp = function (success) {
                    MicroFront.Scripts.Common.sendGet("App/StartAllApp", null, success);
                };
                AppRepository.prototype.RestartAllApp = function (success) {
                    MicroFront.Scripts.Common.sendGet("App/RestartAllApp", null, success);
                };
                AppRepository.prototype.StopAllApp = function (success) {
                    MicroFront.Scripts.Common.sendGet("App/StopAllApp", null, success);
                };
                AppRepository.prototype.AddApp = function (data, success) {
                    MicroFront.Scripts.Common.sendPost("App/AddApp", data, success);
                };
                AppRepository.prototype.EditApp = function (data, success) {
                    MicroFront.Scripts.Common.sendPost("App/EditApp", data, success);
                };
                AppRepository.prototype.DeleteApp = function (data, success) {
                    MicroFront.Scripts.Common.sendGet("App/DeleteApp?id=" + data, null, success);
                };
                AppRepository.prototype.GetAppInfo = function (data, success) {
                    MicroFront.Scripts.Common.sendGet("App/GetAppInfo?id=" + data, null, success);
                };
                AppRepository.prototype.StartApp = function (data, success) {
                    MicroFront.Scripts.Common.sendGet("App/StartApp?id=" + data, null, success);
                };
                AppRepository.prototype.RestartApp = function (data, success) {
                    MicroFront.Scripts.Common.sendGet("App/RestartApp?id=" + data, null, success);
                };
                AppRepository.prototype.StopApp = function (data, success) {
                    MicroFront.Scripts.Common.sendGet("App/StopApp?id=" + data, null, success);
                };
                AppRepository.prototype.GetConsoleList = function (data, success, fail) {
                    MicroFront.Scripts.Common.sendGet("App/GetConsoleList?id=" + data, null, success, fail);
                };
                return AppRepository;
            }());
            Repositories.AppRepository = AppRepository;
        })(Repositories = MicroFront.Repositories || (MicroFront.Repositories = {}));
    })(MicroFront = Materal.MicroFront || (Materal.MicroFront = {}));
})(Materal || (Materal = {}));
