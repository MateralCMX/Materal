var WebHelper;
(function (WebHelper) {
    var Example;
    (function (Example) {
        var DictionaryTableIndexViewModel = /** @class */ (function () {
            /**
             * 构造方法
             */
            function DictionaryTableIndexViewModel() {
                WebHelper.Common.loadDefaultTemplate();
                this.loadData();
                this.initDictionaryTable();
                this.addEventListener();
            }
            /**
             * 加载数据
             */
            DictionaryTableIndexViewModel.prototype.loadData = function () {
                this.data = new Materal.Dictionary();
                for (var i = 0; i < 10000; i++) {
                    this.data.set("temp" + i, { Name: "Name" + i, Value: "Value" + i, Temp: { AA: "AA" + i, BB: i } });
                }
            };
            /**
             * 初始化字典表
             */
            DictionaryTableIndexViewModel.prototype.initDictionaryTable = function () {
                this.tableData = new Materal.Dictionary();
                var allKeys = this.data.getAllKeys();
                for (var key in allKeys) {
                    if (allKeys.hasOwnProperty(key)) {
                        var temp = this.data.get(allKeys[key]);
                        this.tableData.set(allKeys[key], temp);
                    }
                }
                this.dictionaryTable = new Materal.Component.DictionaryTable("dicTable", this.tableData, 10);
            };
            /**
             * 添加事件监听
             */
            DictionaryTableIndexViewModel.prototype.addEventListener = function () {
                var _this = this;
                var btnUp = document.getElementById("BtnUp");
                btnUp.addEventListener("click", function () { return _this.btnUpClickEvent(); });
                var btnDown = document.getElementById("BtnDown");
                btnDown.addEventListener("click", function () { return _this.btnDownClickEvent(); });
                var btnSearch = document.getElementById("BtnSearch");
                btnSearch.addEventListener("click", function () { return _this.btnSearchClickEvent(); });
                var btnReduction = document.getElementById("BtnReduction");
                btnReduction.addEventListener("click", function () { return _this.btnReductionClickEvent(); });
                var btnClear = document.getElementById("BtnClear");
                btnClear.addEventListener("click", function () { return _this.btnClearClickEvent(); });
                var dicTable = document.getElementById("dicTable");
                dicTable.addEventListener("mousewheel", function (event) { return _this.tableMouseWheelEvent(event); });
            };
            /**
             * 向上按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnUpClickEvent = function () {
                this.dictionaryTable.dataIndex--;
                this.dictionaryTable.updateTable();
            };
            /**
             * 向下按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnDownClickEvent = function () {
                this.dictionaryTable.dataIndex++;
                this.dictionaryTable.updateTable();
            };
            /**
             * 查询按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnSearchClickEvent = function () {
                this.tableData.clear();
                var allKeys = this.data.getAllKeys();
                for (var key in allKeys) {
                    if (allKeys.hasOwnProperty(key)) {
                        var temp = this.data.get(allKeys[key]);
                        //if (temp.Name.indexOf("2") >= 0) {
                        //    this.tableData.set(allKeys[key], temp);
                        //}
                        if (temp.Name === "Name99") {
                            this.tableData.set(allKeys[key], temp);
                        }
                    }
                }
                this.dictionaryTable.dataIndex = 0;
                this.dictionaryTable.updateTable();
            };
            /**
             * 复原按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnReductionClickEvent = function () {
                this.tableData.clear();
                var allKeys = this.data.getAllKeys();
                for (var key in allKeys) {
                    if (allKeys.hasOwnProperty(key)) {
                        var temp = this.data.get(allKeys[key]);
                        this.tableData.set(allKeys[key], temp);
                    }
                }
                this.dictionaryTable.dataIndex = 0;
                this.dictionaryTable.updateTable();
            };
            /**
             * 清空按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnClearClickEvent = function () {
                this.tableData.clear();
                this.dictionaryTable.dataIndex = 0;
                this.dictionaryTable.updateTable();
            };
            /**
             * 表格鼠标滚轮事件
             * @param event
             */
            DictionaryTableIndexViewModel.prototype.tableMouseWheelEvent = function (event) {
                if (event.deltaY > 0) {
                    this.dictionaryTable.dataIndex++;
                }
                else {
                    this.dictionaryTable.dataIndex--;
                }
                this.dictionaryTable.updateTable();
            };
            return DictionaryTableIndexViewModel;
        }());
        Example.DictionaryTableIndexViewModel = DictionaryTableIndexViewModel;
    })(Example = WebHelper.Example || (WebHelper.Example = {}));
})(WebHelper || (WebHelper = {}));
window.addEventListener("load", function () {
    var viewModel = new WebHelper.Example.DictionaryTableIndexViewModel();
});
//# sourceMappingURL=Index.js.map