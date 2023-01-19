declare namespace Materal.Component {
    class DictionaryTable {
        private readonly tableElement;
        private readonly tableBodyElement;
        private readonly targetRow;
        private readonly tableData;
        private readonly showNumber;
        dataIndex: number;
        /**
         * 构造方法
         * @param elementId 元素ID
         * @param data 数据源
         * @param showNumber 显示行数
         */
        constructor(elementId: string, data: Dictionary, showNumber: number);
        /**
         * 更新表
         */
        updateTable(): number;
        /**
         * 更新行
         * @param index
         */
        updateRow(index: any): void;
        /**
         * 更新单元格
         * @param trElement
         * @param index
         * @param data
         */
        updateCell(trElement: HTMLTableRowElement, index: number, data: Object): void;
    }
}
