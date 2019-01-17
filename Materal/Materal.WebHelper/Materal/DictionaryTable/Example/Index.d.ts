declare namespace WebHelper.Example {
    class DictionaryTableIndexViewModel {
        private dictionaryTable;
        private data;
        private tableData;
        private readonly tablePanel;
        private readonly scrollPanel;
        private readonly scrollBlock;
        private scrollMove;
        private marginDeviation;
        private showNumber;
        private readonly scrollPanelMarginTop;
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
         * 清空按钮单击事件
         */
        private btnClearClickEvent;
        /**
         * 滑条单击
         * @param event
         */
        private scrollPanelMouseDown;
        /**
         * 滑块鼠标按下
         * @param event
         */
        private scrollBlockMouseDown;
        /**
         * 表格面板鼠标移动
         * @param event
         */
        private tablePanelMouseMove;
        /**
         * 表格面板鼠标弹起
         */
        private tablePanelMouseUp;
        /**
         * 表格鼠标滚轮事件
         * @param event
         */
        private tableMouseWheelEvent;
        /**
         * 更新表数据
         */
        private updateTable;
        /**
         * 滑块移动
         * @param layerY
         */
        private scrollBlockMove;
        /**
         * 根据顶部距离获取位序
         * @param marginTop
         */
        private getIndexByMarginTop;
        /**
         * 获得滑块距离顶部的距离
         * @param dataCount
         */
        private getScrollBlockMarginTop;
        /**
         * 获得滑动块高度
         * @param dataCount
         * @param showNumber
         * @returns 滑动块高度
         */
        private getScrollBlockHeight;
        /**
         * 获得滚动条分为几份
         * @param dataCount
         * @param showNumber
         */
        private getProportion;
    }
}
declare function btnGetRowID(event: MouseEvent): void;
