"use strict";
var Materal;
(function (Materal) {
    var ConDep;
    (function (ConDep) {
        var Scripts;
        (function (Scripts) {
            var Common = /** @class */ (function () {
                function Common() {
                }
                /**
                 * 设置权限信息
                 * @param token token值
                 */
                Common.setAuthoirtyInfo = function (token) {
                    sessionStorage.setItem("token", token);
                };
                /**
                 * 获得权限信息
                 */
                Common.getAuthoirtyInfo = function () {
                    return sessionStorage.getItem("token");
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
                        console.error(result);
                    };
                    var token = this.getAuthoirtyInfo();
                    if (token) {
                        if (method == Materal.HttpMethod.GET) {
                            if (url.indexOf("?") > 0) {
                                url += "&token=" + token;
                            }
                            else {
                                url += "?token=" + token;
                            }
                        }
                        else {
                            data["Token"] = token;
                        }
                    }
                    var config = new Materal.HttpConfigModel("http://" + this.serverAddress + "/api/" + url, method, data, Materal.HttpHeadContentType.Json, successFunc, errorFunc);
                    Materal.HttpManager.send(config);
                };
                Common.serverAddress = "192.168.0.107:8910";
                return Common;
            }());
            Scripts.Common = Common;
        })(Scripts = ConDep.Scripts || (ConDep.Scripts = {}));
    })(ConDep = Materal.ConDep || (Materal.ConDep = {}));
})(Materal || (Materal = {}));
