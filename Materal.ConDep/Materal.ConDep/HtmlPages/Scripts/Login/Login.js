"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Scripts;
        (function (Scripts) {
            var LoginViewModel = /** @class */ (function () {
                function LoginViewModel() {
                    this.authorityRepository = new ConDep.Repositorys.AuthorityRepository();
                    this.loginForm = document.getElementById("loginForm");
                    this.inputPassword = document.getElementById("inputPassword");
                    this.btnLogin = document.getElementById("btnLogin");
                    this.bindEvents();
                }
                /**
                 * 绑定事件
                 */
                LoginViewModel.prototype.bindEvents = function () {
                    var _this = this;
                    this.loginForm.addEventListener("submit", function (event) {
                        _this.btnLogin_Click(event);
                    });
                };
                /**
                 * 登录事件
                 */
                LoginViewModel.prototype.btnLogin_Click = function (event) {
                    var data = {
                        Password: this.inputPassword.value
                    };
                    var success = function (result) {
                        Scripts.Common.setAuthoirtyInfo(result.Data);
                        window.location.href = "/Index";
                    };
                    this.authorityRepository.Login(data, success);
                    event.preventDefault();
                };
                return LoginViewModel;
            }());
            window.addEventListener("load", function () {
                var viewModel = new LoginViewModel();
            });
        })(Scripts = ConDep.Scripts || (ConDep.Scripts = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
