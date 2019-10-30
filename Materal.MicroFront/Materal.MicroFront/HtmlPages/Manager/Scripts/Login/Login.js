"use strict";
var Materal;
(function (Materal) {
    var MicroFront;
    (function (MicroFront) {
        var Scripts;
        (function (Scripts) {
            var LoginViewModel = /** @class */ (function () {
                function LoginViewModel() {
                    this._authorityRepository = new MicroFront.Repositories.AuthorityRepository();
                    this._systemRepository = new MicroFront.Repositories.SystemRepository();
                    this._loginForm = document.getElementById("loginForm");
                    this._inputPassword = document.getElementById("inputPassword");
                    this._systemName = document.getElementById("systemName");
                    this._systemVersion = document.getElementById("systemVersion");
                    this.bindEvents();
                    this.logout();
                    this.bindTitle();
                }
                /**
                 * 退出登录
                 */
                LoginViewModel.prototype.logout = function () {
                    var token = Scripts.Common.getAuthoirtyInfo();
                    if (token != null) {
                        this._authorityRepository.Logout(token, function () {
                            Scripts.Common.removeAuthoirtyInfo();
                        });
                    }
                };
                /**
                 * 绑定事件
                 */
                LoginViewModel.prototype.bindEvents = function () {
                    var _this = this;
                    this._loginForm.addEventListener("submit", function (event) {
                        _this.loginFrom_Submit(event);
                    });
                };
                /**
                 * 绑定标题
                 */
                LoginViewModel.prototype.bindTitle = function () {
                    var _this = this;
                    var getNameSuccess = function (event) {
                        _this._systemName.innerText = event.Data;
                    };
                    var getVersionSuccess = function (event) {
                        _this._systemVersion.innerText = "Materal.MicroFront v" + event.Data;
                    };
                    this._systemRepository.GetSystemName(getNameSuccess);
                    this._systemRepository.GetSystemVersion(getVersionSuccess);
                };
                /**
                 * 登录事件
                 */
                LoginViewModel.prototype.loginFrom_Submit = function (event) {
                    var data = {
                        Password: this._inputPassword.value
                    };
                    var success = function (result) {
                        Scripts.Common.setAuthoirtyInfo(result.Data);
                        window.location.href = "/Manager/UploadApplicationPack.html";
                    };
                    this._authorityRepository.Login(data, success);
                    event.preventDefault();
                };
                return LoginViewModel;
            }());
            window.addEventListener("load", function () {
                new LoginViewModel();
            });
        })(Scripts = MicroFront.Scripts || (MicroFront.Scripts = {}));
    })(MicroFront = Materal.MicroFront || (Materal.MicroFront = {}));
})(Materal || (Materal = {}));
