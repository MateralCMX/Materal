var WebHelper;
(function (WebHelper) {
    var IndexViewModel = /** @class */ (function () {
        function IndexViewModel() {
            var headers = document.getElementsByTagName("header");
            if (headers.length <= 0)
                throw "加载头部失败";
            WebHelper.Common.loadHeader(headers[0]);
        }
        return IndexViewModel;
    }());
    WebHelper.IndexViewModel = IndexViewModel;
})(WebHelper || (WebHelper = {}));
window.addEventListener("load", function () {
    var viewModel = new WebHelper.IndexViewModel();
});
//# sourceMappingURL=Index.js.map