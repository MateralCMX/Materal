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
                    this._serviceRepository = new MicroFront.Repositories.ServiceRepository();
                    this.GetServices();
                }
                IndexViewModel.prototype.GetServices = function () {
                    var _this = this;
                    this._serviceRepository.GetAppList(function (result) {
                        _this._services = result.Data;
                    });
                };
                IndexViewModel.prototype.ChangeService = function (serviceName) {
                    this._services.forEach(function (service) {
                        if (service.Name == serviceName) {
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
                            for (var i = 0; i < service.Scripts.length; i++) {
                                var script = service.Scripts[i];
                                var scriptElement = document.createElement("script");
                                scriptElement.setAttribute("src", script);
                                scripts.appendChild(scriptElement);
                            }
                        }
                    });
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
