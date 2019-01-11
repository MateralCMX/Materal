class IndexViewModel
{
    private readonly dictionaryTable: Materal.DictionaryTable;
    private readonly tableData: Materal.Dictionary;
    constructor()
    {
        this.tableData = new Materal.Dictionary();
        for (let i = 0; i < 100000; i++) {
            this.tableData.set(`temp${i}`, { Name: `Name${i}`, Value: `Value${i}`, Temp: { AA: `AA${i}`,BB:i} });
        }
        this.dictionaryTable = new Materal.DictionaryTable("dicTable", this.tableData, 20);
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

window.addEventListener("load", () =>
{
    const viewModel = new IndexViewModel();
});