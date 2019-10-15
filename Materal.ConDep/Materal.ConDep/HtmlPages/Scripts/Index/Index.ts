namespace Materal.ConDep.Scripts {
    class IndexViewModel {
        private _appRepository: Repositorys.AppRepository;
        private _dataTableBody: HTMLTableSectionElement;
        private _btnStartAllApp: HTMLInputElement;
        private _btnRestartAllApp: HTMLInputElement;
        private _btnStopAllApp: HTMLInputElement;
        private _consolePanel: HTMLDivElement;
        constructor() {
            this._appRepository = new Repositorys.AppRepository();
            this._dataTableBody = document.getElementById("dataTableBody") as HTMLTableSectionElement;
            this._btnStartAllApp = document.getElementById("btnStartAllApp") as HTMLInputElement;
            this._btnRestartAllApp = document.getElementById("btnRestartAllApp") as HTMLInputElement;
            this._btnStopAllApp = document.getElementById("btnStopAllApp") as HTMLInputElement;
            this._consolePanel = document.getElementById("consolePanel") as HTMLDivElement;
            this.bindEvent();
            this.bindAppList();
        }
        /**
         * 绑定事件
         */
        private bindEvent() {
            this._btnStartAllApp.addEventListener("click", event => { this.btnStartAllApp_Click(); });
            this._btnRestartAllApp.addEventListener("click", event => { this.btnRestartAllApp_Click(); });
            this._btnStopAllApp.addEventListener("click", event => { this.btnStopAllApp_Click(); });
        }
        /**
         * 启动所有应用
         */
        private btnStartAllApp_Click() {
            var success = (result: any) => {
                alert(result.Message);
                this.bindAppList();
            };
            this._appRepository.StartAllApp(success);
        }
        /**
         * 重启所有应用
         */
        private btnRestartAllApp_Click() {
            var success = (result: any) => {
                alert(result.Message);
                this.bindAppList();
            };
            this._appRepository.RestartAllApp(success);
        }
        /**
         * 停止所有应用
         */
        private btnStopAllApp_Click() {
            var success = (result: any) => {
                alert(result.Message);
                this.bindAppList();
            };
            this._appRepository.StopAllApp(success);
        }
        /**
         * 绑定应用列表
         */
        private bindAppList() {
            const success = (result: any) => {
                this.updateDataTableElement(result.Data);
            }
            this._appRepository.GetAppList(success);
        }
        /**
         * 更新数据表
         * @param appList 应用列表
         */
        private updateDataTableElement(appList: Array<any>) {
            this._dataTableBody.innerHTML = "";
            appList.forEach((data: any) => {
                this.updateDataTableRow(data);
            });
        }
        /**
         * 更新数据行
         * @param data 数据
         */
        private updateDataTableRow(data: any) {
            const tdName = document.createElement("td");
            tdName.innerText = data.Name;
            const tdAppStatus = document.createElement("td");
            tdAppStatus.innerText = data.AppStatusText;
            const tdOperation = document.createElement("td");
            const buttonConsole = document.createElement("button");
            buttonConsole.setAttribute("type", "button");
            buttonConsole.setAttribute("data-ID", data.ID);
            buttonConsole.innerText = "控制台";
            buttonConsole.addEventListener("click", event => { this.btnConsole_Click(event); });
            const aEdit = document.createElement("a");
            aEdit.setAttribute("href", `/EditApplication?id=${data.ID}`);
            aEdit.innerText = "编辑";
            const buttonStart = document.createElement("button");
            buttonStart.setAttribute("type", "button");
            buttonStart.setAttribute("data-ID", data.ID);
            buttonStart.innerText = "启动";
            buttonStart.addEventListener("click", event => { this.btnStart_Click(event); });
            const buttonStop = document.createElement("button");
            buttonStop.setAttribute("type", "button");
            buttonStop.setAttribute("data-ID", data.ID);
            buttonStop.innerText = "停止";
            buttonStop.addEventListener("click", event => { this.btnStop_Click(event); });
            const buttonRestart = document.createElement("button");
            buttonRestart.setAttribute("type", "button");
            buttonRestart.setAttribute("data-ID", data.ID);
            buttonRestart.innerText = "重启";
            buttonRestart.addEventListener("click", event => { this.btnRestart_Click(event); });
            const buttonDelete = document.createElement("button");
            buttonDelete.setAttribute("type", "button");
            buttonDelete.setAttribute("data-ID", data.ID);
            buttonDelete.innerText = "删除";
            buttonDelete.addEventListener("click", event => { this.btnDelete_Click(event); });
            tdOperation.appendChild(buttonConsole);
            tdOperation.appendChild(aEdit);
            tdOperation.appendChild(buttonStart);
            tdOperation.appendChild(buttonStop);
            tdOperation.appendChild(buttonRestart);
            tdOperation.appendChild(buttonDelete);
            const tr = document.createElement("tr");
            tr.appendChild(tdName);
            tr.appendChild(tdAppStatus);
            tr.appendChild(tdOperation);
            this._dataTableBody.appendChild(tr);
        }
        /**
         * 查看控制台
         * @param event 
         */
        private btnConsole_Click(event: MouseEvent) {
            const id = (event.target as HTMLButtonElement).getAttribute("data-ID");
            if (id == null) return;
            this._appRepository.GetConsoleList(id, (result: any) => {
                this._consolePanel.innerHTML = "";
                result.Data.forEach((message: string) => {
                    var pMessage = document.createElement("p");
                    pMessage.innerText = message;
                    this._consolePanel.appendChild(pMessage);
                });
            });
        }
        /**
         * 启动
         */
        private btnStart_Click(event: MouseEvent) {
            const id = (event.target as HTMLButtonElement).getAttribute("data-ID");
            if (id == null) return;
            this._appRepository.StartApp(id, (result: any) => {
                alert(result.Message);
                this.bindAppList();
            });
        }
        /**
         * 结束
         */
        private btnStop_Click(event: MouseEvent) {
            const id = (event.target as HTMLButtonElement).getAttribute("data-ID");
            if (id == null) return;
            this._appRepository.StopApp(id, (result: any) => {
                alert(result.Message);
                this.bindAppList();
            });
        }
        /**
         * 重启
         */
        private btnRestart_Click(event: MouseEvent) {
            const id = (event.target as HTMLButtonElement).getAttribute("data-ID");
            if (id == null) return;
            this._appRepository.RestartApp(id, (result: any) => {
                alert(result.Message);
                this.bindAppList();
            });
        }
        /**
         * 删除
         */
        private btnDelete_Click(event: MouseEvent) {
            if (confirm("确定要删除该应用吗？")) {
                const id = (event.target as HTMLButtonElement).getAttribute("data-ID");
                if (id == null) return;
                this._appRepository.DeleteApp(id, (result: any) => {
                    alert(result.Message);
                    this.bindAppList();
                });
            }
        }
    }
    window.addEventListener("load", () => {
        const viewModel = new IndexViewModel();
    });
}