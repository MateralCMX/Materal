var WebHelper;
(function (WebHelper) {
    var Example;
    (function (Example) {
        var DictionaryTableIndexViewModel = /** @class */ (function () {
            /**
             * 构造方法
             */
            function DictionaryTableIndexViewModel() {
                this.scrollMove = false; //滑块移动
                this.marginDeviation = 0; //滑块偏移
                this.showNumber = 10; //显示数量
                WebHelper.Common.loadDefaultTemplate("TopNavWeb");
                this.scrollPanel = document.getElementById("scrollPanel");
                this.scrollBlock = document.getElementById("scrollBlock");
                this.tablePanel = document.getElementById("tablePanel");
                var scrollPanelMarginTop = Materal.ElementHelper.getComputedStyle(this.scrollPanel).marginTop;
                scrollPanelMarginTop = scrollPanelMarginTop.substring(0, scrollPanelMarginTop.length - 2);
                this.scrollPanelMarginTop = parseFloat(scrollPanelMarginTop);
                this.loadData();
                this.initDictionaryTable();
                this.addEventListener();
            }
            /**
             * 加载数据
             */
            DictionaryTableIndexViewModel.prototype.loadData = function () {
                this.data = new Materal.Dictionary();
                for (var i = 0; i < 2000000; i++) {
                    var temp = {
                        Name: "Name" + i,
                        Value: "Value" + i,
                        Temp: { AA: "AA" + i, BB: i },
                        Attribute: {
                            "id": "temp" + i
                        }
                    };
                    if (i % 2 === 0) {
                        temp.Attribute["class"] = "blueRow";
                    }
                    this.data.set("temp" + i, temp);
                }
            };
            /**
             * 初始化字典表
             */
            DictionaryTableIndexViewModel.prototype.initDictionaryTable = function () {
                this.tableData = new Materal.Dictionary();
                var allKeys = this.data.getAllKeys();
                for (var key in allKeys) {
                    if (allKeys.hasOwnProperty(key)) {
                        var temp = this.data.get(allKeys[key]);
                        this.tableData.set(allKeys[key], temp);
                    }
                }
                this.dictionaryTable = new Materal.Component.DictionaryTable("dicTable", this.tableData, this.showNumber);
                this.updateTable();
            };
            /**
             * 添加事件监听
             */
            DictionaryTableIndexViewModel.prototype.addEventListener = function () {
                var _this = this;
                var btnUp = document.getElementById("BtnUp");
                btnUp.addEventListener("click", function () { return _this.btnUpClickEvent(); });
                var btnDown = document.getElementById("BtnDown");
                btnDown.addEventListener("click", function () { return _this.btnDownClickEvent(); });
                var btnSearch = document.getElementById("BtnSearch");
                btnSearch.addEventListener("click", function () { return _this.btnSearchClickEvent(); });
                var btnReduction = document.getElementById("BtnReduction");
                btnReduction.addEventListener("click", function () { return _this.btnReductionClickEvent(); });
                var btnClear = document.getElementById("BtnClear");
                btnClear.addEventListener("click", function () { return _this.btnClearClickEvent(); });
                var dicTable = document.getElementById("dicTable");
                dicTable.addEventListener("mousewheel", function (event) { return _this.tableMouseWheelEvent(event); });
                this.scrollBlock.addEventListener("mousedown", function (event) { return _this.scrollBlockMouseDown(event); });
                this.tablePanel.addEventListener("mousemove", function (event) { return _this.tablePanelMouseMove(event); });
                this.tablePanel.addEventListener("mouseup", function () { return _this.tablePanelMouseUp(); });
                this.scrollPanel.addEventListener("mousedown", function (event) { return _this.scrollPanelMouseDown(event); });
            };
            /**
             * 向上按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnUpClickEvent = function () {
                this.dictionaryTable.dataIndex--;
                this.updateTable();
            };
            /**
             * 向下按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnDownClickEvent = function () {
                this.dictionaryTable.dataIndex++;
                this.updateTable();
            };
            /**
             * 查询按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnSearchClickEvent = function () {
                this.tableData.clear();
                var allKeys = this.data.getAllKeys();
                for (var key in allKeys) {
                    if (allKeys.hasOwnProperty(key)) {
                        var temp = this.data.get(allKeys[key]);
                        if (temp.Name === "Name99") {
                            this.tableData.set(allKeys[key], temp);
                        }
                    }
                }
                this.dictionaryTable.dataIndex = 0;
                this.updateTable();
            };
            /**
             * 复原按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnReductionClickEvent = function () {
                this.tableData.clear();
                var allKeys = this.data.getAllKeys();
                for (var key in allKeys) {
                    if (allKeys.hasOwnProperty(key)) {
                        var temp = this.data.get(allKeys[key]);
                        this.tableData.set(allKeys[key], temp);
                    }
                }
                this.dictionaryTable.dataIndex = 0;
                this.updateTable();
            };
            /**
             * 清空按钮单击事件
             */
            DictionaryTableIndexViewModel.prototype.btnClearClickEvent = function () {
                this.tableData.clear();
                this.dictionaryTable.dataIndex = 0;
                this.updateTable();
            };
            /**
             * 滑条单击
             * @param event
             */
            DictionaryTableIndexViewModel.prototype.scrollPanelMouseDown = function (event) {
                var id = event.target.id;
                if (id === this.scrollPanel.id) {
                    var marginTop = event.layerY - this.scrollBlock.offsetHeight / 2;
                    this.scrollBlockMove(marginTop);
                    this.scrollMove = true;
                }
            };
            /**
             * 滑块鼠标按下
             * @param event
             */
            DictionaryTableIndexViewModel.prototype.scrollBlockMouseDown = function (event) {
                this.scrollMove = true;
                var scrollBlockMarginTop = Materal.ElementHelper.getComputedStyle(this.scrollBlock).marginTop;
                scrollBlockMarginTop = scrollBlockMarginTop.substring(0, scrollBlockMarginTop.length - 2);
                this.marginDeviation = parseFloat(scrollBlockMarginTop) - event.layerY;
                event.stopPropagation();
            };
            /**
             * 表格面板鼠标移动
             * @param event
             */
            DictionaryTableIndexViewModel.prototype.tablePanelMouseMove = function (event) {
                if (!this.scrollMove)
                    return;
                var id = event.target.id;
                var layerY = event.layerY;
                if (id !== this.scrollBlock.id && id !== this.scrollPanel.id) {
                    layerY -= this.scrollPanelMarginTop;
                }
                var marginTop = layerY + this.marginDeviation;
                this.scrollBlockMove(marginTop);
            };
            /**
             * 表格面板鼠标弹起
             */
            DictionaryTableIndexViewModel.prototype.tablePanelMouseUp = function () {
                this.scrollMove = false;
            };
            /**
             * 表格鼠标滚轮事件
             * @param event
             */
            DictionaryTableIndexViewModel.prototype.tableMouseWheelEvent = function (event) {
                if (event.deltaY > 0) {
                    this.dictionaryTable.dataIndex++;
                }
                else {
                    this.dictionaryTable.dataIndex--;
                }
                this.updateTable();
            };
            /**
             * 更新表数据
             */
            DictionaryTableIndexViewModel.prototype.updateTable = function () {
                var dataCount = this.tableData.getCount();
                this.showNumber = this.dictionaryTable.updateTable();
                var proportion = this.getProportion(dataCount, this.showNumber);
                var blockHeight = this.getScrollBlockHeight(proportion);
                var marginTop = this.getScrollBlockMarginTop(dataCount, proportion, blockHeight);
                this.scrollBlock.setAttribute("style", "height:" + blockHeight + "px;margin-top:" + marginTop + "px;");
            };
            /**
             * 滑块移动
             * @param layerY
             */
            DictionaryTableIndexViewModel.prototype.scrollBlockMove = function (marginTop) {
                if (marginTop < 0) {
                    marginTop = 0;
                }
                else if (marginTop > this.scrollPanel.offsetHeight - this.scrollBlock.offsetHeight) {
                    marginTop = this.scrollPanel.offsetHeight - this.scrollBlock.offsetHeight;
                }
                this.scrollBlock.style.marginTop = marginTop + "px";
                this.dictionaryTable.dataIndex = this.getIndexByMarginTop(marginTop);
                this.dictionaryTable.updateTable();
            };
            /**
             * 根据顶部距离获取位序
             * @param marginTop
             */
            DictionaryTableIndexViewModel.prototype.getIndexByMarginTop = function (marginTop) {
                var dataCount = this.tableData.getCount();
                var index = Math.round(marginTop / (this.scrollPanel.offsetHeight - this.scrollBlock.offsetHeight) * dataCount);
                return index;
            };
            /**
             * 获得滑块距离顶部的距离
             * @param dataCount
             */
            DictionaryTableIndexViewModel.prototype.getScrollBlockMarginTop = function (dataCount, proportion, blockHeight) {
                var marginTop = blockHeight * (this.dictionaryTable.dataIndex / dataCount * proportion);
                var maxMarginTop = this.scrollPanel.offsetHeight - blockHeight;
                if (marginTop > maxMarginTop) {
                    marginTop = maxMarginTop;
                }
                return marginTop;
            };
            /**
             * 获得滑动块高度
             * @param dataCount
             * @param showNumber
             * @returns 滑动块高度
             */
            DictionaryTableIndexViewModel.prototype.getScrollBlockHeight = function (proportion) {
                var blockHeight = this.scrollPanel.offsetHeight / proportion;
                return blockHeight;
            };
            /**
             * 获得滚动条分为几份
             * @param dataCount
             * @param showNumber
             */
            DictionaryTableIndexViewModel.prototype.getProportion = function (dataCount, showNumber) {
                var proportion = dataCount / showNumber;
                if (proportion > 100) {
                    proportion = 100;
                }
                return proportion;
            };
            return DictionaryTableIndexViewModel;
        }());
        Example.DictionaryTableIndexViewModel = DictionaryTableIndexViewModel;
    })(Example = WebHelper.Example || (WebHelper.Example = {}));
})(WebHelper || (WebHelper = {}));
window.addEventListener("load", function () {
    return new WebHelper.Example.DictionaryTableIndexViewModel();
});
function btnGetRowID(event) {
    var trElement = event.target.parentElement.parentElement;
    alert(trElement.getAttribute("class"));
}
//# sourceMappingURL=Index.js.map