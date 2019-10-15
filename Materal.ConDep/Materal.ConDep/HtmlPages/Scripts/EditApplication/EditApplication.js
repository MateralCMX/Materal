"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Scripts;
        (function (Scripts) {
            var EditApplicationViewModel = /** @class */ (function () {
                function EditApplicationViewModel() {
                    this._addForm = document.getElementById("addForm");
                    this._inputName = document.getElementById("inputName");
                    this._inputPath = document.getElementById("inputPath");
                    this._inputMainModule = document.getElementById("inputMainModule");
                    this._inputParameters = document.getElementById("inputParameters");
                    this._appRepository = new ConDep.Repositorys.AppRepository();
                    this._id = Materal.LocationHelper.getUrlParam("id");
                    this.bindEvent();
                    this.bindAppInfo();
                }
                EditApplicationViewModel.prototype.bindEvent = function () {
                    var _this = this;
                    this._addForm.addEventListener("submit", function (event) {
                        _this.addForm_Submit(event);
                    });
                };
                EditApplicationViewModel.prototype.addForm_Submit = function (event) {
                    var data = {
                        ID: this._id,
                        AppPath: this._inputPath.value,
                        MainModuleName: this._inputMainModule.value,
                        Name: this._inputName.value,
                        Parameters: this._inputParameters.value
                    };
                    var success = function (result) {
                        window.location.href = "/Index";
                    };
                    this._appRepository.EditApp(data, success);
                    event.preventDefault();
                };
                EditApplicationViewModel.prototype.bindAppInfo = function () {
                    var _this = this;
                    var success = function (result) {
                        _this._inputPath.value = result.Data.AppPath;
                        _this._inputMainModule.value = result.Data.MainModuleName;
                        _this._inputParameters.value = result.Data.Parameters;
                        _this._inputName.value = result.Data.Name;
                    };
                    this._appRepository.GetAppInfo(this._id, success);
                };
                return EditApplicationViewModel;
            }());
            window.addEventListener("load", function () {
                var viewModel = new EditApplicationViewModel();
            });
        })(Scripts = ConDep.Scripts || (ConDep.Scripts = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
