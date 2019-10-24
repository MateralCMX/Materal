"use strict";
var Materal;
(function (Materal) {
    var MicroFront;
    (function (MicroFront) {
        var Scripts;
        (function (Scripts) {
            var WebSocktHelper = /** @class */ (function () {
                function WebSocktHelper() {
                    var _this = this;
                    this.eventHandlers = new Materal.Dictionary();
                    this.websocket = new WebSocket("ws://" + Scripts.Common.getServerAddress() + "/websocket");
                    this.websocket.onclose = function (event) {
                        console.log("WebSocket已断开");
                    };
                    this.websocket.onerror = function (event) {
                        console.error(event);
                    };
                    this.websocket.onopen = function (event) {
                        console.log("WebSocket已连接");
                    };
                    this.websocket.onmessage = function (event) {
                        var data = JSON.parse(event.data);
                        if (!data.hasOwnProperty("HandlerName")) {
                            console.error("未找到事件处理器");
                        }
                        else {
                            var handler = _this.eventHandlers.get(data.HandlerName);
                            if (handler == null) {
                                console.error("\u672A\u627E\u5230\u4E8B\u4EF6\u5904\u7406\u5668" + data["HandlerName"]);
                            }
                            else {
                                handler(data);
                            }
                        }
                    };
                    this.registerEventHandler("ServerErrorEventHandler", function (event) {
                        switch (event.Status) {
                            case 401:
                                window.location.href = "/Login";
                                break;
                            default:
                                console.error(event);
                                break;
                        }
                    });
                }
                WebSocktHelper.prototype.sendCommand = function (handlerName, data) {
                    data["HandlerName"] = handlerName;
                    data["Token"] = Scripts.Common.getAuthoirtyInfo();
                    var commandString = JSON.stringify(data);
                    this.websocket.send(commandString);
                };
                WebSocktHelper.prototype.registerEventHandler = function (name, callback) {
                    this.eventHandlers.set(name, function (event) {
                        callback(event);
                    });
                };
                return WebSocktHelper;
            }());
            Scripts.WebSocktHelper = WebSocktHelper;
        })(Scripts = MicroFront.Scripts || (MicroFront.Scripts = {}));
    })(MicroFront = Materal.MicroFront || (Materal.MicroFront = {}));
})(Materal || (Materal = {}));
