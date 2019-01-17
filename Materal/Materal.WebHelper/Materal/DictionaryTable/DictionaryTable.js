var Materal;
(function (Materal) {
    var Component;
    (function (Component) {
        var DictionaryTable = /** @class */ (function () {
            /**
             * 构造方法
             * @param elementId 元素ID
             * @param data 数据源
             * @param showNumber 显示行数
             */
            function DictionaryTable(elementId, data, showNumber) {
                this.dataIndex = 0;
                var targetElement = document.getElementById(elementId);
                if (!targetElement)
                    throw "\u672A\u627E\u5230id\u4E3A" + elementId + "\u7684<table>";
                if (Materal.Common.getType(targetElement) !== "HTMLTableElement")
                    throw "\u672A\u627E\u5230id\u4E3A" + elementId + "\u7684<table>";
                this.tableElement = targetElement;
                var tbodyElements = this.tableElement.getElementsByTagName("tbody");
                if (tbodyElements.length === 0)
                    throw "\u672A\u5728id\u4E3A" + elementId + "\u7684<table>\u4E0B\u627E\u5230<tbody>";
                if (tbodyElements.length > 1)
                    throw "\u5728id\u4E3A" + elementId + "\u7684<table>\u4E0B\u627E\u5230\u591A\u4E2A<tbody>";
                this.tableBodyElement = tbodyElements[0];
                for (var i = 0; i < this.tableBodyElement.children.length; i++) {
                    var element = this.tableBodyElement.children[i];
                    if (Materal.Common.getType(element) === "HTMLTableRowElement") {
                        var row = element;
                        if (row.hasAttribute("m-target")) {
                            this.targetRow = row;
                            break;
                        }
                    }
                }
                if (Materal.Common.isNullOrUndefined(this.targetRow))
                    throw "\u672A\u5728id\u4E3A" + elementId + "\u7684<table><tbody>\u4E0B\u627E\u5230<tr m-target>";
                this.tableBodyElement.innerHTML = "";
                this.tableData = data;
                this.showNumber = showNumber;
            }
            /**
             * 更新表
             */
            DictionaryTable.prototype.updateTable = function () {
                var minIndex = 0;
                var tableDataCount = this.tableData.getCount();
                var showNumber = tableDataCount > this.showNumber ? this.showNumber : tableDataCount;
                var maxIndex = tableDataCount - showNumber;
                if (this.dataIndex < minIndex)
                    this.dataIndex = minIndex;
                if (this.dataIndex > maxIndex)
                    this.dataIndex = maxIndex;
                while (this.tableBodyElement.children.length > showNumber) {
                    var index = this.tableBodyElement.children.length - 1;
                    this.tableBodyElement.removeChild(this.tableBodyElement.children[index]);
                }
                for (var count = 0; count < showNumber; count++) {
                    this.updateRow(this.dataIndex + count);
                }
                return showNumber;
            };
            /**
             * 更新行
             * @param index
             */
            DictionaryTable.prototype.updateRow = function (index) {
                var addTr = false;
                var trElement = this.tableBodyElement.children[index - this.dataIndex];
                if (Materal.Common.isNullOrUndefined(trElement) || Materal.Common.getType(trElement) !== "HTMLTableRowElement") {
                    trElement = document.createElement("tr");
                    addTr = true;
                }
                var data = this.tableData.getByIndex(index);
                while (trElement.attributes.length > 0) {
                    trElement.removeAttribute(trElement.attributes[0].name);
                }
                if (data.hasOwnProperty("Attribute")) {
                    var attribute = data["Attribute"];
                    if (Materal.Common.getType(attribute) === "Object" && !Materal.Common.isNullOrUndefined(attribute)) {
                        for (var key in attribute) {
                            if (attribute.hasOwnProperty(key)) {
                                var attributeItem = attribute[key];
                                if (Materal.Common.getType(attributeItem) === "string" && !Materal.StringHelper.isNullOrrUndefinedOrEmpty(attributeItem)) {
                                    trElement.setAttribute(key, attributeItem);
                                }
                                else {
                                    trElement.removeAttribute(key);
                                }
                            }
                        }
                    }
                }
                if (Materal.Common.getType(data, false) !== "Object")
                    throw "\u6570\u636E\u6E90" + this.tableData.getKeyByIndex(index) + "\u4E0D\u662FObject";
                for (var i = 0; i < this.targetRow.children.length; i++) {
                    this.updateCell(trElement, i, data);
                }
                if (addTr) {
                    this.tableBodyElement.appendChild(trElement);
                }
            };
            /**
             * 更新单元格
             * @param trElement
             * @param index
             * @param data
             */
            DictionaryTable.prototype.updateCell = function (trElement, index, data) {
                var element = this.targetRow.children[index];
                var tdElement = trElement.children[index];
                var addTd = false;
                if (Materal.Common.isNullOrUndefined(tdElement)) {
                    tdElement = document.createElement("td");
                    tdElement.innerHTML = element.innerHTML;
                    addTd = true;
                }
                if (element.hasAttribute("m-model")) {
                    var modelName = element.getAttribute("m-model");
                    if (Materal.StringHelper.isNullOrrUndefinedOrEmpty(modelName))
                        throw "\u672A\u8BC6\u522Bm-model";
                    var modelNames = modelName.split(".");
                    var modelValue = data;
                    for (var i = 0; i < modelNames.length; i++) {
                        if (!modelValue.hasOwnProperty(modelNames[i])) {
                            modelValue = "";
                            break;
                        }
                        else {
                            modelValue = modelValue[modelNames[i]];
                        }
                    }
                    var valueType = Materal.Common.getType(modelValue);
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
                                throw "\u6570\u636E\u6E90\u6570\u636E\u4E0D\u80FD\u8F6C\u6362\u4E3AString";
                            }
                            break;
                    }
                }
                if (addTd) {
                    trElement.appendChild(tdElement);
                }
            };
            return DictionaryTable;
        }());
        Component.DictionaryTable = DictionaryTable;
    })(Component = Materal.Component || (Materal.Component = {}));
})(Materal || (Materal = {}));
//# sourceMappingURL=DictionaryTable.js.map