var Materal;
(function (Materal) {
    var Common = /** @class */ (function () {
        function Common() {
        }
        /**
         * 是否为undefined
         * @param obj 需要判断的对象
         * @returns 判断结果
         */
        Common.isUndefined = function (obj) {
            return typeof obj === "undefined";
        };
        /**
         * 是否为null或者undefined
         * @param obj 需要判断的对象
         * @returns 判断结果
         */
        Common.isNullOrUndefined = function (obj) {
            return obj === null || this.isUndefined(obj);
        };
        /**
         * 是否为Empty字符串
         * @param str 需要判断的对象
         * @returns 判断结果
         */
        Common.isEmpty = function (str) {
            return str === "";
        };
        /**
         *  是否为null或undefined或Empty字符串
         * @param str
         */
        Common.isNullOrrUndefinedOrEmpty = function (str) {
            return this.isNullOrUndefined(str) || this.isEmpty(str);
        };
        /**
         * 鉴别类型
         * @param obj 传入对象
         * @param includeCustom 包括自定义类型
         * @returns 对象类型
         */
        Common.getType = function (obj, includeCustom) {
            if (includeCustom === void 0) { includeCustom = true; }
            var lowercase = true;
            var objType = typeof obj;
            var result;
            if (objType === "object") {
                if (obj === null) {
                    result = "null";
                }
                else {
                    lowercase = false;
                    result = Object.prototype.toString.call(obj).slice(8, -1);
                    if (result === "Object" &&
                        !this.isNullOrUndefined(obj.constructor) &&
                        obj.constructor.name !== "Object" &&
                        includeCustom) {
                        result = obj.constructor.name;
                    }
                }
            }
            else {
                result = objType;
            }
            if (!this.isNullOrrUndefinedOrEmpty(result) && lowercase) {
                result = result.toLowerCase();
            }
            return result;
        };
        return Common;
    }());
    Materal.Common = Common;
    /**
     * 数组帮助类
     */
    var ArrayHelper = /** @class */ (function () {
        function ArrayHelper() {
        }
        /**
         * 移除
         * @param array 原数组
         * @param index 位序
         * @returns 移除后的数组
         */
        ArrayHelper.removeAt = function (array, index) {
            array = array.splice(index, 1);
            return array;
        };
        /**
         * 清空
         * @param array 原数组
         * @returns 清空后的数组
         */
        ArrayHelper.clear = function (array) {
            array = array.splice(0, array.length);
            return array;
        };
        return ArrayHelper;
    }());
    Materal.ArrayHelper = ArrayHelper;
    /**
     * 字典
     */
    var Dictionary = /** @class */ (function () {
        function Dictionary() {
            this.data = new Object();
            this.keys = new Array();
        }
        /**
         * 设置值
         * @param key 键
         * @param value 值
         */
        Dictionary.prototype.set = function (key, value) {
            if (!this.data.hasOwnProperty(key)) {
                this.keys.push(key);
            }
            this.data[key] = value;
        };
        /**
         * 获得值
         * @param key 键
         * @returns 值
         */
        Dictionary.prototype.get = function (key) {
            if (this.data.hasOwnProperty(key)) {
                return this.data[key];
            }
            else {
                return undefined;
            }
        };
        /**
         * 根据位序获得值
         * @param index 位序
         * @returns 值
         */
        Dictionary.prototype.getByIndex = function (index) {
            if (index >= this.keys.length)
                throw "位序超过索引";
            return this.get(this.keys[index]);
        };
        /**
         * 移除值
         * @param key 键
         */
        Dictionary.prototype.remove = function (key) {
            if (this.data.hasOwnProperty(key)) {
                var index = this.keys.indexOf(key);
                this.keys = ArrayHelper.removeAt(this.keys, index);
                delete this.data[key];
            }
        };
        /**
         * 获取所有的键
         */
        Dictionary.prototype.getAllKeys = function () {
            return this.keys;
        };
        /**
         * 根据位序获得键
         * @param index 位序
         * @returns 键
         */
        Dictionary.prototype.getKeyByIndex = function (index) {
            if (index >= this.keys.length)
                throw "位序超过索引";
            return this.keys[index];
        };
        /**
         * 获得总数
         * @returns 总数
         */
        Dictionary.prototype.getCount = function () {
            return this.keys.length;
        };
        /**
         * 清空
         */
        Dictionary.prototype.clear = function () {
            this.keys = ArrayHelper.clear(this.keys);
            for (var key in this.data) {
                if (this.data.hasOwnProperty(key)) {
                    delete this.data[key];
                }
            }
        };
        return Dictionary;
    }());
    Materal.Dictionary = Dictionary;
})(Materal || (Materal = {}));
//# sourceMappingURL=Materal.js.map