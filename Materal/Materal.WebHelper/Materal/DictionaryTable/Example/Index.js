var DictionaryTableIndexViewModel = /** @class */ (function () {
    /**
     * 构造方法
     */
    function DictionaryTableIndexViewModel() {
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
        var btnUp = document.getElementById("BtnUp");
        btnUp.addEventListener("click", this.btnUpClickEvent);
        var btnDown = document.getElementById("BtnDown");
        btnDown.addEventListener("click", this.btnDownClickEvent);
        var btnSearch = document.getElementById("BtnSearch");
        btnSearch.addEventListener("click", this.btnSearchClickEvent);
        var btnReduction = document.getElementById("BtnReduction");
        btnReduction.addEventListener("click", this.btnReductionClickEvent);
        var dicTable = document.getElementById("dicTable");
        dicTable.addEventListener("mousewheel", this.tableMouseWheelEvent);
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
                if (temp.Name.indexOf("2") >= 0) {
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
window.addEventListener("load", function () {
    var viewModel = new DictionaryTableIndexViewModel();
    console.log(viewModel);
});
//# sourceMappingURL=Index.js.map