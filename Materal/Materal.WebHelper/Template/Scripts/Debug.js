var WebHelper;
(function (WebHelper) {
    var Template;
    (function (Template) {
        var DebugViewModel = /** @class */ (function () {
            function DebugViewModel() {
                var templateConfigs = [
                    {
                        element: document.createElement("header"),
                        url: "/Template/Header.html",
                        style: null,
                        cssClass: null
                    },
                    {
                        element: document.createElement("footer"),
                        url: "/Template/Footer.html",
                        style: null,
                        cssClass: null
                    }
                ];
                for (var i = 0; i < templateConfigs.length; i++) {
                    document.body.appendChild(templateConfigs[i].element);
                }
                WebHelper.Common.loadTemplate(templateConfigs, function () {
                    var topNavLi = document.getElementById("TopNavDebug");
                    if (topNavLi) {
                        Materal.ElementHelper.addClass(topNavLi, "active");
                    }
                });
            }
            return DebugViewModel;
        }());
        Template.DebugViewModel = DebugViewModel;
    })(Template = WebHelper.Template || (WebHelper.Template = {}));
})(WebHelper || (WebHelper = {}));
window.addEventListener("load", function () {
    var viewModel = new WebHelper.Template.DebugViewModel();
});
//# sourceMappingURL=Debug.js.map