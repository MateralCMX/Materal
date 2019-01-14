var DictionaryTableIndexViewModel = /** @class */ (function () {
    function DictionaryTableIndexViewModel() {
        var _this = this;
        this.tableData = new Materal.Dictionary();
        for (var i = 0; i < 100000; i++) {
            this.tableData.set("temp" + i, { Name: "Name" + i, Value: "Value" + i, Temp: { AA: "AA" + i, BB: i } });
        }
        this.dictionaryTable = new Materal.DictionaryTable("dicTable", this.tableData, 20);
        var btnUp = document.getElementById("BtnUp");
        btnUp.addEventListener("click", function (event) {
            _this.dictionaryTable.dataIndex--;
            _this.dictionaryTable.updateTable();
        });
        var btnDown = document.getElementById("BtnDown");
        btnDown.addEventListener("click", function (event) {
            _this.dictionaryTable.dataIndex++;
            _this.dictionaryTable.updateTable();
        });
    }
    return DictionaryTableIndexViewModel;
}());
window.addEventListener("load", function () {
    var viewModel = new DictionaryTableIndexViewModel();
});
//# sourceMappingURL=Index.js.map