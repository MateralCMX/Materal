"use strict";
var Materal;
(function (Materal) {
    var MicroFront;
    (function (MicroFront) {
        var Scripts;
        (function (Scripts) {
            var UploadApplicationPackViewModel = /** @class */ (function () {
                function UploadApplicationPackViewModel() {
                    this._params = {
                        selectFile: null,
                        reader: null,
                        index: 0,
                        fileSize: 0,
                        readSize: 20480
                    };
                    this._progressCount = 0;
                    this._inputFile = document.getElementById("inputFile");
                    this._btnUpdate = document.getElementById("btnUpdate");
                    this._progressPanel = document.getElementById("progressPanel");
                    this._progress = document.getElementById("progress");
                    this._websocket = new Scripts.WebSocktHelper();
                    this.registerEventHandler();
                    this.bindEvent();
                    this._inputFile.removeAttribute("disabled");
                    this._btnUpdate.removeAttribute("disabled");
                }
                UploadApplicationPackViewModel.prototype.registerEventHandler = function () {
                    var _this = this;
                    this._websocket.registerEventHandler("UploadReadyEventHandler", function (event) {
                        _this.updateProgress(_this._params.index);
                        _this.readerBlob(_this._params.index);
                    });
                    this._websocket.registerEventHandler("UploadEndEventHandler", function (event) {
                        _this.updateProgress(_this._progressCount);
                        alert("更新完毕");
                        _this._inputFile.removeAttribute("disabled");
                        _this._btnUpdate.removeAttribute("disabled");
                    });
                };
                UploadApplicationPackViewModel.prototype.readerBlob = function (start) {
                    if (this._params.selectFile == null || this._params.reader == null)
                        return;
                    var blob = this._params.selectFile.slice(start, start + this._params.readSize);
                    this._params.reader.readAsDataURL(blob);
                };
                ;
                UploadApplicationPackViewModel.prototype.bindEvent = function () {
                    var _this = this;
                    this._btnUpdate.addEventListener("click", function (event) {
                        if (_this._inputFile.files == null || _this._inputFile.files.length === 0) {
                            event.preventDefault();
                            event.stopPropagation();
                            return;
                        }
                        _this._inputFile.setAttribute("disabled", "disabled");
                        _this._btnUpdate.setAttribute("disabled", "disabled");
                        _this._progressPanel.removeAttribute("style");
                        _this.uploadStart();
                    });
                };
                UploadApplicationPackViewModel.prototype.uploadStart = function () {
                    var _this = this;
                    if (this._inputFile.files == null)
                        return;
                    this._params.selectFile = this._inputFile.files[0];
                    this._params.fileSize = this._params.selectFile.size;
                    this._params.index = 0;
                    this._params.reader = new FileReader();
                    this._params.reader.onload = function (event) {
                        _this.onFileReaderLoad(event);
                    };
                    this.getFileMD5(this._params.selectFile, function (abstract) {
                        if (_this._params.selectFile == null)
                            return;
                        var commandData = {
                            Size: _this._params.fileSize,
                            Name: _this._params.selectFile.name,
                            Abstract: abstract
                        };
                        _this._websocket.sendCommand("UploadStartCommandHandler", commandData);
                    });
                };
                UploadApplicationPackViewModel.prototype.onFileReaderLoad = function (event) {
                    if (event.target == null || event.target.result == null)
                        return;
                    var uploadData = {
                        Index: this._params.index,
                        Base64Buffer: event.target.result.split(",")[1]
                    };
                    if (uploadData.Base64Buffer) {
                        this._websocket.sendCommand("UploadPartCommandHandler", uploadData);
                        this._params.index += event.loaded;
                    }
                    else {
                        console.error("Buffer为空");
                    }
                };
                UploadApplicationPackViewModel.prototype.getFileMD5 = function (file, callback) {
                    var fileReader = new FileReader();
                    fileReader.onload = function (event) {
                        var spark = new window["SparkMD5"].ArrayBuffer();
                        if (!event.target)
                            return;
                        spark.append(event.target.result);
                        var result = spark.end();
                        callback(result);
                    };
                    fileReader.readAsArrayBuffer(file);
                };
                UploadApplicationPackViewModel.prototype.updateProgress = function (index) {
                    if (this._progressCount == 0) {
                        this._progressCount = this._params.fileSize / 95 * 100;
                    }
                    var value = (index / this._progressCount * 100).toFixed(0);
                    this._progress.setAttribute("aria-valuenow", value);
                    this._progress.setAttribute("style", "width: " + value + "%;");
                    this._progress.innerText = value + "%";
                };
                return UploadApplicationPackViewModel;
            }());
            window.addEventListener("load", function () {
                var viewModel = new UploadApplicationPackViewModel();
            });
        })(Scripts = MicroFront.Scripts || (MicroFront.Scripts = {}));
    })(MicroFront = Materal.MicroFront || (Materal.MicroFront = {}));
})(Materal || (Materal = {}));
