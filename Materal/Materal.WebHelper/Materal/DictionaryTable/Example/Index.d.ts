declare class DictionaryTableIndexViewModel {
    private dictionaryTable;
    private data;
    private tableData;
    /**
     * 构造方法
     */
    constructor();
    /**
     * 加载数据
     */
    private loadData;
    /**
     * 初始化字典表
     */
    private initDictionaryTable;
    /**
     * 添加事件监听
     */
    private addEventListener;
    /**
     * 向上按钮单击事件
     */
    private btnUpClickEvent;
    /**
     * 向下按钮单击事件
     */
    private btnDownClickEvent;
    /**
     * 查询按钮单击事件
     */
    private btnSearchClickEvent;
    /**
     * 复原按钮单击事件
     */
    private btnReductionClickEvent;
    /**
     * 表格鼠标滚轮事件
     * @param event
     */
    private tableMouseWheelEvent;
}
