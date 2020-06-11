var WebHelper;
(function (WebHelper) {
    var IndexViewModel = /** @class */ (function () {
        function IndexViewModel() {
            WebHelper.Common.loadDefaultTemplate();
        }
        return IndexViewModel;
    }());
    WebHelper.IndexViewModel = IndexViewModel;
})(WebHelper || (WebHelper = {}));
window.addEventListener("load", function () {
    var viewModel = new WebHelper.IndexViewModel();
});
//# sourceMappingURL=Index.js.map