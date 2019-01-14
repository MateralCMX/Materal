class DictionaryTableIndexViewModel {
    constructor() {
        this.tableData = new Materal.Dictionary();
        for (let i = 0; i < 100000; i++) {
            this.tableData.set(`temp${i}`, { Name: `Name${i}`, Value: `Value${i}`, Temp: { AA: `AA${i}`, BB: i } });
        }
        this.dictionaryTable = new Materal.Component.DictionaryTable("dicTable", this.tableData, 20);
        const btnUp = document.getElementById("BtnUp");
        btnUp.addEventListener("click", (event) => {
            this.dictionaryTable.dataIndex--;
            this.dictionaryTable.updateTable();
        });
        const btnDown = document.getElementById("BtnDown");
        btnDown.addEventListener("click", (event) => {
            this.dictionaryTable.dataIndex++;
            this.dictionaryTable.updateTable();
        });
    }
}
window.addEventListener("load", () => {
    const viewModel = new DictionaryTableIndexViewModel();
});
//# sourceMappingURL=Index.js.map