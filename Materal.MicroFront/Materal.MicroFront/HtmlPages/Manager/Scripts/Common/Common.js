"use strict";
var Materal;
(function (Materal) {
    var MicroFront;
    (function (MicroFront) {
        var Scripts;
        (function (Scripts) {
            var Common = /** @class */ (function () {
                function Common() {
                }
                Common.getServerAddress = function () {
                    return window.location.host;
                };
                /**
                 * 设置权限信息
                 * @param token token值
                 */
                Common.setAuthoirtyInfo = function (token) {
                    localStorage.setItem("token", token);
                };
                /**
                 * 获得权限信息
                 */
                Common.getAuthoirtyInfo = function () {
                    return localStorage.getItem("token");
                };
                /**
                 * 获得权限信息
                 */
                Common.removeAuthoirtyInfo = function () {
                    localStorage.removeItem("token");
                };
                /**
                 * 发送Post请求
                 * @param url
                 * @param data
                 * @param success
                 * @param fail
                 */
                Common.sendPost = function (url, data, success, fail) {
                    this.send(url, data, Materal.HttpMethod.POST, success, fail);
                };
                /**
                 * 发送Get请求
                 * @param url
                 * @param data
                 * @param success
                 * @param fail
                 */
                Common.sendGet = function (url, data, success, fail) {
                    this.send(url, data, Materal.HttpMethod.GET, success, fail);
                };
                /**
                 * 发送请求
                 * @param url
                 * @param data
                 * @param method
                 * @param success
                 * @param fail
                 */
                Common.send = function (url, data, method, success, fail) {
                    if (!fail) {
                        fail = function (res) {
                            alert(res.Message);
                        };
                    }
                    var successFunc = function (result, xhr, status) {
                        if (result.ResultType == 0) {
                            if (success) {
                                success(result, xhr, status);
                            }
                        }
                        else {
                            if (fail) {
                                fail(result, xhr, status);
                            }
                        }
                    };
                    var errorFunc = function (result, xhr, status) {
                        switch (status) {
                            case 401:
                                window.location.href = "/Manager/Login.html";
                                break;
                            default:
                                console.error(result);
                                break;
                        }
                    };
                    var heads = {
                        "Content-Type": "application/json"
                    };
                    var token = this.getAuthoirtyInfo();
                    if (token) {
                        heads["Authorization"] = "Bearer " + token;
                    }
                    var config = new Materal.HttpConfigModel("http://" + this.getServerAddress() + "/api/" + url, method, data, heads, successFunc, errorFunc);
                    Materal.HttpManager.send(config);
                };
                return Common;
            }());
            Scripts.Common = Common;
        })(Scripts = MicroFront.Scripts || (MicroFront.Scripts = {}));
    })(MicroFront = Materal.MicroFront || (Materal.MicroFront = {}));
})(Materal || (Materal = {}));
