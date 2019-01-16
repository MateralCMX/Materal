var WebHelper;
(function (WebHelper) {
    var Template;
    (function (Template) {
        var StringHelper = Materal.StringHelper;
        var DebugViewModel = /** @class */ (function () {
            function DebugViewModel(element, name) {
                if (!element)
                    return;
                if (StringHelper.isNullOrrUndefinedOrEmpty(name))
                    return;
                var url = "/Template/" + name + ".html";
                var config = new Materal.HttpConfigModel(url, Materal.HttpMethod.GET);
                config.success = function (result, xhr, state) {
                    element.innerHTML = result;
                };
                config.error = function (result, xhr, state) {
                    console.error("加载模板失败");
                };
                Materal.HttpManager.send(config);
            }
            return DebugViewModel;
        }());
        Template.DebugViewModel = DebugViewModel;
    })(Template = WebHelper.Template || (WebHelper.Template = {}));
})(WebHelper || (WebHelper = {}));
window.addEventListener("load", function () {
    var element = document.getElementById("DebugBody");
    var viewModel = new WebHelper.Template.DebugViewModel(element, "Header");
});
//# sourceMappingURL=Debug.js.map