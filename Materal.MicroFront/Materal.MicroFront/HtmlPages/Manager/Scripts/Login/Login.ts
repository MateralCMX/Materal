namespace Materal.MicroFront.Scripts {
    class LoginViewModel {
        private _authorityRepository: Repositories.AuthorityRepository;
        private _systemRepository: Repositories.SystemRepository;
        private _loginForm: HTMLFormElement;
        private _inputPassword: HTMLInputElement;
        private _systemName: HTMLHeadingElement;
        private _systemVersion: HTMLHeadingElement;
        constructor() {
            this._authorityRepository = new Repositories.AuthorityRepository();
            this._systemRepository = new Repositories.SystemRepository();
            this._loginForm = document.getElementById("loginForm") as HTMLFormElement;
            this._inputPassword = document.getElementById("inputPassword") as HTMLInputElement;
            this._systemName = document.getElementById("systemName") as HTMLHeadingElement;
            this._systemVersion = document.getElementById("systemVersion") as HTMLHeadingElement;
            this.bindEvents();
            this.logout();
            this.bindTitle();
        }
        /**
         * 退出登录
         */
        private logout() {
            var token = Common.getAuthoirtyInfo();
            if (token != null) {
                this._authorityRepository.Logout(token, () => {
                    Common.removeAuthoirtyInfo();
                });
            }
        }
        /**
         * 绑定事件
         */
        private bindEvents() {
            this._loginForm.addEventListener("submit", event => {
                this.loginFrom_Submit(event);
            });
        }
        /**
         * 绑定标题
         */
        private bindTitle() {
            var getNameSuccess = (event: any) => {
                this._systemName.innerText = event.Data;
            }
            var getVersionSuccess = (event: any) => {
                this._systemVersion.innerText = `Materal.MicroFront v${event.Data}`;
            }
            this._systemRepository.GetSystemName(getNameSuccess);
            this._systemRepository.GetSystemVersion(getVersionSuccess);
        }
        /**
         * 登录事件
         */
        private loginFrom_Submit(event: Event) {
            var data = {
                Password: this._inputPassword.value
            };
            var success = function (result: any) {
                Scripts.Common.setAuthoirtyInfo(result.Data);
                window.location.href = "/Manager/UploadApplicationPack.html";
            }
            this._authorityRepository.Login(data, success);
            event.preventDefault();
        }
    }
    window.addEventListener("load", () => {
        new LoginViewModel();
    });
}