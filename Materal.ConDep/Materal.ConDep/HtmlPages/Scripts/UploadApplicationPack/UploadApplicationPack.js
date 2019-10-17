"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Scripts;
        (function (Scripts) {
            var UploadApplicationPackViewModel = /** @class */ (function () {
                function UploadApplicationPackViewModel() {
                    this.params = {
                        selectFile: null,
                        reader: null,
                        index: 0,
                        fileSize: 0,
                        readSize: 20480
                    };
                    this._inputFile = document.getElementById("inputFile");
                    this._btnUpdate = document.getElementById("btnUpdate");
                    this._websocket = new Scripts.WebSocktHelper();
                    this.registerEventHandler();
                    this.bindEvent();
                    this._inputFile.removeAttribute("disabled");
                    this._btnUpdate.removeAttribute("disabled");
                }
                UploadApplicationPackViewModel.prototype.registerEventHandler = function () {
                    var _this = this;
                    this._websocket.registerEventHandler("UploadReadyEventHandler", function (event) {
                        _this.readerBlob(_this.params.index);
                    });
                    this._websocket.registerEventHandler("UploadEndEventHandler", function (event) {
                        alert("更新完毕");
                    });
                    this._websocket.registerEventHandler("ServerErrorEventHandler", function (event) {
                        console.error(event);
                    });
                };
                UploadApplicationPackViewModel.prototype.readerBlob = function (start) {
                    if (this.params.selectFile == null || this.params.reader == null)
                        return;
                    var blob = this.params.selectFile.slice(start, start + this.params.readSize);
                    this.params.reader.readAsDataURL(blob);
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
                        _this.uploadStart();
                    });
                };
                UploadApplicationPackViewModel.prototype.uploadStart = function () {
                    var _this = this;
                    if (this._inputFile.files == null)
                        return;
                    this.params.selectFile = this._inputFile.files[0];
                    this.params.fileSize = this.params.selectFile.size;
                    this.params.index = 0;
                    this.params.reader = new FileReader();
                    this.params.reader.onload = function (event) {
                        _this.onFileReaderLoad(event);
                    };
                    this.getFileMD5(this.params.selectFile, function (abstract) {
                        if (_this.params.selectFile == null)
                            return;
                        var commandData = {
                            Size: _this.params.fileSize,
                            Name: _this.params.selectFile.name,
                            Abstract: abstract
                        };
                        _this._websocket.sendCommand("UploadStartCommandHandler", commandData);
                    });
                };
                UploadApplicationPackViewModel.prototype.onFileReaderLoad = function (event) {
                    if (event.target == null || event.target.result == null)
                        return;
                    var uploadData = {
                        Index: this.params.index,
                        Base64Buffer: event.target.result.split(",")[1]
                    };
                    if (uploadData.Base64Buffer) {
                        this._websocket.sendCommand("UploadPartCommandHandler", uploadData);
                        this.params.index += event.loaded;
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
                return UploadApplicationPackViewModel;
            }());
            window.addEventListener("load", function () {
                var viewModel = new UploadApplicationPackViewModel();
            });
        })(Scripts = ConDep.Scripts || (ConDep.Scripts = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
