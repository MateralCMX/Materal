namespace Materal.MicroFront.Scripts {
    class UploadApplicationPackViewModel {
        private _websocket: WebSocktHelper;
        private _inputFile: HTMLInputElement;
        private _btnUpdate: HTMLButtonElement;
        private _progressPanel: HTMLDivElement;
        private _progress: HTMLDivElement;
        constructor() {
            this._inputFile = document.getElementById("inputFile") as HTMLInputElement;
            this._btnUpdate = document.getElementById("btnUpdate") as HTMLButtonElement;
            this._progressPanel = document.getElementById("progressPanel") as HTMLDivElement;
            this._progress = document.getElementById("progress") as HTMLDivElement;
            this._websocket = new WebSocktHelper();
            this.registerEventHandler();
            this.bindEvent();
            this._inputFile.removeAttribute("disabled");
            this._btnUpdate.removeAttribute("disabled");
        }
        private registerEventHandler() {
            this._websocket.registerEventHandler("UploadReadyEventHandler", (event: any) => {
                this.updateProgress(this._params.index);
                this.readerBlob(this._params.index);
            });
            this._websocket.registerEventHandler("UploadEndEventHandler", (event: any) => {
                this.updateProgress(this._progressCount);
                alert("更新完毕");
                this._inputFile.removeAttribute("disabled");
                this._btnUpdate.removeAttribute("disabled");
            });
        }
        private readerBlob(start: number) {
            if (this._params.selectFile == null || this._params.reader == null) return;
            const blob = this._params.selectFile.slice(start, start + this._params.readSize);
            this._params.reader.readAsDataURL(blob);
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
                this._progressPanel.removeAttribute("style");
                this.uploadStart();
            });
        }
        private _params: UploadApplicationPackParams = {
            selectFile: null,
            reader: null,
            index: 0,
            fileSize: 0,
            readSize: 20480
        };
        private uploadStart() {
            if (this._inputFile.files == null) return;
            this._params.selectFile = this._inputFile.files[0];
            this._params.fileSize = this._params.selectFile.size;
            this._params.index = 0;
            this._params.reader = new FileReader();
            this._params.reader.onload = event => {
                this.onFileReaderLoad(event);
            };
            this.getFileMD5(this._params.selectFile, (abstract: any) => {
                if (this._params.selectFile == null) return;
                const commandData = {
                    Size: this._params.fileSize,
                    Name: this._params.selectFile.name,
                    Abstract: abstract
                };
                this._websocket.sendCommand("UploadStartCommandHandler", commandData);
            });
        }
        private onFileReaderLoad(event: ProgressEvent<FileReader>) {
            if (event.target == null || event.target.result == null) return;
            const uploadData = {
                Index: this._params.index,
                Base64Buffer: (event.target.result as string).split(",")[1]
            };
            if (uploadData.Base64Buffer) {
                this._websocket.sendCommand("UploadPartCommandHandler", uploadData);
                this._params.index += event.loaded;
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
        private _progressCount: number = 0;
        private updateProgress(index: number) {
            if (this._progressCount == 0) {
                this._progressCount = this._params.fileSize / 95 * 100;
            }
            const value = (index / this._progressCount * 100).toFixed(0);
            this._progress.setAttribute("aria-valuenow", value);
            this._progress.setAttribute("style", `width: ${value}%;`);
            this._progress.innerText = `${value}%`;
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