namespace Materal
{
    export class DictionaryTable
    {
        private readonly tableElement: HTMLTableElement;
        private readonly tableBodyElement: HTMLTableSectionElement;
        private readonly targetRow: HTMLTableRowElement;
        private readonly tableData: Dictionary;
        private readonly showNumber: number;
        dataIndex = 0;
        /**
         * 构造方法
         * @param elementId 元素ID
         * @param data 数据源
         * @param showNumber 显示行数
         */
        constructor(elementId: string, data: Dictionary, showNumber: number)
        {
            const targetElement = document.getElementById(elementId);
            if (!targetElement) throw `未找到id为${elementId}的<table>`;
            if (Common.getType(targetElement) !== "HTMLTableElement") throw `未找到id为${elementId}的<table>`;
            this.tableElement = targetElement as HTMLTableElement;
            const tbodyElements = this.tableElement.getElementsByTagName("tbody");
            if (tbodyElements.length === 0) throw `未在id为${elementId}的<table>下找到<tbody>`;
            if (tbodyElements.length > 1) throw `在id为${elementId}的<table>下找到多个<tbody>`;
            this.tableBodyElement = tbodyElements[0] as HTMLTableSectionElement;
            for (let i = 0; i < this.tableBodyElement.children.length; i++) {
                const element = this.tableBodyElement.children[i];
                if (Common.getType(element) === "HTMLTableRowElement") {
                    const row = element as HTMLTableRowElement;
                    if (row.hasAttribute("m-target")) {
                        this.targetRow = row;
                        break;
                    }
                }
            }
            if (Common.isNullOrUndefined(this.targetRow)) throw `未在id为${elementId}的<table><tbody>下找到<tr m-target>`;
            this.tableBodyElement.innerHTML = "";
            this.tableData = data;
            this.showNumber = showNumber;
            this.updateTable();
        }
        /**
         * 更新表
         */
        updateTable()
        {
            if (this.dataIndex < 0) this.dataIndex = 0;
            const dataCount1 = this.tableData.getCount();
            if (this.dataIndex > dataCount1) this.dataIndex = dataCount1;
            const tempIndex = (dataCount1 - this.dataIndex) >= this.showNumber
                ? this.dataIndex
                : this.showNumber;
            const dataCount2 = tempIndex + this.showNumber;
            for (let index = tempIndex; index < dataCount1 && index < dataCount2; index++) {
                this.updateRow(index);
            }
        }
        /**
         * 更新行
         * @param index
         */
        updateRow(index)
        {
            let addTr = false;
            let trElement = this.tableBodyElement.children[index - this.dataIndex];
            if (Common.isNullOrUndefined(trElement) || Common.getType(trElement) !== "HTMLTableRowElement") {
                trElement = document.createElement("tr");
                addTr = true;
            }
            const data: Object = this.tableData.getByIndex(index);
            if (Common.getType(data, false) !== "Object") throw `数据源${this.tableData.getKeyByIndex(index)}不是Object`;
            for (let i = 0; i < this.targetRow.children.length; i++) {
                this.updateCell(trElement as HTMLTableRowElement, i, data);
            }
            if (addTr) {
                this.tableBodyElement.appendChild(trElement);
            }
        }
        /**
         * 更新单元格
         * @param trElement
         * @param index
         * @param data
         */
        updateCell(trElement: HTMLTableRowElement, index: number, data: Object)
        {
            const element = this.targetRow.children[index];
            let tdElement = trElement.children[index];
            let addTd = false;
            if (Common.isNullOrUndefined(tdElement)) {
                tdElement = document.createElement("td");
                tdElement.innerHTML = element.innerHTML;
                addTd = true;
            }
            if (element.hasAttribute("m-model")) {
                const modelName = element.getAttribute("m-model");
                if (Common.isNullOrrUndefinedOrEmpty(modelName)) throw `未识别m-model`;
                const modelNames = modelName.split(".");
                let modelValue = data;
                for (let i = 0; i < modelNames.length; i++) {
                    if (!modelValue.hasOwnProperty(modelNames[i])) throw `数据源无此属性`;
                    modelValue = modelValue[modelNames[i]];
                }
                const valueType = Common.getType(modelValue);
                switch (valueType) {
                    case "string":
                        tdElement.innerHTML = modelValue as string;
                        break;
                    case "number":
                        tdElement.innerHTML = modelValue as number + "";
                        break;
                    default:
                        if (Common.getType(modelValue.toString) === "Function") {
                            tdElement.innerHTML = modelValue.toString();
                        }
                        else {
                            throw `数据源数据不能转换为String`;
                        }
                        break;
                }
            }
            if (addTd) {
                trElement.appendChild(tdElement);
            }
        }
    }
}