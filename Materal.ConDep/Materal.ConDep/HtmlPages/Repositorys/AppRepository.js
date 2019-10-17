"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Repositories;
        (function (Repositories) {
            var AppRepository = /** @class */ (function () {
                function AppRepository() {
                }
                AppRepository.prototype.GetAppList = function (success) {
                    ConDep.Scripts.Common.sendGet("App/GetAppList", null, success);
                };
                AppRepository.prototype.StartAllApp = function (success) {
                    ConDep.Scripts.Common.sendGet("App/StartAllApp", null, success);
                };
                AppRepository.prototype.RestartAllApp = function (success) {
                    ConDep.Scripts.Common.sendGet("App/RestartAllApp", null, success);
                };
                AppRepository.prototype.StopAllApp = function (success) {
                    ConDep.Scripts.Common.sendGet("App/StopAllApp", null, success);
                };
                AppRepository.prototype.AddApp = function (data, success) {
                    ConDep.Scripts.Common.sendPost("App/AddApp", data, success);
                };
                AppRepository.prototype.EditApp = function (data, success) {
                    ConDep.Scripts.Common.sendPost("App/EditApp", data, success);
                };
                AppRepository.prototype.DeleteApp = function (data, success) {
                    ConDep.Scripts.Common.sendGet("App/DeleteApp?id=" + data, null, success);
                };
                AppRepository.prototype.GetAppInfo = function (data, success) {
                    ConDep.Scripts.Common.sendGet("App/GetAppInfo?id=" + data, null, success);
                };
                AppRepository.prototype.StartApp = function (data, success) {
                    ConDep.Scripts.Common.sendGet("App/StartApp?id=" + data, null, success);
                };
                AppRepository.prototype.RestartApp = function (data, success) {
                    ConDep.Scripts.Common.sendGet("App/RestartApp?id=" + data, null, success);
                };
                AppRepository.prototype.StopApp = function (data, success) {
                    ConDep.Scripts.Common.sendGet("App/StopApp?id=" + data, null, success);
                };
                AppRepository.prototype.GetConsoleList = function (data, success, fail) {
                    ConDep.Scripts.Common.sendGet("App/GetConsoleList?id=" + data, null, success, fail);
                };
                return AppRepository;
            }());
            Repositories.AppRepository = AppRepository;
        })(Repositories = ConDep.Repositories || (ConDep.Repositories = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
