var Materal;
(function (Materal) {
    /*
     * 时间类型
     */
    var TimeType;
    (function (TimeType) {
        /**
         * 年
         */
        TimeType[TimeType["Years"] = 0] = "Years";
        /**
         * 月
         */
        TimeType[TimeType["Months"] = 1] = "Months";
        /**
         * 日
         */
        TimeType[TimeType["Day"] = 2] = "Day";
        /**
         * 时
         */
        TimeType[TimeType["Hours"] = 3] = "Hours";
        /**
         * 分
         */
        TimeType[TimeType["Minutes"] = 4] = "Minutes";
        /**
         * 秒
         */
        TimeType[TimeType["Seconds"] = 5] = "Seconds";
        /**
         * 毫秒
         */
        TimeType[TimeType["Milliseconds"] = 6] = "Milliseconds";
    })(TimeType = Materal.TimeType || (Materal.TimeType = {}));
    /**
     * 公共
     */
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
            if (!StringHelper.isNullOrrUndefinedOrEmpty(result) && lowercase) {
                result = result.toLowerCase();
            }
            return result;
        };
        /**
         * 克隆对象
         * @param obj 要克隆的对象
         */
        Common.clone = function (obj) {
            var objectType = this.getType(obj, false);
            var result;
            if (objectType === "Object") {
                result = new Object();
            }
            else if (objectType === "array") {
                result = new Array();
            }
            else {
                result = obj;
            }
            for (var i in obj) {
                if (obj.hasOwnProperty(i)) {
                    var copy = obj[i];
                    var subObjectType = this.getType(copy, false);
                    if (subObjectType === "Object" || subObjectType === "array") {
                        result[i] = this.arguments.callee(copy);
                    }
                    else {
                        result[i] = copy;
                    }
                }
            }
            return result;
        };
        return Common;
    }());
    Materal.Common = Common;
    /**
     * 字符串帮助类
     */
    var StringHelper = /** @class */ (function () {
        function StringHelper() {
        }
        /**
         * 是否为Empty字符串
         * @param inputStr 需要判断的对象
         * @returns 判断结果
         */
        StringHelper.isEmpty = function (inputStr) {
            return inputStr === "";
        };
        /**
         * 是否为null或undefined或Empty字符串
         * @param inputStr 输入的字符串
         * 判定结果
         */
        StringHelper.isNullOrrUndefinedOrEmpty = function (inputStr) {
            return Common.isNullOrUndefined(inputStr) || this.isEmpty(inputStr);
        };
        /**
         * 移除左边空格
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        StringHelper.trimLeft = function (inputStr) {
            while (inputStr.substr(0, 1) === " ") {
                inputStr = inputStr.substr(1, inputStr.length - 1);
            }
            return inputStr;
        };
        /**
         * 移除右边空格
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        StringHelper.trimRight = function (inputStr) {
            while (inputStr.substr(inputStr.length - 2, 1) === " ") {
                inputStr = inputStr.substr(0, inputStr.length - 2);
            }
            return inputStr;
        };
        /**
         * 移除所有空格
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        StringHelper.trimAll = function (inputStr) {
            return inputStr.replace(/\s/g, "");
        };
        /**
         * 移除多余空格(连续的空格将变成一个)
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        StringHelper.simplifyMultiSpaceToSingle = function (inputStr) {
            return inputStr.replace(/\s{2,}/g, " ");
        };
        /**
         * 左侧填充字符
         * @param inputStr 输入的字符串
         * @param length 位数
         * @param character 填充字符
         */
        StringHelper.padLeft = function (inputStr, length, character) {
            for (var i = inputStr.length; i < length; i++) {
                inputStr = character + inputStr;
            }
            return inputStr.substr(0, length);
        };
        /**
         * 右侧填充字符
         * @param length 位数
         * @param character 填充字符
         */
        StringHelper.padRight = function (inputStr, length, character) {
            for (var i = inputStr.length; i < length; i++) {
                inputStr = inputStr + character;
            }
            return inputStr.substr(0, length);
        };
        /**
         * 获取32位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        StringHelper.get32Md5String = function (str, isLower) {
            if (isLower === void 0) { isLower = false; }
            function l(a) {
                return h(g(o(a), a.length * 8));
            }
            function m(e) {
                var b = "0123456789ABCDEF";
                if (isLower) {
                    b = b.toLowerCase();
                }
                var c = "";
                var d;
                for (var a_1 = 0; a_1 < e.length; a_1++) {
                    d = e.charCodeAt(a_1);
                    c += b.charAt(d >>> 4 & 15) + b.charAt(d & 15);
                }
                return c;
            }
            function n(d) {
                var b = "";
                var c = -1;
                var a, e;
                while (++c < d.length) {
                    a = d.charCodeAt(c),
                        e = c + 1 < d.length ? d.charCodeAt(c + 1) : 0;
                    55296 <= a && a <= 56319 && 56320 <= e && e <= 57343 && (a = 65536 + ((a & 1023) << 10) + (e & 1023), c++);
                    a <= 127 ? b += String.fromCharCode(a) : a <= 2047 ? b += String.fromCharCode(192 | a >>> 6 & 31, 128 | a & 63) : a <= 65535 ? b += String.fromCharCode(224 | a >>> 12 & 15, 128 | a >>> 6 & 63, 128 | a & 63) : a <= 2097151 && (b += String.fromCharCode(240 | a >>> 18 & 7, 128 | a >>> 12 & 63, 128 | a >>> 6 & 63, 128 | a & 63));
                }
                return b;
            }
            function o(c) {
                var b = Array(c.length >> 2);
                for (var a_2 = 0; a_2 < b.length; a_2++) {
                    b[a_2] = 0;
                }
                for (var a_3 = 0; a_3 < c.length * 8; a_3 += 8) {
                    b[a_3 >> 5] |= (c.charCodeAt(a_3 / 8) & 255) << a_3 % 32;
                }
                return b;
            }
            function h(c) {
                var b = "";
                for (var a_4 = 0; a_4 < c.length * 32; a_4 += 8) {
                    b += String.fromCharCode(c[a_4 >> 5] >>> a_4 % 32 & 255);
                }
                return b;
            }
            function g(j, l) {
                j[l >> 5] |= 128 << l % 32, j[(l + 64 >>> 9 << 4) + 14] = l;
                var g = 1732584193;
                var h = -271733879;
                var i = -1732584194;
                var f = 271733878;
                for (var k = 0; k < j.length; k += 16) {
                    var n_1 = g;
                    var o_1 = h;
                    var p = i;
                    var m_1 = f;
                    g = a(g, h, i, f, j[k + 0], 7, -680876936);
                    f = a(f, g, h, i, j[k + 1], 12, -389564586);
                    i = a(i, f, g, h, j[k + 2], 17, 606105819);
                    h = a(h, i, f, g, j[k + 3], 22, -1044525330);
                    g = a(g, h, i, f, j[k + 4], 7, -176418897);
                    f = a(f, g, h, i, j[k + 5], 12, 1200080426);
                    i = a(i, f, g, h, j[k + 6], 17, -1473231341);
                    h = a(h, i, f, g, j[k + 7], 22, -45705983);
                    g = a(g, h, i, f, j[k + 8], 7, 1770035416);
                    f = a(f, g, h, i, j[k + 9], 12, -1958414417);
                    i = a(i, f, g, h, j[k + 10], 17, -42063);
                    h = a(h, i, f, g, j[k + 11], 22, -1990404162);
                    g = a(g, h, i, f, j[k + 12], 7, 1804603682);
                    f = a(f, g, h, i, j[k + 13], 12, -40341101);
                    i = a(i, f, g, h, j[k + 14], 17, -1502002290);
                    h = a(h, i, f, g, j[k + 15], 22, 1236535329);
                    g = b(g, h, i, f, j[k + 1], 5, -165796510);
                    f = b(f, g, h, i, j[k + 6], 9, -1069501632);
                    i = b(i, f, g, h, j[k + 11], 14, 643717713);
                    h = b(h, i, f, g, j[k + 0], 20, -373897302);
                    g = b(g, h, i, f, j[k + 5], 5, -701558691);
                    f = b(f, g, h, i, j[k + 10], 9, 38016083);
                    i = b(i, f, g, h, j[k + 15], 14, -660478335);
                    h = b(h, i, f, g, j[k + 4], 20, -405537848);
                    g = b(g, h, i, f, j[k + 9], 5, 568446438);
                    f = b(f, g, h, i, j[k + 14], 9, -1019803690);
                    i = b(i, f, g, h, j[k + 3], 14, -187363961);
                    h = b(h, i, f, g, j[k + 8], 20, 1163531501);
                    g = b(g, h, i, f, j[k + 13], 5, -1444681467);
                    f = b(f, g, h, i, j[k + 2], 9, -51403784);
                    i = b(i, f, g, h, j[k + 7], 14, 1735328473);
                    h = b(h, i, f, g, j[k + 12], 20, -1926607734);
                    g = c(g, h, i, f, j[k + 5], 4, -378558);
                    f = c(f, g, h, i, j[k + 8], 11, -2022574463);
                    i = c(i, f, g, h, j[k + 11], 16, 1839030562);
                    h = c(h, i, f, g, j[k + 14], 23, -35309556);
                    g = c(g, h, i, f, j[k + 1], 4, -1530992060);
                    f = c(f, g, h, i, j[k + 4], 11, 1272893353);
                    i = c(i, f, g, h, j[k + 7], 16, -155497632);
                    h = c(h, i, f, g, j[k + 10], 23, -1094730640);
                    g = c(g, h, i, f, j[k + 13], 4, 681279174);
                    f = c(f, g, h, i, j[k + 0], 11, -358537222);
                    i = c(i, f, g, h, j[k + 3], 16, -722521979);
                    h = c(h, i, f, g, j[k + 6], 23, 76029189);
                    g = c(g, h, i, f, j[k + 9], 4, -640364487);
                    f = c(f, g, h, i, j[k + 12], 11, -421815835);
                    i = c(i, f, g, h, j[k + 15], 16, 530742520);
                    h = c(h, i, f, g, j[k + 2], 23, -995338651);
                    g = d(g, h, i, f, j[k + 0], 6, -198630844);
                    f = d(f, g, h, i, j[k + 7], 10, 1126891415);
                    i = d(i, f, g, h, j[k + 14], 15, -1416354905);
                    h = d(h, i, f, g, j[k + 5], 21, -57434055);
                    g = d(g, h, i, f, j[k + 12], 6, 1700485571);
                    f = d(f, g, h, i, j[k + 3], 10, -1894986606);
                    i = d(i, f, g, h, j[k + 10], 15, -1051523);
                    h = d(h, i, f, g, j[k + 1], 21, -2054922799);
                    g = d(g, h, i, f, j[k + 8], 6, 1873313359);
                    f = d(f, g, h, i, j[k + 15], 10, -30611744);
                    i = d(i, f, g, h, j[k + 6], 15, -1560198380);
                    h = d(h, i, f, g, j[k + 13], 21, 1309151649);
                    g = d(g, h, i, f, j[k + 4], 6, -145523070);
                    f = d(f, g, h, i, j[k + 11], 10, -1120210379);
                    i = d(i, f, g, h, j[k + 2], 15, 718787259);
                    h = d(h, i, f, g, j[k + 9], 21, -343485551);
                    g = e(g, n_1);
                    h = e(h, o_1);
                    i = e(i, p);
                    f = e(f, m_1);
                }
                return Array(g, h, i, f);
            }
            function f(a, b, c, d, f, g) {
                return e(j(e(e(b, a), e(d, g)), f), c);
            }
            function a(b, a, c, d, e, g, h) {
                return f(a & c | ~a & d, b, a, e, g, h);
            }
            function b(c, a, d, b, e, g, h) {
                return f(a & b | d & ~b, c, a, e, g, h);
            }
            function c(b, a, c, d, e, g, h) {
                return f(a ^ c ^ d, b, a, e, g, h);
            }
            function d(b, a, c, d, e, g, h) {
                return f(c ^ (a | ~d), b, a, e, g, h);
            }
            function e(b, c) {
                var a = (b & 65535) + (c & 65535);
                var d = (b >> 16) + (c >> 16) + (a >> 16);
                return d << 16 | a & 65535;
            }
            function j(a, b) {
                return a << b | a >>> 32 - b;
            }
            return m(l(n(str)));
        };
        /**
         * 获取16位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        StringHelper.get16Md5String = function (str, isLower) {
            if (isLower === void 0) { isLower = false; }
            return this.get32Md5String(str, isLower).substr(8, 16);
        };
        /**
         * 转换为二进制字符串
         * @param str 要转换的字符串
         * @returns 转换后的字符串
         */
        StringHelper.convertToBinary = function (str) {
            var strList = Array.prototype.map.call(str, function (c) { return c.charCodeAt(0).toString(2); });
            var resStr = "";
            for (var i = 0; i < strList.length; i++) {
                resStr += StringHelper.padLeft(strList[i], 8, "0");
            }
            return resStr;
        };
        /**
         * 隐藏代码
         * @param codeStr 要隐藏的代码
         * @returns 隐藏后的代码
         */
        StringHelper.hideCode = function (codeStr) {
            var resStr = this.convertToBinary(codeStr);
            resStr = resStr.replace(/0/g, "\u200d");
            resStr = resStr.replace(/1/g, "\u200c");
            return resStr;
        };
        /**
         * 显示代码
         * @param codeStr 被隐藏的代码
         * @returns 显示的代码
         */
        StringHelper.showCode = function (codeStr) {
            var resStr = codeStr.replace(/.{8}/g, function (u) { return String.fromCharCode(parseInt(u.replace(/\u200c/g, "1").replace(/\u200d/g, "0"), 2)); });
            return resStr;
        };
        return StringHelper;
    }());
    Materal.StringHelper = StringHelper;
    /**
     * 时间帮助类
     */
    var DateHelper = /** @class */ (function () {
        function DateHelper() {
        }
        /**
         * 获得时间差
         * @param inputDateTime 输入时间
         * @param targetDate 对比时间
         * @param TimeType 返回类型
         * @param isFloor 向下取整
         * @returns 时间差
         */
        DateHelper.getTimeDifference = function (inputDateTime, targetDate, timeType, isFloor) {
            if (isFloor === void 0) { isFloor = true; }
            var timeDifference = inputDateTime.getTime() - targetDate.getTime();
            switch (timeType) {
                case TimeType.Day:
                    timeDifference = timeDifference / (24 * 3600 * 1000);
                    break;
                case TimeType.Hours:
                    timeDifference = timeDifference / (3600 * 1000);
                    break;
                case TimeType.Minutes:
                    timeDifference = timeDifference / (60 * 1000);
                    break;
                case TimeType.Seconds:
                    timeDifference = timeDifference / 1000;
                    break;
                case TimeType.Milliseconds:
                    break;
                default:
                    throw "参数timeType错误";
            }
            if (isFloor) {
                timeDifference = Math.floor(timeDifference);
            }
            return timeDifference;
        };
        /**
         * 时间字符串格式化
         * @param inputDateTime 输入时间
         * @param formatStr 格式化字符串[y年|M月|d日|H时|m分|s秒|S毫秒|q季度]
         * @returns 格式化后的时间字符串
         */
        DateHelper.formatString = function (inputDateTime, formatStr) {
            var formatData = {
                "M+": inputDateTime.getMonth() + 1,
                "d+": inputDateTime.getDate(),
                "H+": inputDateTime.getHours(),
                "m+": inputDateTime.getMinutes(),
                "s+": inputDateTime.getSeconds(),
                "q+": Math.floor((inputDateTime.getMonth() + 3) / 3),
                "S": inputDateTime.getMilliseconds() //毫秒 
            };
            if (/(y+)/.test(formatStr)) {
                formatStr = formatStr.replace(RegExp.$1, (inputDateTime.getFullYear() + "").substr(4 - RegExp.$1.length));
            }
            for (var data in formatData) {
                if (formatData.hasOwnProperty(data)) {
                    if (new RegExp("(" + data + ")").test(formatStr)) {
                        var tempString = void 0;
                        if (RegExp.$1.length === 1) {
                            tempString = formatData[data];
                        }
                        else {
                            tempString = ("00" + formatData[data]).substr(("" + formatData[data]).length);
                        }
                        formatStr = formatStr.replace(RegExp.$1, tempString);
                    }
                }
            }
            return formatStr;
        };
        /**
         * 获取Input dateTime设置值字符串
         * @param inputDateTime 输入时间
         * @param dateTime 要设置的时间
         * @returns 可以设置给Input的时间值
         */
        DateHelper.getInputDateTimeValueStr = function (inputDateTime) {
            return this.formatString(inputDateTime, "yyyy-MM-ddTHH:mm:ss");
        };
        /**
         * 获取对应时区时间
         * @param inputDateTime 输入时间
         * @param timeZone 时区
         * @returns 对应时区时间
         */
        DateHelper.getDateByTimeZone = function (inputDateTime, timeZone) {
            var len = inputDateTime.getTime();
            var offset = inputDateTime.getTimezoneOffset() * 60000;
            var utcTime = len + offset;
            var result = new Date(utcTime + 3600000 * timeZone);
            return result;
        };
        /**
         * 将其他时区时间转换为对应时区时间
         * @param inputDateTime 输入时间
         * @param timeZone 时区[null则自动为本地时区]
         * @returns 对应时区时间
         */
        DateHelper.convertTimeZone = function (inputDateTime, timeZone) {
            if (!timeZone) {
                timeZone = inputDateTime.getTimezoneOffset() / 60;
            }
            inputDateTime.setTime(inputDateTime.getTime() - timeZone * 60 * 60 * 1000);
            return inputDateTime;
        };
        return DateHelper;
    }());
    Materal.DateHelper = DateHelper;
    /**
     * 数组帮助类
     */
    var ArrayHelper = /** @class */ (function () {
        function ArrayHelper() {
        }
        /**
         * 插入数组
         * @param inputArray 原数组
         * @param item 要插入的对象
         * @param index 位序
         * @returns 插入后的数组
         */
        ArrayHelper.insert = function (inputArray, item, index) {
            inputArray.splice(index, 0, item);
            return inputArray;
        };
        /**
         * 删除数组
         * @param inputArray 原数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        ArrayHelper.remove = function (inputArray, item) {
            var index = inputArray.indexOf(item);
            if (index >= 0) {
                inputArray = this.removeAt(inputArray, index);
            }
            return inputArray;
        };
        /**
         * 删除所有数组
         * @param inputArray 原数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        ArrayHelper.removeAll = function (inputArray, item) {
            var index = inputArray.indexOf(item);
            while (index >= 0) {
                inputArray = this.removeAt(inputArray, index);
                index = inputArray.indexOf(item);
            }
            return inputArray;
        };
        /**
         * 移除
         * @param inputArray 原数组
         * @param index 位序
         * @returns 移除后的数组
         */
        ArrayHelper.removeAt = function (inputArray, index) {
            var count = inputArray.length;
            inputArray = inputArray.splice(index, 1);
            if (count === inputArray.length && count === 1) {
                inputArray = [];
            }
            return inputArray;
        };
        /**
         * 清空
         * @param inputArray 原数组
         * @returns 清空后的数组
         */
        ArrayHelper.clear = function (inputArray) {
            inputArray = inputArray.splice(0, 0);
            return inputArray;
        };
        return ArrayHelper;
    }());
    Materal.ArrayHelper = ArrayHelper;
    /**
     * 地址帮助类
     */
    var LocationHelper = /** @class */ (function () {
        function LocationHelper() {
        }
        /**
         * 获得参数
         * @returns URL参数
         */
        LocationHelper.getUrlParamsByArray = function () {
            var result = null;
            var paramString = window.location.search;
            if (!StringHelper.isNullOrrUndefinedOrEmpty(paramString)) {
                paramString = paramString.substring(1, paramString.length);
                result = paramString.split("&");
            }
            return result;
        };
        /**
         * 获得URL参数
         * @returns URL参数
         */
        LocationHelper.getUrlParamsByObject = function () {
            var result = new Object();
            var paramStrings = this.getUrlParamsByArray();
            if (!Common.isNullOrUndefined(paramStrings)) {
                for (var i = 0; i < paramStrings.length; i++) {
                    var params = paramStrings[i].split("=");
                    if (params.length === 2) {
                        result[params[0]] = params[1];
                    }
                    else if (params.length === 1) {
                        result[params[0]] = null;
                    }
                }
            }
            return result;
        };
        /**
         * 获得URL参数
         * @returns URL参数
         */
        LocationHelper.getUrlParamsByDictionary = function () {
            var result = new Dictionary();
            var paramStrings = this.getUrlParamsByArray();
            if (!Common.isNullOrUndefined(paramStrings)) {
                for (var i = 0; i < paramStrings.length; i++) {
                    var params = paramStrings[i].split("=");
                    if (params.length === 2) {
                        result.set(params[0], params[1]);
                    }
                    else if (params.length === 1) {
                        result.set(params[0], null);
                    }
                }
            }
            return result;
        };
        /**
         * 获得URL参数
         * @param key 键
         * @returns URL参数
         */
        LocationHelper.getUrlParam = function (key) {
            var params = this.getUrlParamsByObject();
            if (params.hasOwnProperty(key)) {
                return params[key];
            }
            else {
                throw "\u672A\u627E\u5230\u952E\u4E3A" + key + "\u7684Url\u53C2\u6570";
            }
        };
        return LocationHelper;
    }());
    Materal.LocationHelper = LocationHelper;
    /**
     * 文档帮助类
     */
    var DocumentHelper = /** @class */ (function () {
        function DocumentHelper() {
        }
        /**
         * 获取滚动条位置
         * @returns 滚动条位置
         */
        DocumentHelper.getScrollTop = function () {
            var scrollTop = 0;
            if (document.documentElement && document.documentElement.scrollTop) {
                scrollTop = document.documentElement.scrollTop;
            }
            else if (document.body) {
                scrollTop = document.body.scrollTop;
            }
            return scrollTop;
        };
        /**
         * 获取可见高度
         * @returns 可见高度
         */
        DocumentHelper.getClientHeight = function () {
            var clientHeight;
            if (document.body.clientHeight && document.documentElement.clientHeight) {
                clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            }
            else {
                clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            }
            return clientHeight;
        };
        return DocumentHelper;
    }());
    Materal.DocumentHelper = DocumentHelper;
    /**
     * 元素帮助类
     */
    var ElementHelper = /** @class */ (function () {
        function ElementHelper() {
        }
        /**
         * 设置样式
         * @param inputElement 目标元素
         * @param element 页面元素
         * @param className 要设置的样式列表
         */
        ElementHelper.setClass = function (inputElement, className) {
            var classString = Common.getType(className) === "Array" ? className.join(" ") : className;
            if (!StringHelper.isNullOrrUndefinedOrEmpty(classString)) {
                classString = StringHelper.simplifyMultiSpaceToSingle(classString).trim();
                inputElement.setAttribute("class", classString);
            }
            else {
                inputElement.removeAttribute("class");
            }
        };
        /**
         * 添加样式
         * @param inputElement 目标元素
         * @param className 要添加的样式
         */
        ElementHelper.addClass = function (inputElement, className) {
            if (Common.getType(className) === "string") {
                className = className.split(" ");
            }
            for (var i = 0; i < className.length; i++) {
                inputElement.classList.add(className[i]);
            }
        };
        /**
         * 删除样式
         * @param inputElement 目标元素
         * @param className 要删除的样式
         */
        ElementHelper.removeClass = function (inputElement, className) {
            if (Common.getType(className) === "string") {
                className = className.split(" ");
            }
            for (var i = 0; i < className.length; i++) {
                inputElement.classList.remove(className[i]);
            }
        };
        /**
         * 是否有拥有样式
         * @param inputElement 目标元素
         * @param className 要查找的样式列表
         * @returns 查询结果
         */
        ElementHelper.hasClass = function (inputElement, className) {
            var resM = true;
            if (Common.getType(className) === "string") {
                className = className.split(" ");
            }
            for (var i = 0; i < className.length && resM; i++) {
                resM = inputElement.classList.contains(className[i]);
                if (resM === false)
                    break;
            }
            return resM;
        };
        /**
         * 根据ClassName获得元素对象
         * @param inputElement 目标元素
         * @param className ClassName
         * @returns Element集合
         */
        ElementHelper.getElementsByClassName = function (inputElement, className) {
            className = className.trim();
            if (inputElement.getElementsByClassName) {
                return inputElement.getElementsByClassName(className);
            }
            else {
                var elements = inputElement.getElementsByTagName("*");
                var result = new Array();
                for (var i = 0; i < elements.length; i++) {
                    if (ElementHelper.hasClass(elements[i], className)) {
                        result.push(elements[i]);
                    }
                }
                return result;
            }
        };
        /**
         * 根据Name获得元素对象
         * @param inputElement 目标元素
         * @param name Name
         * @returns Element集合
         */
        ElementHelper.getElementsByName = function (inputElement, name) {
            name = name.trim();
            var result = new Array();
            var elements = inputElement.getElementsByTagName("*");
            for (var i = 0; i < elements.length; i++) {
                if (elements[i].name && elements[i].name === name) {
                    result.push(elements[i]);
                }
            }
            return result;
        };
        /**
         * 获得子节点
         * @param inputElement 目标元素
         * @returns 子节点
         */
        ElementHelper.getChildren = function (inputElement) {
            if (inputElement.children) {
                return inputElement.children;
            }
            else {
                var result = new Array();
                var length_1 = inputElement.childNodes.length;
                for (var i = 0; i < length_1; i++) {
                    if (inputElement.childNodes[i].nodeType === 1) {
                        result.push(inputElement.childNodes[i]);
                    }
                }
                return result;
            }
        };
        /**
         * 获得元素的实际样式
         * @param inputElement 目标元素
         * @returns 实际样式
         */
        ElementHelper.getComputedStyle = function (inputElement) {
            var cssStyle;
            if (inputElement["currentStyle"]) {
                cssStyle = inputElement["currentStyle"];
            }
            else {
                cssStyle = getComputedStyle(inputElement);
            }
            return cssStyle;
        };
        /**
         * 获得自定义属性
         * @param inputElement 目标元素
         * @returns 自定义属性
         */
        ElementHelper.prototype.getDataSet = function (inputElement) {
            if (inputElement.dataset) {
                return inputElement.dataset;
            }
            else {
                var result = new Object();
                var length_2 = inputElement.attributes.length;
                var item = void 0;
                for (var i = 0; i < length_2; i++) {
                    item = inputElement.attributes[i];
                    if (item.specified && /^data-/.test(item.nodeName)) {
                        result[item.nodeName.substring(5)] = item.nodeValue;
                    }
                }
                return result;
            }
        };
        return ElementHelper;
    }());
    Materal.ElementHelper = ElementHelper;
    /**
     * 事件帮助类
     */
    var EventHelper = /** @class */ (function () {
        function EventHelper() {
        }
        /**
         * 获得事件触发元素
         * @returns 触发元素
         */
        EventHelper.getEventTarget = function (inputEvent) {
            return inputEvent["target"] || inputEvent["srcElement"];
        };
        return EventHelper;
    }());
    Materal.EventHelper = EventHelper;
    /**
     * Json帮助类
     */
    var JsonHelper = /** @class */ (function () {
        function JsonHelper() {
        }
        /**
         * json转换为对象
         * @param jsonString json字符串
         * @returns 对象
         */
        JsonHelper.jsonStringToObject = function (jsonString) {
            return JSON.parse ? JSON.parse(jsonString) : eval("(" + jsonString + ")");
        };
        /**
         * 对象转换为Json
         * @param obj json对象
         * @returns json字符串
         */
        JsonHelper.objectToJsonString = function (obj) {
            var result = "";
            var isArray;
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    isArray = false;
                    var typeString = Common.getType(obj[key]);
                    if (obj instanceof Array) {
                        isArray = true;
                    }
                    if (typeString === "string") {
                        if (isArray) {
                            result += "\"" + obj[key].toString() + "\",";
                        }
                        else {
                            result += "\"" + key + "\":\"" + obj[key].toString() + "\",";
                        }
                    }
                    else if (obj[key] instanceof RegExp) {
                        if (isArray) {
                            result += obj[key].toString() + ",";
                        }
                        else {
                            result += "\"" + key + "\":\"" + obj[key].toString() + "\",";
                        }
                    }
                    else if (obj[key] instanceof Array) {
                        result += "\"" + key + "\":" + this.objectToJsonString(obj[key]) + ",";
                    }
                    else if (typeString === "boolean") {
                        if (isArray) {
                            result += obj[key].toString() + ",";
                        }
                        else {
                            result += "\"" + key + "\":" + obj[key].toString() + ",";
                        }
                    }
                    else if (typeString === "number") {
                        if (isArray) {
                            result += obj[key].toString() + ",";
                        }
                        else {
                            result += "\"" + key + "\":" + obj[key].toString() + ",";
                        }
                    }
                    else if (obj[key] instanceof Object) {
                        if (isArray) {
                            result += this.objectToJsonString(obj[key]) + ",";
                        }
                        else {
                            result += "\"" + key + "\":" + this.objectToJsonString(obj[key]) + ",";
                        }
                    }
                    else if (!obj[key] || obj[key] instanceof Function) {
                        if (isArray) {
                            result += "null,";
                        }
                        else {
                            result += "\"" + key + "\":null,";
                        }
                    }
                }
            }
            if (isArray) {
                result = "[" + result.slice(0, -1) + "]";
            }
            else {
                result = "{" + result.slice(0, -1) + "}";
            }
            return result;
        };
        return JsonHelper;
    }());
    Materal.JsonHelper = JsonHelper;
    /**
     * 数学帮助类
     */
    var MathHelper = /** @class */ (function () {
        function MathHelper() {
        }
        /**
         * 返回一个随机数
         * @param min 最小值
         * @param max 最大值
         * @returns 随机数
         */
        MathHelper.getRandom = function (min, max) {
            return Math.floor(Math.random() * max + min);
        };
        /**
         * 获取四边形的外接圆半径
         * @param length 长
         * @param width 宽
         * @param isRound 是圆形
         * @returns 外接圆半径
         */
        MathHelper.getCircumscribedCircleRadius = function (length, width, isRound) {
            var max = Math.max(length, width);
            //正方形的对角线=边长^2*2
            var diameter = Math.sqrt(Math.pow(max, 2) * 2);
            //外接圆的直径=正方形的对角线
            //圆的半径=直径/2
            var radius = diameter / 2;
            if (isRound) {
                radius = Math.round(radius);
            }
            return radius;
        };
        return MathHelper;
    }());
    Materal.MathHelper = MathHelper;
    /**
     * HttpMethod枚举
     */
    var HttpMethod;
    (function (HttpMethod) {
        HttpMethod[HttpMethod["GET"] = ("get")] = "GET";
        HttpMethod[HttpMethod["POST"] = ("post")] = "POST";
    })(HttpMethod = Materal.HttpMethod || (Materal.HttpMethod = {}));
    /**
     * Http配置类
     */
    var HttpConfigModel = /** @class */ (function () {
        /**
         * 构造方法
         * @param url URL地址
         * @param method HttpMethod类型
         * @param data 要发送的数据
         * @param dataType 数据类型
         * @param success 成功方法
         * @param error 失败方法
         * @param complete 成功错误都执行的方法
         */
        function HttpConfigModel(url, method, data, heads, success, error, complete) {
            if (method === void 0) { method = HttpMethod.POST; }
            if (data === void 0) { data = null; }
            if (heads === void 0) { heads = null; }
            if (success === void 0) { success = null; }
            if (error === void 0) { error = null; }
            if (complete === void 0) { complete = null; }
            /**
             * 超时时间
             */
            this.timeout = 15000;
            /**
             * 异步发送
             */
            this.async = true;
            if (heads == null) {
                heads = {
                    "Content-type": "application/json"
                };
            }
            this.url = url;
            this.method = method;
            this.data = data;
            this.heads = heads;
            this.success = success;
            this.error = error;
            this.complete = complete;
        }
        return HttpConfigModel;
    }());
    Materal.HttpConfigModel = HttpConfigModel;
    /**
     * Http帮助类
     */
    var HttpManager = /** @class */ (function () {
        function HttpManager() {
        }
        /**
         * 获取XMLHttpRequest对象
         * @param config 配置对象
         * @returns HttpRequest对象
         */
        HttpManager.getHttpRequest = function (config) {
            var xhr;
            if (window["XMLHttpRequest"]) {
                xhr = new XMLHttpRequest();
            }
            else {
                xhr = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhr.onreadystatechange = function () {
                HttpManager.readyStateChange(xhr, config);
            };
            return xhr;
        };
        /**
         * 状态更改方法
         * @param xhr XMLHttpRequest对象
         * @param config 配置对象
         */
        HttpManager.readyStateChange = function (xhr, config) {
            if (xhr.readyState === 4) {
                var resM = void 0;
                try {
                    resM = JSON.parse(xhr.responseText);
                }
                catch (ex) {
                    resM = xhr.responseText;
                }
                if ((xhr.status >= 200 && xhr.status < 300) || xhr.status === 304) {
                    if (config.complete) {
                        config.complete(resM, xhr, xhr.status);
                    }
                    if (config.success) {
                        config.success(resM, xhr, xhr.status);
                    }
                }
                else {
                    if (config.complete) {
                        config.complete(resM, xhr, xhr.status);
                    }
                    if (config.error) {
                        config.error(resM, xhr, xhr.status);
                    }
                }
            }
        };
        /**
         * 序列化参数
         * @param data 要序列化的参数
         * @returns 序列化后的字符串
         */
        HttpManager.serialize = function (data) {
            var result = new Array();
            for (var name_1 in data) {
                if (data.hasOwnProperty(name_1)) {
                    if (typeof data[name_1] === "function") {
                        continue;
                    }
                    if (Common.getType(data[name_1]) === "Object") {
                        result.push(this.serialize(data[name_1]));
                    }
                    else {
                        name_1 = encodeURIComponent(name_1);
                        var value = void 0;
                        if (data[name_1]) {
                            value = data[name_1].toString();
                            value = encodeURIComponent(value);
                        }
                        else {
                            value = "";
                        }
                        result.push(name_1 + "=" + value);
                    }
                }
            }
            ;
            return result.join("&");
        };
        /**
         * 发送Post请求
         * @param config 配置对象
         */
        HttpManager.sendPost = function (config) {
            var xhr = this.getHttpRequest(config);
            xhr.open(String(config.method), config.url, config.async);
            for (var head in config.heads) {
                if (config.heads.hasOwnProperty(head)) {
                    xhr.setRequestHeader(head, config.heads[head]);
                }
            }
            if (config.data) {
                xhr.send(JSON.stringify(config.data));
            }
            else {
                xhr.send(null);
            }
        };
        /**
         * 发送Get请求
         * @param config 配置对象
         */
        HttpManager.sendGet = function (config) {
            var xhr = HttpManager.getHttpRequest(config);
            var url = config.url;
            if (config.data) {
                url += "?" + HttpManager.serialize(config.data);
            }
            xhr.open(String(config.method), url, config.async);
            for (var head in config.heads) {
                if (config.heads.hasOwnProperty(head)) {
                    xhr.setRequestHeader(head, config.heads[head]);
                }
            }
            xhr.send(null);
        };
        /**
         * 发送请求
         * @param config 配置对象
         */
        HttpManager.send = function (config) {
            if (config.method === HttpMethod.POST) {
                HttpManager.sendPost(config);
            }
            else {
                HttpManager.sendGet(config);
            }
        };
        return HttpManager;
    }());
    Materal.HttpManager = HttpManager;
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
        /**
         * 是否拥有键
         * @param key
         */
        Dictionary.prototype.hasKey = function (key) {
            return this.data.hasOwnProperty(key);
        };
        return Dictionary;
    }());
    Materal.Dictionary = Dictionary;
    /**
    * 本地存储帮助类
    */
    var LocalDataManager = /** @class */ (function () {
        function LocalDataManager() {
        }
        /**
         * 是否支持本地存储
         * @returns 是否支持
         */
        LocalDataManager.isLocalStorage = function () {
            if (window.localStorage) {
                return true;
            }
            else {
                return false;
            }
        };
        /**
         * 清空本地存储对象
         */
        LocalDataManager.cleanLocalData = function () {
            if (this.isLocalStorage()) {
                window.localStorage.clear();
            }
        };
        /**
         * 移除本地存储对象
         * @param key Key值
         */
        LocalDataManager.removeLocalData = function (key) {
            if (this.isLocalStorage() && key) {
                window.localStorage.removeItem(key);
            }
        };
        /**
         * 设置本地存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        LocalDataManager.setLocalData = function (key, value, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (this.isLocalStorage() && key && value) {
                this.removeLocalData(key);
                if (isJson) {
                    window.localStorage.setItem(key, JSON.stringify(value));
                }
                else {
                    window.localStorage.setItem(key, value.toString());
                }
            }
        };
        /**
         * 获取本地存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据
         */
        LocalDataManager.getLocalData = function (key, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (this.isLocalStorage() && key) {
                if (isJson) {
                    return JsonHelper.jsonStringToObject(window.localStorage.getItem(key));
                }
                else {
                    return window.localStorage.getItem(key);
                }
            }
            return null;
        };
        /**
         * 是否支持网页存储
         * @returns 是否支持
         */
        LocalDataManager.isSessionStorage = function () {
            if (window.sessionStorage) {
                return true;
            }
            else {
                return false;
            }
        };
        /**
         * 清空网页存储对象
         */
        LocalDataManager.cleanSessionData = function () {
            if (this.isSessionStorage()) {
                window.sessionStorage.clear();
            }
        };
        /**
         * 移除网页存储对象
         * @param key Key值
         */
        LocalDataManager.removeSessionData = function (key) {
            if (this.isSessionStorage() && key) {
                window.sessionStorage.removeItem(key);
            }
        };
        /**
         * 设置网页存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        LocalDataManager.setSessionData = function (key, value, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (!isJson && isJson !== false) {
                isJson = true;
            }
            if (this.isSessionStorage() && key && value) {
                this.removeSessionData(key);
                if (isJson) {
                    window.sessionStorage.setItem(key, JSON.stringify(value));
                }
                else {
                    window.sessionStorage.setItem(key, value.toString());
                }
            }
        };
        /**
         * 获取网页存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据
         */
        LocalDataManager.getSessionData = function (key, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (this.isSessionStorage() && key) {
                if (isJson) {
                    return JSON.parse(window.sessionStorage.getItem(key));
                }
                else {
                    return window.sessionStorage.getItem(key);
                }
            }
            return null;
        };
        /**
         * 获得有效时间
         * @param timeValue 值
         * @param timeType 单位
         * @returns 计算后的时间
         */
        LocalDataManager.getTime = function (timeValue, timeType) {
            if (timeValue === void 0) { timeValue = 10000; }
            if (timeType === void 0) { timeType = TimeType.Minutes; }
            switch (timeType) {
                case TimeType.Years:
                    timeValue = 60 * 60 * 24 * 365 * timeValue * 1000;
                    break;
                case TimeType.Months:
                    timeValue = 60 * 60 * 24 * 30 * timeValue * 1000;
                    break;
                case TimeType.Day:
                    timeValue = 60 * 60 * 24 * timeValue * 1000;
                    break;
                case TimeType.Hours:
                    timeValue = 60 * 60 * timeValue * 1000;
                    break;
                case TimeType.Minutes:
                    timeValue = 60 * timeValue * 1000;
                    break;
                case TimeType.Seconds:
                    timeValue = timeValue * 1000;
                    break;
                case TimeType.Milliseconds:
                    break;
            }
            return timeValue;
        };
        /**
         * 设置一个Cookie
         * @param key Key值
         * @param value 要保存的值
         * @param time 持续时间
         * @param timeType 单位(默认s[秒])
         */
        LocalDataManager.setCookie = function (key, value, isJson, timeValue, timeType, path) {
            if (isJson === void 0) { isJson = true; }
            if (timeValue === void 0) { timeValue = 60; }
            if (timeType === void 0) { timeType = TimeType.Minutes; }
            if (path === void 0) { path = "/"; }
            if (isJson) {
                document.cookie = key + "=" + JSON.stringify(value) + ";max-age=" + this.getTime(timeValue, timeType) + ";path=" + path;
            }
            else {
                document.cookie = key + "=" + value + ";max-age=" + this.getTime(timeValue, timeType) + ";path=" + path;
            }
        };
        /**
         * 删除一个Cookie
         * @param key Key值
         */
        LocalDataManager.removeCookie = function (key) {
            document.cookie = key + "=;max-age=0";
        };
        /**
         * 获得所有Cookie
         * @returns Cookie对象
         */
        LocalDataManager.getAllCookie = function () {
            var cookies = document.cookie.split(";");
            var cookie = new Array();
            var localCookie = new Object();
            for (var i = 0; i < cookies.length; i++) {
                if (!StringHelper.isNullOrrUndefinedOrEmpty(cookies[i])) {
                    cookie[i] = cookies[i].trim().split("=");
                    if (cookie[i][0] && cookie[i][1]) {
                        localCookie[cookie[i][0]] = cookie[i][1];
                    }
                }
            }
            return localCookie;
        };
        /**
         * 获得Cookie
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns
         */
        LocalDataManager.getCookie = function (key, isJson) {
            if (isJson === void 0) { isJson = true; }
            var resM = this.getAllCookie();
            if (isJson && resM && resM[key]) {
                return JSON.parse(resM[key]);
            }
            else {
                return null;
            }
        };
        /**
         * 设置数据
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         * @param time 时间
         * @param timeType 时间类型
         */
        LocalDataManager.setData = function (key, value, isJson, time, timeType) {
            if (isJson === void 0) { isJson = true; }
            if (time === void 0) { time = 60; }
            if (timeType === void 0) { timeType = TimeType.Minutes; }
            if (this.isLocalStorage()) {
                this.setLocalData(key, value, isJson);
            }
            else {
                this.setCookie(key, value, isJson, time, timeType);
            }
        };
        /**
         * 获得数据
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns 获取到的数据
         */
        LocalDataManager.getData = function (key, isJson) {
            if (isJson === void 0) { isJson = true; }
            if (this.isLocalStorage()) {
                return this.getLocalData(key, isJson);
            }
            else {
                return this.getCookie(key, isJson);
            }
        };
        /**
         * 移除数据
         * @param key Key值
         */
        LocalDataManager.removeData = function (key) {
            if (this.isLocalStorage()) {
                this.removeLocalData(key);
            }
            else {
                this.removeCookie(key);
            }
        };
        return LocalDataManager;
    }());
    Materal.LocalDataManager = LocalDataManager;
    /**
     * 实现引擎模型
     */
    var EngineInfoModel = /** @class */ (function () {
        function EngineInfoModel() {
            //是否为Trident引擎
            this.trident = false;
            //是否为Gecko引擎
            this.gecko = false;
            //是否为WebKit引擎
            this.webKit = false;
            //是否为KHTML引擎
            this.kHtml = false;
            //是否为Presto引擎
            this.presto = false;
            //具体版本号
            this.version = "";
        }
        return EngineInfoModel;
    }());
    Materal.EngineInfoModel = EngineInfoModel;
    /**
     * 浏览器模型
     */
    var BrowserInfoModel = /** @class */ (function () {
        function BrowserInfoModel() {
            //是否为IE浏览器
            this.internetExplorer = false;
            //是否为Firefox浏览器
            this.firefox = false;
            //是否为Safari浏览器
            this.safari = false;
            //是否为Konqueror浏览器
            this.konqueror = false;
            //是否为Opera浏览器
            this.opera = false;
            //是否为Chrome浏览器
            this.chrome = false;
            //是否为Edge浏览器
            this.edge = false;
            //是否为QQ浏览器
            this.qq = false;
            //是否为UC浏览器
            this.uc = false;
            //是否为Maxthon(遨游)浏览器
            this.maxthon = false;
            //是否为微信浏览器
            this.weChat = false;
            //具体版本号
            this.version = "";
        }
        return BrowserInfoModel;
    }());
    Materal.BrowserInfoModel = BrowserInfoModel;
    /**
     * 系统模型
     */
    var SystemInfoModel = /** @class */ (function () {
        function SystemInfoModel() {
            //是否为Windows操作系统
            this.windows = false;
            //是否为WindowsMobile操作系统
            this.windowsMobile = false;
            //Windows版本
            this.windowsVersion = "";
            //是否为Mac操作系统
            this.mac = false;
            //是否为Unix操作系统
            this.unix = false;
            //是否为Linux操作系统
            this.linux = false;
            //是否为iPhone操作系统
            this.iPhone = false;
            //是否为iPod操作系统
            this.iPod = false;
            //是否为Windows操作系统
            this.iPad = false;
            //是否为Windows操作系统
            this.ios = false;
            //IOS版本
            this.iosVersion = "";
            //是否为Android操作系统
            this.android = false;
            //Android版本
            this.androidVersion = "";
            //是否为NokiaN操作系统
            this.nokiaN = false;
            //是否为Wii操作系统
            this.wii = false;
            //是否为PS操作系统
            this.ps = false;
        }
        return SystemInfoModel;
    }());
    Materal.SystemInfoModel = SystemInfoModel;
    /**
     * 客户端信息模型
     */
    var ClientInfoModel = /** @class */ (function () {
        /**
         * 客户端信息模型
         */
        function ClientInfoModel() {
            this.engineInfoModel = new EngineInfoModel();
            this.browserInfoModel = new BrowserInfoModel();
            this.systemInfoModel = new SystemInfoModel();
            //检测呈现引擎和浏览器
            var userAgent = navigator.userAgent;
            if (window["opera"]) {
                this.engineInfoModel.version = this.engineInfoModel.version = window["opera"].version();
                this.engineInfoModel.presto = this.browserInfoModel.opera = true;
            }
            else if (/AppleWebKit\/(\S+)/.test(userAgent)) {
                this.engineInfoModel.version = RegExp["$1"];
                this.engineInfoModel.webKit = true;
                if (/MicroMessenger\/(\S+)/.test(userAgent)) {
                    this.browserInfoModel.version = RegExp["$1"];
                    this.browserInfoModel.weChat = true;
                }
                else if (/Edge\/(\S+)/.test(userAgent)) {
                    this.browserInfoModel.version = RegExp["$1"];
                    this.browserInfoModel.edge = true;
                }
                else if (/QQBrowser\/(\S+)/.test(userAgent)) {
                    this.browserInfoModel.version = RegExp["$1"];
                    this.browserInfoModel.qq = true;
                }
                else if (/UBrowser\/(\S+)/.test(userAgent)) {
                    this.browserInfoModel.version = RegExp["$1"];
                    this.browserInfoModel.uc = true;
                }
                else if (/Maxthon\/(\S+)/.test(userAgent)) {
                    this.browserInfoModel.version = RegExp["$1"];
                    this.browserInfoModel.maxthon = true;
                }
                else if (/Chrome\/(\S+)/.test(userAgent)) {
                    this.browserInfoModel.version = RegExp["$1"];
                    this.browserInfoModel.chrome = true;
                }
                else if (/Safari\/(\S+)/.test(userAgent)) {
                    this.browserInfoModel.version = RegExp["$1"];
                    this.browserInfoModel.safari = true;
                }
                else {
                    if (this.engineInfoModel.webKit) {
                        var safariVersion = "";
                        var webKitVersion = parseInt(this.engineInfoModel.version);
                        if (webKitVersion < 100) {
                            safariVersion = "1";
                        }
                        else if (webKitVersion < 312) {
                            safariVersion = "1.2";
                        }
                        else if (webKitVersion < 412) {
                            safariVersion = "1.3";
                        }
                        else {
                            safariVersion = "2";
                        }
                        this.browserInfoModel.version = safariVersion;
                        this.browserInfoModel.safari = true;
                    }
                }
            }
            else if (/KHTML\/(\S+)/.test(userAgent) || /Konqueror\/([^;]+)/.test(userAgent)) {
                this.engineInfoModel.version = this.browserInfoModel.version = RegExp["$1"];
                this.engineInfoModel.kHtml = this.browserInfoModel.konqueror = true;
            }
            else if (/rv:([^\)]+)\) Gecko\/\d{8}/.test(userAgent)) {
                this.engineInfoModel.version = RegExp["$1"];
                this.engineInfoModel.gecko = true;
                if (/Firefox\/(\S+)/.test(userAgent)) {
                    this.browserInfoModel.version = RegExp["$1"];
                    this.browserInfoModel.firefox = true;
                }
            }
            else if (/MSIE ([^;]+)/.test(userAgent)) {
                this.engineInfoModel.version = this.browserInfoModel.version = RegExp["$1"];
                this.engineInfoModel.trident = this.browserInfoModel.internetExplorer = true;
            }
            else {
                if (window["ActiveXObject"] || "ActiveXObject" in window) {
                    this.engineInfoModel.version = this.browserInfoModel.version = "11";
                    this.engineInfoModel.trident = this.browserInfoModel.internetExplorer = true;
                }
            }
            //检测平台
            var p = navigator.platform;
            this.systemInfoModel.windows = p.indexOf("Win") === 0;
            if (this.systemInfoModel.windows) {
                if (/Win(?:dows )?([^do]{2})\s?(\d+\.\d+)?/.test(userAgent)) {
                    if (RegExp["$1"] === "NT") {
                        switch (RegExp["$2"]) {
                            case "5.0":
                                this.systemInfoModel.windowsVersion = "2000";
                                break;
                            case "5.1":
                                this.systemInfoModel.windowsVersion = "XP";
                                break;
                            case "6.0":
                                this.systemInfoModel.windowsVersion = "Vista";
                                break;
                            case "6.1":
                                this.systemInfoModel.windowsVersion = "7";
                                break;
                            case "6.2":
                                this.systemInfoModel.windowsVersion = "8";
                                break;
                            case "10.0":
                                this.systemInfoModel.windowsVersion = "10";
                                break;
                            default:
                                this.systemInfoModel.windowsVersion = "NT";
                                break;
                        }
                    }
                    else if (RegExp["$1"] === "9X") {
                        this.systemInfoModel.windowsVersion = "ME";
                    }
                    else {
                        this.systemInfoModel.windowsVersion = RegExp["$1"];
                    }
                }
                if (this.systemInfoModel.windowsVersion === "CE") {
                    this.systemInfoModel.windowsMobile = true;
                }
                else if (this.systemInfoModel.windowsVersion === "Ph") {
                    if (/Windows Phone OS (\d+.\d+)/.test(userAgent)) {
                        this.systemInfoModel.windowsMobile = true;
                    }
                }
            }
            this.systemInfoModel.mac = p.indexOf("Mac") === 0;
            if (this.systemInfoModel.mac && userAgent.indexOf("Mobile") > -1) {
                if (/CPU (?:iPhone)?OS (\d+_\d+)/.test(userAgent)) {
                    this.systemInfoModel.ios = true;
                    this.systemInfoModel.iosVersion = RegExp["$1"].replace("_", ".");
                }
                else {
                    this.systemInfoModel.ios = true;
                    this.systemInfoModel.iosVersion = "2";
                }
            }
            this.systemInfoModel.unix = p.indexOf("X11") === 0;
            this.systemInfoModel.linux = p.indexOf("Linux") === 0;
            this.systemInfoModel.iPhone = p.indexOf("iPhone") === 0;
            this.systemInfoModel.iPod = p.indexOf("iPod") === 0;
            this.systemInfoModel.iPad = p.indexOf("iPad") === 0;
            this.systemInfoModel.nokiaN = userAgent.indexOf("NokiaN") > -1;
            this.systemInfoModel.wii = userAgent.indexOf("Wii") > -1;
            this.systemInfoModel.ps = /playstation/i.test(userAgent);
            if (/Android (\d+\.\d+)/.test(userAgent)) {
                this.systemInfoModel.android = true;
                this.systemInfoModel.androidVersion = RegExp["$1"];
            }
        }
        Object.defineProperty(ClientInfoModel.prototype, "engineInfo", {
            /**
             * 实现引擎信息
             */
            get: function () {
                return Common.clone(this.engineInfoModel);
            },
            enumerable: false,
            configurable: true
        });
        Object.defineProperty(ClientInfoModel.prototype, "browserInfo", {
            /**
             * 浏览器信息
             */
            get: function () {
                return Common.clone(this.browserInfoModel);
            },
            enumerable: false,
            configurable: true
        });
        Object.defineProperty(ClientInfoModel.prototype, "systemInfo", {
            /**
             * 系统信息
             */
            get: function () {
                return Common.clone(this.systemInfoModel);
            },
            enumerable: false,
            configurable: true
        });
        return ClientInfoModel;
    }());
    Materal.ClientInfoModel = ClientInfoModel;
})(Materal || (Materal = {}));
//# sourceMappingURL=Materal.js.map