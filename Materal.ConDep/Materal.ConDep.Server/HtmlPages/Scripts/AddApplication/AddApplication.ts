namespace Materal.ConDep.Scripts {
    class AddApplicationViewModel {
        private _addForm: HTMLFormElement;
        private _inputName: HTMLInputElement;
        private _inputPath: HTMLInputElement;
        private _inputMainModule: HTMLInputElement;
        private _inputParameters: HTMLInputElement;
        private _appRepository: Repositories.AppRepository;
        constructor() {
            this._addForm = document.getElementById("addForm") as HTMLFormElement;
            this._inputName = document.getElementById("inputName") as HTMLInputElement;
            this._inputPath = document.getElementById("inputPath") as HTMLInputElement;
            this._inputMainModule = document.getElementById("inputMainModule") as HTMLInputElement;
            this._inputParameters = document.getElementById("inputParameters") as HTMLInputElement;
            this._appRepository = new Repositories.AppRepository();
            this.bindEvent();
        }
        private bindEvent() {
            this._addForm.addEventListener("submit", event => {
                this.addForm_Submit(event);
            });
        }
        private addForm_Submit(event: Event) {
            const data = {
                AppPath: this._inputPath.value,
                MainModuleName: this._inputMainModule.value,
                Name: this._inputName.value,
                Parameters: this._inputParameters.value
            };
            var success = (result: any) => {
                window.location.href = "/Index";
            };
            this._appRepository.AddApp(data, success);
            event.preventDefault();
        }
    }
    window.addEventListener("load", () => {
        const viewModel = new AddApplicationViewModel();
    });
}