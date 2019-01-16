class DictionaryTableIndexViewModel
{
    private dictionaryTable: Materal.Component.DictionaryTable;//字典表
    private data: Materal.Dictionary;//数据源
    private tableData: Materal.Dictionary;//显示数据源
    /**
     * 构造方法
     */
    constructor()
    {
        this.loadData();
        this.initDictionaryTable();
        this.addEventListener();
    }
    /**
     * 加载数据
     */
    private loadData()
    {
        this.data = new Materal.Dictionary();
        for (let i = 0; i < 10000; i++) {
            this.data.set(`temp${i}`, { Name: `Name${i}`, Value: `Value${i}`, Temp: { AA: `AA${i}`, BB: i } });
        }
    }
    /**
     * 初始化字典表
     */
    private initDictionaryTable()
    {
        this.tableData = new Materal.Dictionary();
        const allKeys = this.data.getAllKeys();
        for (let key in allKeys) {
            if (allKeys.hasOwnProperty(key)) {
                const temp = this.data.get(allKeys[key]);
                this.tableData.set(allKeys[key], temp);
            }
        }
        this.dictionaryTable = new Materal.Component.DictionaryTable("dicTable", this.tableData, 10);
    }
    /**
     * 添加事件监听
     */
    private addEventListener()
    {
        const btnUp = document.getElementById("BtnUp");
        btnUp.addEventListener("click", this.btnUpClickEvent);
        const btnDown = document.getElementById("BtnDown");
        btnDown.addEventListener("click", this.btnDownClickEvent);
        const btnSearch = document.getElementById("BtnSearch");
        btnSearch.addEventListener("click", this.btnSearchClickEvent);
        const btnReduction = document.getElementById("BtnReduction");
        btnReduction.addEventListener("click", this.btnReductionClickEvent);
        const dicTable = document.getElementById("dicTable");
        dicTable.addEventListener("mousewheel", this.tableMouseWheelEvent);
    }
    /**
     * 向上按钮单击事件
     */
    private btnUpClickEvent()
    {
        this.dictionaryTable.dataIndex--;
        this.dictionaryTable.updateTable();
    }
    /**
     * 向下按钮单击事件
     */
    private btnDownClickEvent()
    {
        this.dictionaryTable.dataIndex++;
        this.dictionaryTable.updateTable();
    }
    /**
     * 查询按钮单击事件
     */
    private btnSearchClickEvent()
    {
        this.tableData.clear();
        const allKeys = this.data.getAllKeys();
        for (let key in allKeys) {
            if (allKeys.hasOwnProperty(key)) {
                const temp = this.data.get(allKeys[key]);
                if (temp.Name.indexOf("2") >= 0) {
                    this.tableData.set(allKeys[key], temp);
                }
            }
        }
        this.dictionaryTable.dataIndex = 0;
        this.dictionaryTable.updateTable();
    }
    /**
     * 复原按钮单击事件
     */
    private btnReductionClickEvent()
    {
        this.tableData.clear();
        const allKeys = this.data.getAllKeys();
        for (let key in allKeys) {
            if (allKeys.hasOwnProperty(key)) {
                const temp = this.data.get(allKeys[key]);
                this.tableData.set(allKeys[key], temp);
            }
        }
        this.dictionaryTable.dataIndex = 0;
        this.dictionaryTable.updateTable();
    }
    /**
     * 表格鼠标滚轮事件
     * @param event
     */
    private tableMouseWheelEvent(event: WheelEvent)
    {
        if (event.deltaY > 0) {
            this.dictionaryTable.dataIndex++;
        } else {
            this.dictionaryTable.dataIndex--;
        }
        this.dictionaryTable.updateTable();
    }
}

window.addEventListener("load", () =>
{
    const viewModel = new DictionaryTableIndexViewModel();
    console.log(viewModel);
});