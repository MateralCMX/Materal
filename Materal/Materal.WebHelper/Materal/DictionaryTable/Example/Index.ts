namespace WebHelper.Example
{
    export class DictionaryTableIndexViewModel
    {
        private dictionaryTable: Materal.Component.DictionaryTable;//字典表
        private data: Materal.Dictionary;//数据源
        private tableData: Materal.Dictionary;//显示数据源
        private readonly tablePanel: HTMLDivElement;//表格面板
        private readonly scrollPanel: HTMLDivElement;//滑动条
        private readonly scrollBlock: HTMLDivElement;//滑块
        private scrollMove = false;//滑块移动
        private marginDeviation = 0;//滑块偏移
        private showNumber = 10;//显示数量
        private readonly scrollPanelMarginTop: number;//滑动条MarginTop
        /**
         * 构造方法
         */
        constructor()
        {
            Common.loadDefaultTemplate("TopNavWeb");
            this.scrollPanel = document.getElementById("scrollPanel") as HTMLDivElement;
            this.scrollBlock = document.getElementById("scrollBlock") as HTMLDivElement;
            this.tablePanel = document.getElementById("tablePanel") as HTMLDivElement;
            let scrollPanelMarginTop = Materal.ElementHelper.getComputedStyle(this.scrollPanel).marginTop;
            scrollPanelMarginTop = scrollPanelMarginTop.substring(0, scrollPanelMarginTop.length - 2);
            this.scrollPanelMarginTop = parseFloat(scrollPanelMarginTop);
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
            for (let i = 0; i < 2000000; i++) {
                const temp = {
                    Name: `Name${i}`,
                    Value: `Value${i}`,
                    Temp: { AA: `AA${i}`, BB: i },
                    Attribute: {
                        "id": `temp${i}`
                    }
                };
                if (i % 2 === 0) {
                    temp.Attribute["class"] = "blueRow";
                }
                this.data.set(`temp${i}`, temp);
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
            this.dictionaryTable = new Materal.Component.DictionaryTable("dicTable", this.tableData, this.showNumber);
            this.updateTable();
        }
        /**
         * 添加事件监听
         */
        private addEventListener()
        {
            const btnUp = document.getElementById("BtnUp");
            btnUp.addEventListener("click", () => this.btnUpClickEvent());
            const btnDown = document.getElementById("BtnDown");
            btnDown.addEventListener("click", () => this.btnDownClickEvent());
            const btnSearch = document.getElementById("BtnSearch");
            btnSearch.addEventListener("click", () => this.btnSearchClickEvent());
            const btnReduction = document.getElementById("BtnReduction");
            btnReduction.addEventListener("click", () => this.btnReductionClickEvent());
            const btnClear = document.getElementById("BtnClear");
            btnClear.addEventListener("click", () => this.btnClearClickEvent());
            const dicTable = document.getElementById("dicTable");
            dicTable.addEventListener("mousewheel", (event: WheelEvent) => this.tableMouseWheelEvent(event));
            this.scrollBlock.addEventListener("mousedown", (event: MouseEvent) => this.scrollBlockMouseDown(event));
            this.tablePanel.addEventListener("mousemove", (event: MouseEvent) => this.tablePanelMouseMove(event));
            this.tablePanel.addEventListener("mouseup", () => this.tablePanelMouseUp());
            this.scrollPanel.addEventListener("mousedown", (event: MouseEvent) => this.scrollPanelMouseDown(event));
        }
        /**
         * 向上按钮单击事件
         */
        private btnUpClickEvent()
        {
            this.dictionaryTable.dataIndex--;
            this.updateTable();
        }
        /**
         * 向下按钮单击事件
         */
        private btnDownClickEvent()
        {
            this.dictionaryTable.dataIndex++;
            this.updateTable();
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
                    if (temp.Name === "Name99") {
                        this.tableData.set(allKeys[key], temp);
                    }
                }
            }
            this.dictionaryTable.dataIndex = 0;
            this.updateTable();
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
            this.updateTable();
        }
        /**
         * 清空按钮单击事件
         */
        private btnClearClickEvent()
        {
            this.tableData.clear();
            this.dictionaryTable.dataIndex = 0;
            this.updateTable();
        }
        /**
         * 滑条单击
         * @param event
         */
        private scrollPanelMouseDown(event: any)
        {
            const id = (event.target as HTMLElement).id;
            if (id === this.scrollPanel.id) {
                const marginTop = event.layerY - this.scrollBlock.offsetHeight / 2;
                this.scrollBlockMove(marginTop);
                this.scrollMove = true;
            }
        }
        /**
         * 滑块鼠标按下
         * @param event
         */
        private scrollBlockMouseDown(event: any) {
            this.scrollMove = true;
            let scrollBlockMarginTop = Materal.ElementHelper.getComputedStyle(this.scrollBlock).marginTop;
            scrollBlockMarginTop = scrollBlockMarginTop.substring(0, scrollBlockMarginTop.length - 2);
            this.marginDeviation = parseFloat(scrollBlockMarginTop) - event.layerY;
            event.stopPropagation();
        }
        /**
         * 表格面板鼠标移动
         * @param event
         */
        private tablePanelMouseMove(event: any) {
            if (!this.scrollMove) return;
            const id = (event.target as HTMLElement).id;
            let layerY = event.layerY;
            if (id !== this.scrollBlock.id && id !== this.scrollPanel.id) {
                layerY -= this.scrollPanelMarginTop;
            }
            const marginTop = layerY + this.marginDeviation;
            this.scrollBlockMove(marginTop);
        }
        /**
         * 表格面板鼠标弹起
         */
        private tablePanelMouseUp()
        {
            this.scrollMove = false;
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
            this.updateTable();
        }
        /**
         * 更新表数据
         */
        private updateTable()
        {
            const dataCount = this.tableData.getCount();
            this.showNumber = this.dictionaryTable.updateTable();
            const proportion = this.getProportion(dataCount, this.showNumber);
            const blockHeight = this.getScrollBlockHeight(proportion);
            const marginTop = this.getScrollBlockMarginTop(dataCount, proportion, blockHeight);
            this.scrollBlock.setAttribute("style", `height:${blockHeight}px;margin-top:${marginTop}px;`);
        }
        /**
         * 滑块移动
         * @param layerY
         */
        private scrollBlockMove(marginTop: number)
        {
            if (marginTop < 0) {
                marginTop = 0;
            }
            else if (marginTop > this.scrollPanel.offsetHeight - this.scrollBlock.offsetHeight) {
                marginTop = this.scrollPanel.offsetHeight - this.scrollBlock.offsetHeight;
            }
            this.scrollBlock.style.marginTop = marginTop + "px";
            this.dictionaryTable.dataIndex = this.getIndexByMarginTop(marginTop);
            this.dictionaryTable.updateTable();
        }
        /**
         * 根据顶部距离获取位序
         * @param marginTop
         */
        private getIndexByMarginTop(marginTop: number)
        {
            const dataCount = this.tableData.getCount();
            const index = Math.round(marginTop / (this.scrollPanel.offsetHeight - this.scrollBlock.offsetHeight) * dataCount);
            return index;
        }
        /**
         * 获得滑块距离顶部的距离
         * @param dataCount
         */
        private getScrollBlockMarginTop(dataCount: number, proportion: number, blockHeight: number)
        {
            let marginTop = blockHeight * (this.dictionaryTable.dataIndex / dataCount * proportion);
            const maxMarginTop = this.scrollPanel.offsetHeight - blockHeight;
            if (marginTop > maxMarginTop) {
                marginTop = maxMarginTop;
            }
            return marginTop;
        }
        /**
         * 获得滑动块高度
         * @param dataCount
         * @param showNumber
         * @returns 滑动块高度
         */
        private getScrollBlockHeight(proportion: number): number {
            const blockHeight = this.scrollPanel.offsetHeight / proportion;
            return blockHeight;
        }
        /**
         * 获得滚动条分为几份
         * @param dataCount
         * @param showNumber
         */
        private getProportion(dataCount: number, showNumber: number)
        {
            let proportion = dataCount / showNumber;
            if (proportion > 100) {
                proportion = 100;
            }
            return proportion;
        }
    }
}

window.addEventListener("load", () =>
{
    return new WebHelper.Example.DictionaryTableIndexViewModel();
});
function btnGetRowID(event: MouseEvent)
{
    const trElement = (event.target as HTMLTableCellElement).parentElement.parentElement;
    alert(trElement.getAttribute("class"));
}