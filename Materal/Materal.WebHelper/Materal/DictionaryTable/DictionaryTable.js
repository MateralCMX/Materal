var Materal;
(function (Materal) {
    var Component;
    (function (Component) {
        class DictionaryTable {
            /**
             * 构造方法
             * @param elementId 元素ID
             * @param data 数据源
             * @param showNumber 显示行数
             */
            constructor(elementId, data, showNumber) {
                this.dataIndex = 0;
                const targetElement = document.getElementById(elementId);
                if (!targetElement)
                    throw `未找到id为${elementId}的<table>`;
                if (Materal.Common.getType(targetElement) !== "HTMLTableElement")
                    throw `未找到id为${elementId}的<table>`;
                this.tableElement = targetElement;
                const tbodyElements = this.tableElement.getElementsByTagName("tbody");
                if (tbodyElements.length === 0)
                    throw `未在id为${elementId}的<table>下找到<tbody>`;
                if (tbodyElements.length > 1)
                    throw `在id为${elementId}的<table>下找到多个<tbody>`;
                this.tableBodyElement = tbodyElements[0];
                for (let i = 0; i < this.tableBodyElement.children.length; i++) {
                    const element = this.tableBodyElement.children[i];
                    if (Materal.Common.getType(element) === "HTMLTableRowElement") {
                        const row = element;
                        if (row.hasAttribute("m-target")) {
                            this.targetRow = row;
                            break;
                        }
                    }
                }
                if (Materal.Common.isNullOrUndefined(this.targetRow))
                    throw `未在id为${elementId}的<table><tbody>下找到<tr m-target>`;
                this.tableBodyElement.innerHTML = "";
                this.tableData = data;
                this.showNumber = showNumber;
                this.updateTable();
            }
            /**
             * 更新表
             */
            updateTable() {
                const minIndex = 0;
                const tableDataCount = this.tableData.getCount();
                const showNumber = tableDataCount > this.showNumber ? this.showNumber : tableDataCount;
                const maxIndex = tableDataCount - showNumber;
                if (this.dataIndex < minIndex)
                    this.dataIndex = minIndex;
                if (this.dataIndex > maxIndex)
                    this.dataIndex = maxIndex;
                for (let count = 0; count < showNumber; count++) {
                    this.updateRow(this.dataIndex + count);
                }
            }
            /**
             * 更新行
             * @param index
             */
            updateRow(index) {
                let addTr = false;
                let trElement = this.tableBodyElement.children[index - this.dataIndex];
                if (Materal.Common.isNullOrUndefined(trElement) || Materal.Common.getType(trElement) !== "HTMLTableRowElement") {
                    trElement = document.createElement("tr");
                    addTr = true;
                }
                const data = this.tableData.getByIndex(index);
                if (Materal.Common.getType(data, false) !== "Object")
                    throw `数据源${this.tableData.getKeyByIndex(index)}不是Object`;
                for (let i = 0; i < this.targetRow.children.length; i++) {
                    this.updateCell(trElement, i, data);
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
            updateCell(trElement, index, data) {
                const element = this.targetRow.children[index];
                let tdElement = trElement.children[index];
                let addTd = false;
                if (Materal.Common.isNullOrUndefined(tdElement)) {
                    tdElement = document.createElement("td");
                    tdElement.innerHTML = element.innerHTML;
                    addTd = true;
                }
                if (element.hasAttribute("m-model")) {
                    const modelName = element.getAttribute("m-model");
                    if (Materal.StringHelper.isNullOrrUndefinedOrEmpty(modelName))
                        throw `未识别m-model`;
                    const modelNames = modelName.split(".");
                    let modelValue = data;
                    for (let i = 0; i < modelNames.length; i++) {
                        if (!modelValue.hasOwnProperty(modelNames[i])) {
                            modelValue = "";
                            break;
                        }
                        else {
                            modelValue = modelValue[modelNames[i]];
                        }
                    }
                    const valueType = Materal.Common.getType(modelValue);
                    switch (valueType) {
                        case "string":
                            tdElement.innerHTML = modelValue;
                            break;
                        case "number":
                            tdElement.innerHTML = modelValue + "";
                            break;
                        default:
                            if (Materal.Common.getType(modelValue.toString) === "Function") {
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
        Component.DictionaryTable = DictionaryTable;
    })(Component = Materal.Component || (Materal.Component = {}));
})(Materal || (Materal = {}));
//# sourceMappingURL=DictionaryTable.js.map