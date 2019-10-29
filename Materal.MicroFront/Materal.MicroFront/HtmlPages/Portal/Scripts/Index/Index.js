"use strict";
var Materal;
(function (Materal) {
    var MicroFront;
    (function (MicroFront) {
        var Scripts;
        (function (Scripts) {
            var IndexViewModel = /** @class */ (function () {
                function IndexViewModel() {
                    this._services = [];
                    this.nowService = "";
                    this._serviceRepository = new MicroFront.Repositories.ServiceRepository();
                    this.GetServices();
                }
                IndexViewModel.prototype.GetServices = function () {
                    var _this = this;
                    this._serviceRepository.GetAppList(function (result) {
                        _this._services = result.Data;
                        var appInfos = document.getElementById("AppInfos");
                        appInfos.innerHTML = "";
                        for (var i = 0; i < _this._services.length; i++) {
                            var service = _this._services[i];
                            var aElement = document.createElement("a");
                            aElement.href = "/#/" + service.Name;
                            aElement.innerHTML = service.Name;
                            appInfos.appendChild(aElement);
                        }
                    });
                };
                IndexViewModel.prototype.ChangeService = function (serviceName) {
                    var _this = this;
                    this._services.forEach(function (service) {
                        if (service.Name == serviceName && _this.nowService != service.Name) {
                            var nowService = window[_this.nowService];
                            if (nowService) {
                                nowService.vue.$destroy();
                            }
                            _this.nowService = service.Name;
                            var head = document.getElementsByTagName("head")[0];
                            //清理路由
                            for (var i = 0; i < head.childNodes.length; i++) {
                                var element = head.childNodes[i];
                                if (element.nodeName == "LINK") {
                                    head.removeChild(element);
                                }
                            }
                            //添加Link
                            for (var i = 0; i < service.Links.length; i++) {
                                var link = service.Links[i];
                                var linkElement = document.createElement("link");
                                linkElement.setAttribute("href", link.HrefAttribute);
                                linkElement.setAttribute("rel", link.RelAttribute);
                                if (link.AsAttribute) {
                                    linkElement.setAttribute("as", link.AsAttribute);
                                }
                                head.appendChild(linkElement);
                            }
                            //添加Script
                            var scripts = document.getElementById("scripts");
                            scripts.innerHTML = "";
                            _this.LoadScripts(0, service.Scripts, scripts);
                        }
                    });
                };
                IndexViewModel.prototype.LoadScripts = function (index, scripts, scriptsDiv) {
                    var _this = this;
                    if (index >= scripts.length) {
                        var nowService = window[this.nowService];
                        nowService.Init();
                        nowService.vue.$mount("#app");
                        return;
                    }
                    var scriptElement = document.createElement("script");
                    scriptElement.setAttribute("src", scripts[index]);
                    scriptElement.addEventListener("load", function () {
                        _this.LoadScripts(index + 1, scripts, scriptsDiv);
                    });
                    scriptsDiv.appendChild(scriptElement);
                };
                return IndexViewModel;
            }());
            var viewModel;
            window.addEventListener("load", function () {
                viewModel = new IndexViewModel();
                var hashs = window.location.hash.split('/');
                if (hashs.length >= 2) {
                    viewModel.ChangeService(hashs[1]);
                }
            });
            window.addEventListener("hashchange", function () {
                var hashs = window.location.hash.split('/');
                if (hashs.length >= 2) {
                    viewModel.ChangeService(hashs[1]);
                }
            });
        })(Scripts = MicroFront.Scripts || (MicroFront.Scripts = {}));
    })(MicroFront = Materal.MicroFront || (Materal.MicroFront = {}));
})(Materal || (Materal = {}));
