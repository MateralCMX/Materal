"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Scripts;
        (function (Scripts) {
            var AddApplicationViewModel = /** @class */ (function () {
                function AddApplicationViewModel() {
                    this._addForm = document.getElementById("addForm");
                    this._inputName = document.getElementById("inputName");
                    this._inputPath = document.getElementById("inputPath");
                    this._inputMainModule = document.getElementById("inputMainModule");
                    this._inputParameters = document.getElementById("inputParameters");
                    this._appRepository = new ConDep.Repositorys.AppRepository();
                    this.bindEvent();
                }
                AddApplicationViewModel.prototype.bindEvent = function () {
                    var _this = this;
                    this._addForm.addEventListener("submit", function (event) {
                        _this.addForm_Submit(event);
                    });
                };
                AddApplicationViewModel.prototype.addForm_Submit = function (event) {
                    var data = {
                        AppPath: this._inputPath.value,
                        MainModuleName: this._inputMainModule.value,
                        Name: this._inputName.value,
                        Parameters: this._inputParameters.value
                    };
                    var success = function (result) {
                        window.location.href = "/Index";
                    };
                    this._appRepository.AddApp(data, success);
                    event.preventDefault();
                };
                return AddApplicationViewModel;
            }());
            window.addEventListener("load", function () {
                var viewModel = new AddApplicationViewModel();
            });
        })(Scripts = ConDep.Scripts || (ConDep.Scripts = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
