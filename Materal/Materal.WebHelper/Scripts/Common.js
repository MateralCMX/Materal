var WebHelper;
(function (WebHelper) {
    var Common = /** @class */ (function () {
        function Common() {
        }
        Common.loadHeader = function (headerElement) {
            if (!headerElement)
                return;
            var url = "/Template/Header.html";
            var config = new Materal.HttpConfigModel(url, Materal.HttpMethod.GET);
            config.success = function (result, xhr, state) {
                headerElement.innerHTML = result;
            };
            config.error = function (result, xhr, state) {
                console.error("加载头部模板失败");
            };
            Materal.HttpManager.send(config);
        };
        return Common;
    }());
    WebHelper.Common = Common;
})(WebHelper || (WebHelper = {}));
//# sourceMappingURL=Common.js.map