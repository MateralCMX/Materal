namespace Materal.ConDep.Scripts {
    class UploadApplicationPackViewModel {
        private _websocket: WebSocktHelper;
        private _inputFile: HTMLInputElement;
        private _btnUpdate: HTMLButtonElement;
        constructor() {
            this._inputFile = document.getElementById("inputFile") as HTMLInputElement;
            this._btnUpdate = document.getElementById("btnUpdate") as HTMLButtonElement;
            this._websocket = new WebSocktHelper();
            this.registerEventHandler();
            this.bindEvent();
            this._inputFile.removeAttribute("disabled");
            this._btnUpdate.removeAttribute("disabled");
        }
        private registerEventHandler() {
            this._websocket.registerEventHandler("UploadReadyEventHandler", (event: any) => {
                this.readerBlob(this.params.index);
            });
            this._websocket.registerEventHandler("UploadEndEventHandler", (event: any) => {
                alert("更新完毕");
            });
        }
        private readerBlob(start: number) {
            if (this.params.selectFile == null || this.params.reader == null) return;
            const blob = this.params.selectFile.slice(start, start + this.params.readSize);
            this.params.reader.readAsDataURL(blob);
        };
        private bindEvent() {
            this._btnUpdate.addEventListener("click", event => {
                if (this._inputFile.files == null || this._inputFile.files.length === 0) {
                    event.preventDefault();
                    event.stopPropagation();
                    return;
                }
                this._inputFile.setAttribute("disabled", "disabled");
                this._btnUpdate.setAttribute("disabled", "disabled");
                this.uploadStart();
            });
        }
        private params: UploadApplicationPackParams = {
            selectFile: null,
            reader: null,
            index: 0,
            fileSize: 0,
            readSize: 20480
        };
        private uploadStart() {
            if (this._inputFile.files == null) return;
            this.params.selectFile = this._inputFile.files[0];
            this.params.fileSize = this.params.selectFile.size;
            this.params.index = 0;
            this.params.reader = new FileReader();
            this.params.reader.onload = event => {
                this.onFileReaderLoad(event);
            };
            this.getFileMD5(this.params.selectFile, (abstract: any) => {
                if (this.params.selectFile == null) return;
                const commandData = {
                    Size: this.params.fileSize,
                    Name: this.params.selectFile.name,
                    Abstract: abstract
                };
                this._websocket.sendCommand("UploadStartCommandHandler", commandData);
            });
        }
        private onFileReaderLoad(event: ProgressEvent<FileReader>) {
            if (event.target == null || event.target.result == null) return;
            const uploadData = {
                Index: this.params.index,
                Base64Buffer: (event.target.result as string).split(",")[1]
            };
            if (uploadData.Base64Buffer) {
                this._websocket.sendCommand("UploadPartCommandHandler", uploadData);
                this.params.index += event.loaded;
            } else {
                console.error("Buffer为空");
            }
        }
        private getFileMD5(file: File, callback: Function) {
            const fileReader = new FileReader();
            fileReader.onload = event => {
                const spark = new (window as any)["SparkMD5"].ArrayBuffer();
                if (!event.target) return;
                spark.append(event.target.result);
                const result = spark.end();
                callback(result);
            };
            fileReader.readAsArrayBuffer(file);
        }
    }
    interface UploadApplicationPackParams {
        selectFile: File | null;
        reader: FileReader | null;
        index: number;
        fileSize: number;
        readSize: number;
    }
    window.addEventListener("load", () => {
        const viewModel = new UploadApplicationPackViewModel();
    });
}