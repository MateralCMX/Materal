namespace Materal.ConDep.Scripts {
    class EditApplicationViewModel {
        private _addForm: HTMLFormElement;
        private _inputName: HTMLInputElement;
        private _inputPath: HTMLInputElement;
        private _inputMainModule: HTMLInputElement;
        private _inputParameters: HTMLInputElement;
        private _appRepository: Repositorys.AppRepository;
        private _id: string;
        constructor() {
            this._addForm = document.getElementById("addForm") as HTMLFormElement;
            this._inputName = document.getElementById("inputName") as HTMLInputElement;
            this._inputPath = document.getElementById("inputPath") as HTMLInputElement;
            this._inputMainModule = document.getElementById("inputMainModule") as HTMLInputElement;
            this._inputParameters = document.getElementById("inputParameters") as HTMLInputElement;
            this._appRepository = new Repositorys.AppRepository();
            this._id = Materal.LocationHelper.getUrlParam("id");
            this.bindEvent();
            this.bindAppInfo();
        }
        private bindEvent() {
            this._addForm.addEventListener("submit", event => {
                this.addForm_Submit(event);
            });
        }
        private addForm_Submit(event: Event) {
            const data = {
                ID: this._id,
                AppPath: this._inputPath.value,
                MainModuleName: this._inputMainModule.value,
                Name: this._inputName.value,
                Parameters: this._inputParameters.value
            };
            var success = (result: any) => {
                window.location.href = "/Index";
            };
            this._appRepository.EditApp(data, success);
            event.preventDefault();
        }
        private bindAppInfo(){
            var success = (result: any) => {
                this._inputPath.value = result.Data.AppPath;
                this._inputMainModule.value = result.Data.MainModuleName;
                this._inputParameters.value = result.Data.Parameters;
                this._inputName.value = result.Data.Name;
            };
            this._appRepository.GetAppInfo(this._id, success);
        }
    }
    window.addEventListener("load", () => {
        const viewModel = new EditApplicationViewModel();
    });
}