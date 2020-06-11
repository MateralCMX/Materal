var WebHelper;
(function (WebHelper) {
    /**
     * 公共库
     */
    var Common = /** @class */ (function () {
        function Common() {
        }
        /**
         * 加载模板
         * @param templateConfigs
         */
        Common.loadTemplate = function (templateConfigs, callFunc) {
            if (callFunc === void 0) { callFunc = null; }
            var successCount = 0;
            var _loop_1 = function (i) {
                var templateConfig = templateConfigs[i];
                if (!Materal.StringHelper.isNullOrrUndefinedOrEmpty(templateConfig.style)) {
                    templateConfig.element.setAttribute("style", templateConfig.style);
                }
                if (!Materal.StringHelper.isNullOrrUndefinedOrEmpty(templateConfig.cssClass)) {
                    Materal.ElementHelper.addClass(templateConfig.element, templateConfig.cssClass);
                }
                var httpConfig = new Materal.HttpConfigModel(templateConfig.url, Materal.HttpMethod.GET);
                httpConfig.success = function (result, xhr, state) {
                    templateConfig.element.innerHTML = result;
                    if (++successCount === templateConfigs.length && callFunc) {
                        callFunc();
                    }
                };
                httpConfig.error = function (result, xhr, state) {
                    throw "加载模板失败";
                };
                Materal.HttpManager.send(httpConfig);
            };
            for (var i = 0; i < templateConfigs.length; i++) {
                _loop_1(i);
            }
        };
        /**
         * 加载默认模板
         */
        Common.loadDefaultTemplate = function (activeTopNavId) {
            if (activeTopNavId === void 0) { activeTopNavId = "TopNavHome"; }
            var templateConfigs = [];
            var headers = document.getElementsByTagName("header");
            if (headers.length <= 0)
                throw "加载头部失败";
            templateConfigs.push({ element: headers[0], url: "/Template/Header.html", style: null, cssClass: null });
            var footers = document.getElementsByTagName("footer");
            if (footers.length <= 0)
                throw "加载腿部失败";
            templateConfigs.push({ element: footers[0], url: "/Template/Footer.html", style: null, cssClass: null });
            this.loadTemplate(templateConfigs, function () {
                var topNavLi = document.getElementById(activeTopNavId);
                if (topNavLi) {
                    Materal.ElementHelper.addClass(topNavLi, "active");
                }
            });
        };
        return Common;
    }());
    WebHelper.Common = Common;
})(WebHelper || (WebHelper = {}));
//# sourceMappingURL=Common.js.map