"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Scripts;
        (function (Scripts) {
            var IndexViewModel = /** @class */ (function () {
                function IndexViewModel() {
                    this._appRepository = new ConDep.Repositorys.AppRepository();
                    this._dataTableBody = document.getElementById("dataTableBody");
                    this._btnStartAllApp = document.getElementById("btnStartAllApp");
                    this._btnRestartAllApp = document.getElementById("btnRestartAllApp");
                    this._btnStopAllApp = document.getElementById("btnStopAllApp");
                    this._consolePanel = document.getElementById("consolePanel");
                    this.bindEvent();
                    this.bindAppList();
                }
                /**
                 * 绑定事件
                 */
                IndexViewModel.prototype.bindEvent = function () {
                    var _this = this;
                    this._btnStartAllApp.addEventListener("click", function (event) { _this.btnStartAllApp_Click(); });
                    this._btnRestartAllApp.addEventListener("click", function (event) { _this.btnRestartAllApp_Click(); });
                    this._btnStopAllApp.addEventListener("click", function (event) { _this.btnStopAllApp_Click(); });
                };
                /**
                 * 启动所有应用
                 */
                IndexViewModel.prototype.btnStartAllApp_Click = function () {
                    var _this = this;
                    var success = function (result) {
                        alert(result.Message);
                        _this.bindAppList();
                    };
                    this._appRepository.StartAllApp(success);
                };
                /**
                 * 重启所有应用
                 */
                IndexViewModel.prototype.btnRestartAllApp_Click = function () {
                    var _this = this;
                    var success = function (result) {
                        alert(result.Message);
                        _this.bindAppList();
                    };
                    this._appRepository.RestartAllApp(success);
                };
                /**
                 * 停止所有应用
                 */
                IndexViewModel.prototype.btnStopAllApp_Click = function () {
                    var _this = this;
                    var success = function (result) {
                        alert(result.Message);
                        _this.bindAppList();
                    };
                    this._appRepository.StopAllApp(success);
                };
                /**
                 * 绑定应用列表
                 */
                IndexViewModel.prototype.bindAppList = function () {
                    var _this = this;
                    var success = function (result) {
                        _this.updateDataTableElement(result.Data);
                    };
                    this._appRepository.GetAppList(success);
                };
                /**
                 * 更新数据表
                 * @param appList 应用列表
                 */
                IndexViewModel.prototype.updateDataTableElement = function (appList) {
                    var _this = this;
                    this._dataTableBody.innerHTML = "";
                    appList.forEach(function (data) {
                        _this.updateDataTableRow(data);
                    });
                };
                /**
                 * 更新数据行
                 * @param data 数据
                 */
                IndexViewModel.prototype.updateDataTableRow = function (data) {
                    var _this = this;
                    var tdName = document.createElement("td");
                    tdName.innerText = data.Name;
                    var tdAppStatus = document.createElement("td");
                    tdAppStatus.innerText = data.AppStatusText;
                    var tdOperation = document.createElement("td");
                    var buttonConsole = document.createElement("button");
                    buttonConsole.setAttribute("type", "button");
                    buttonConsole.setAttribute("data-ID", data.ID);
                    buttonConsole.innerText = "控制台";
                    buttonConsole.addEventListener("click", function (event) { _this.btnConsole_Click(event); });
                    var aEdit = document.createElement("a");
                    aEdit.setAttribute("href", "/EditApplication?id=" + data.ID);
                    aEdit.innerText = "编辑";
                    var buttonStart = document.createElement("button");
                    buttonStart.setAttribute("type", "button");
                    buttonStart.setAttribute("data-ID", data.ID);
                    buttonStart.innerText = "启动";
                    buttonStart.addEventListener("click", function (event) { _this.btnStart_Click(event); });
                    var buttonStop = document.createElement("button");
                    buttonStop.setAttribute("type", "button");
                    buttonStop.setAttribute("data-ID", data.ID);
                    buttonStop.innerText = "停止";
                    buttonStop.addEventListener("click", function (event) { _this.btnStop_Click(event); });
                    var buttonRestart = document.createElement("button");
                    buttonRestart.setAttribute("type", "button");
                    buttonRestart.setAttribute("data-ID", data.ID);
                    buttonRestart.innerText = "重启";
                    buttonRestart.addEventListener("click", function (event) { _this.btnRestart_Click(event); });
                    var buttonDelete = document.createElement("button");
                    buttonDelete.setAttribute("type", "button");
                    buttonDelete.setAttribute("data-ID", data.ID);
                    buttonDelete.innerText = "删除";
                    buttonDelete.addEventListener("click", function (event) { _this.btnDelete_Click(event); });
                    tdOperation.appendChild(buttonConsole);
                    tdOperation.appendChild(aEdit);
                    tdOperation.appendChild(buttonStart);
                    tdOperation.appendChild(buttonStop);
                    tdOperation.appendChild(buttonRestart);
                    tdOperation.appendChild(buttonDelete);
                    var tr = document.createElement("tr");
                    tr.appendChild(tdName);
                    tr.appendChild(tdAppStatus);
                    tr.appendChild(tdOperation);
                    this._dataTableBody.appendChild(tr);
                };
                /**
                 * 查看控制台
                 * @param event
                 */
                IndexViewModel.prototype.btnConsole_Click = function (event) {
                    var _this = this;
                    var id = event.target.getAttribute("data-ID");
                    if (id == null)
                        return;
                    this._appRepository.GetConsoleList(id, function (result) {
                        _this._consolePanel.innerHTML = "";
                        result.Data.forEach(function (message) {
                            var pMessage = document.createElement("p");
                            pMessage.innerText = message;
                            _this._consolePanel.appendChild(pMessage);
                        });
                    });
                };
                /**
                 * 启动
                 */
                IndexViewModel.prototype.btnStart_Click = function (event) {
                    var _this = this;
                    var id = event.target.getAttribute("data-ID");
                    if (id == null)
                        return;
                    this._appRepository.StartApp(id, function (result) {
                        alert(result.Message);
                        _this.bindAppList();
                    });
                };
                /**
                 * 结束
                 */
                IndexViewModel.prototype.btnStop_Click = function (event) {
                    var _this = this;
                    var id = event.target.getAttribute("data-ID");
                    if (id == null)
                        return;
                    this._appRepository.StopApp(id, function (result) {
                        alert(result.Message);
                        _this.bindAppList();
                    });
                };
                /**
                 * 重启
                 */
                IndexViewModel.prototype.btnRestart_Click = function (event) {
                    var _this = this;
                    var id = event.target.getAttribute("data-ID");
                    if (id == null)
                        return;
                    this._appRepository.RestartApp(id, function (result) {
                        alert(result.Message);
                        _this.bindAppList();
                    });
                };
                /**
                 * 删除
                 */
                IndexViewModel.prototype.btnDelete_Click = function (event) {
                    var _this = this;
                    if (confirm("确定要删除该应用吗？")) {
                        var id = event.target.getAttribute("data-ID");
                        if (id == null)
                            return;
                        this._appRepository.DeleteApp(id, function (result) {
                            alert(result.Message);
                            _this.bindAppList();
                        });
                    }
                };
                return IndexViewModel;
            }());
            window.addEventListener("load", function () {
                var viewModel = new IndexViewModel();
            });
        })(Scripts = ConDep.Scripts || (ConDep.Scripts = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
