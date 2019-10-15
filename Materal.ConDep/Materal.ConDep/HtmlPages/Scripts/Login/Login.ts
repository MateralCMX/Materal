namespace Materal.ConDep.Scripts {
    class LoginViewModel {
        private authorityRepository: Repositorys.AuthorityRepository;
        private loginForm: HTMLFormElement;
        private inputPassword: HTMLInputElement;
        private btnLogin: HTMLButtonElement;
        constructor() {
            this.authorityRepository = new Repositorys.AuthorityRepository();
            this.loginForm = document.getElementById("loginForm") as HTMLFormElement;
            this.inputPassword = document.getElementById("inputPassword") as HTMLInputElement;
            this.btnLogin = document.getElementById("btnLogin") as HTMLButtonElement;
            this.bindEvents();
        }
        /**
         * 绑定事件
         */
        private bindEvents() {
            this.loginForm.addEventListener("submit", event => {
                this.btnLogin_Click(event);
            });
        }
        /**
         * 登录事件
         */
        private btnLogin_Click(event: Event) {
            var data = {
                Password: this.inputPassword.value
            };
            var success = function (result: any) {
                Scripts.Common.setAuthoirtyInfo(result.Data);
                window.location.href = "/Index";
            }
            this.authorityRepository.Login(data, success);
            event.preventDefault();
        }
    }
    window.addEventListener("load", () => {
        const viewModel = new LoginViewModel();
    });
}