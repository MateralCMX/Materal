namespace Materal {
    /*
     * 时间类型
     */
    export enum TimeType {
        /**
         * 年
         */
        Years = 0,
        /**
         * 月
         */
        Months = 1,
        /**
         * 日
         */
        Day = 2,
        /**
         * 时
         */
        Hours = 3,
        /**
         * 分
         */
        Minutes = 4,
        /**
         * 秒
         */
        Seconds = 5,
        /**
         * 毫秒
         */
        Milliseconds = 6
    }
    /**
     * 公共
     */
    export class Common {
        /**
         * 是否为undefined
         * @param obj 需要判断的对象
         * @returns 判断结果
         */
        static isUndefined(obj: any): boolean {
            return typeof obj === "undefined";
        }
        /**
         * 是否为null或者undefined
         * @param obj 需要判断的对象
         * @returns 判断结果
         */
        static isNullOrUndefined(obj: any): boolean {
            return obj === null || this.isUndefined(obj);
        }
        /**
         * 鉴别类型
         * @param obj 传入对象
         * @param includeCustom 包括自定义类型
         * @returns 对象类型 
         */
        static getType(obj: any, includeCustom: boolean = true): string {
            let lowercase = true;
            const objType = typeof obj;
            let result: string;
            if (objType === "object") {
                if (obj === null) {
                    result = "null";
                } else {
                    lowercase = false;
                    result = Object.prototype.toString.call(obj).slice(8, -1);
                    if (result === "Object" &&
                        !this.isNullOrUndefined(obj.constructor) &&
                        obj.constructor.name !== "Object" &&
                        includeCustom) {
                        result = obj.constructor.name;
                    }
                }
            } else {
                result = objType;
            }
            if (!StringHelper.isNullOrrUndefinedOrEmpty(result) && lowercase) {
                result = result.toLowerCase();
            }
            return result;
        }
        /**
         * 克隆对象
         * @param obj 要克隆的对象
         */
        static clone(obj: any): any {
            const objectType = this.getType(obj, false);
            let result: any;
            if (objectType === "Object") {
                result = new Object();
            }
            else if (objectType === "array") {
                result = new Array();
            }
            else {
                result = obj;
            }
            for (let i in obj) {
                if (obj.hasOwnProperty(i)) {
                    const copy = obj[i];
                    const subObjectType = this.getType(copy, false);
                    if (subObjectType === "Object" || subObjectType === "array") {
                        result[i] = this.arguments.callee(copy);
                    } else {
                        result[i] = copy;
                    }
                }
            }
            return result;
        }
    }
    /**
     * 字符串帮助类
     */
    export class StringHelper {
        /**
         * 是否为Empty字符串
         * @param inputStr 需要判断的对象
         * @returns 判断结果
         */
        static isEmpty(inputStr: string): boolean {
            return inputStr === "";
        }
        /**
         * 是否为null或undefined或Empty字符串
         * @param inputStr 输入的字符串
         * 判定结果
         */
        static isNullOrrUndefinedOrEmpty(inputStr: string): boolean {
            return Common.isNullOrUndefined(inputStr) || this.isEmpty(inputStr);
        }
        /**
         * 移除左边空格
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        static trimLeft(inputStr: string): string {
            while (inputStr.substr(0, 1) === " ") {
                inputStr = inputStr.substr(1, inputStr.length - 1);
            }
            return inputStr;
        }
        /**
         * 移除右边空格
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        static trimRight(inputStr: string): string {
            while (inputStr.substr(inputStr.length - 2, 1) === " ") {
                inputStr = inputStr.substr(0, inputStr.length - 2);
            }
            return inputStr;
        }
        /**
         * 移除所有空格
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        static trimAll(inputStr: string): string {
            return inputStr.replace(/\s/g, "");
        }
        /**
         * 移除多余空格(连续的空格将变成一个)
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        static simplifyMultiSpaceToSingle(inputStr: string): string {
            return inputStr.replace(/\s{2,}/g, " ");
        }
        /**
         * 左侧填充字符
         * @param inputStr 输入的字符串
         * @param length 位数
         * @param character 填充字符
         */
        static padLeft(inputStr: string, length: number, character: string): string {
            for (let i = inputStr.length; i < length; i++) {
                inputStr = character + inputStr;
            }
            return inputStr.substr(0, length);
        }
        /**
         * 右侧填充字符
         * @param length 位数
         * @param character 填充字符
         */
        static padRight(inputStr: string, length: number, character: string): string {
            for (let i = inputStr.length; i < length; i++) {
                inputStr = inputStr + character;
            }
            return inputStr.substr(0, length);
        }

        /**
         * 获取32位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        static get32Md5String(str: string, isLower: boolean = false): string {
            function l(a) {
                return h(g(o(a), a.length * 8));
            }
            function m(e) {
                let b = "0123456789ABCDEF";
                if (isLower) {
                    b = b.toLowerCase();
                }
                let c = "";
                let d;
                for (let a = 0; a < e.length; a++) {
                    d = e.charCodeAt(a);
                    c += b.charAt(d >>> 4 & 15) + b.charAt(d & 15);
                }
                return c;
            }
            function n(d) {
                let b = "";
                let c = -1;
                let a, e;
                while (++c < d.length) {
                    a = d.charCodeAt(c),
                        e = c + 1 < d.length ? d.charCodeAt(c + 1) : 0;
                    55296 <= a && a <= 56319 && 56320 <= e && e <= 57343 && (a = 65536 + ((a & 1023) << 10) + (e & 1023), c++);
                    a <= 127 ? b += String.fromCharCode(a) : a <= 2047 ? b += String.fromCharCode(192 | a >>> 6 & 31, 128 | a & 63) : a <= 65535 ? b += String.fromCharCode(224 | a >>> 12 & 15, 128 | a >>> 6 & 63, 128 | a & 63) : a <= 2097151 && (b += String.fromCharCode(240 | a >>> 18 & 7, 128 | a >>> 12 & 63, 128 | a >>> 6 & 63, 128 | a & 63));
                }
                return b;
            }
            function o(c) {
                let b = Array(c.length >> 2);
                for (let a = 0; a < b.length; a++) {
                    b[a] = 0;
                }
                for (let a = 0; a < c.length * 8; a += 8) {
                    b[a >> 5] |= (c.charCodeAt(a / 8) & 255) << a % 32;
                }
                return b;
            }
            function h(c) {
                let b = "";
                for (let a = 0; a < c.length * 32; a += 8) {
                    b += String.fromCharCode(c[a >> 5] >>> a % 32 & 255);
                }
                return b;
            }
            function g(j, l) {
                j[l >> 5] |= 128 << l % 32, j[(l + 64 >>> 9 << 4) + 14] = l;
                let g = 1732584193;
                let h = -271733879;
                let i = -1732584194;
                let f = 271733878;
                for (let k = 0; k < j.length; k += 16) {
                    let n = g;
                    let o = h;
                    let p = i;
                    let m = f;
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
                    g = e(g, n);
                    h = e(h, o);
                    i = e(i, p);
                    f = e(f, m);
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
                let a = (b & 65535) + (c & 65535);
                let d = (b >> 16) + (c >> 16) + (a >> 16);
                return d << 16 | a & 65535;
            }
            function j(a, b) {
                return a << b | a >>> 32 - b;
            }
            return m(l(n(str)));
        }
        /**
         * 获取16位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        static get16Md5String(str: string, isLower: boolean = false): string {
            return this.get32Md5String(str, isLower).substr(8, 16);
        }
        /**
         * 转换为二进制字符串
         * @param str 要转换的字符串
         * @returns 转换后的字符串 
         */
        static convertToBinary(str: string): string {
            const strList: string = Array.prototype.map.call(str, c => c.charCodeAt(0).toString(2));
            let resStr = "";
            for (let i = 0; i < strList.length; i++) {
                resStr += StringHelper.padLeft(strList[i], 8, "0");
            }
            return resStr;
        }
        /**
         * 隐藏代码
         * @param codeStr 要隐藏的代码
         * @returns 隐藏后的代码
         */
        static hideCode(codeStr: string): string {
            let resStr = this.convertToBinary(codeStr);
            resStr = resStr.replace(/0/g, "\u200d");
            resStr = resStr.replace(/1/g, "\u200c");
            return resStr;
        }
        /**
         * 显示代码
         * @param codeStr 被隐藏的代码
         * @returns 显示的代码 
         */
        static showCode(codeStr: string): string {
            const resStr = codeStr.replace(/.{8}/g, u => String.fromCharCode(parseInt(u.replace(/\u200c/g, "1").replace(/\u200d/g, "0"), 2)));
            return resStr;
        }
    }
    /**
     * 时间帮助类
     */
    export class DateHelper {
        /**
         * 获得时间差
         * @param inputDateTime 输入时间
         * @param targetDate 对比时间
         * @param TimeType 返回类型
         * @param isFloor 向下取整
         * @returns 时间差
         */
        static getTimeDifference(inputDateTime: Date, targetDate: Date, timeType: TimeType, isFloor: boolean = true): number {
            let timeDifference = inputDateTime.getTime() - targetDate.getTime();
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
        }
        /**
         * 时间字符串格式化
         * @param inputDateTime 输入时间
         * @param formatStr 格式化字符串[y年|M月|d日|H时|m分|s秒|S毫秒|q季度]
         * @returns 格式化后的时间字符串
         */
        static formatString(inputDateTime: Date, formatStr: string): string {
            const formatData = {
                "M+": inputDateTime.getMonth() + 1, //月份 
                "d+": inputDateTime.getDate(), //日 
                "H+": inputDateTime.getHours(), //小时 
                "m+": inputDateTime.getMinutes(), //分 
                "s+": inputDateTime.getSeconds(), //秒 
                "q+": Math.floor((inputDateTime.getMonth() + 3) / 3), //季度 
                "S": inputDateTime.getMilliseconds() //毫秒 
            };
            if (/(y+)/.test(formatStr)) {
                formatStr = formatStr.replace(RegExp.$1, (inputDateTime.getFullYear() + "").substr(4 - RegExp.$1.length));
            }
            for (let data in formatData) {
                if (formatData.hasOwnProperty(data)) {
                    if (new RegExp(`(${data})`).test(formatStr)) {
                        let tempString: string;
                        if (RegExp.$1.length === 1) {
                            tempString = formatData[data];
                        } else {
                            tempString = (`00${formatData[data]}`).substr((`${formatData[data]}`).length);
                        }
                        formatStr = formatStr.replace(RegExp.$1, tempString);
                    }
                }
            }
            return formatStr;
        }
        /**
         * 获取Input dateTime设置值字符串
         * @param inputDateTime 输入时间
         * @param dateTime 要设置的时间
         * @returns 可以设置给Input的时间值
         */
        static getInputDateTimeValueStr(inputDateTime: Date): string {
            return this.formatString(inputDateTime, "yyyy-MM-ddTHH:mm:ss");
        }
        /**
         * 获取对应时区时间
         * @param inputDateTime 输入时间
         * @param timeZone 时区
         * @returns 对应时区时间
         */
        static getDateByTimeZone(inputDateTime: Date, timeZone: number): Date {
            const len = inputDateTime.getTime();
            const offset = inputDateTime.getTimezoneOffset() * 60000;
            const utcTime = len + offset;
            const result = new Date(utcTime + 3600000 * timeZone);
            return result;
        }
        /**
         * 将其他时区时间转换为对应时区时间
         * @param inputDateTime 输入时间
         * @param timeZone 时区[null则自动为本地时区]
         * @returns 对应时区时间
         */
        static convertTimeZone(inputDateTime: Date, timeZone: number): Date {
            if (!timeZone) {
                timeZone = inputDateTime.getTimezoneOffset() / 60;
            }
            inputDateTime.setTime(inputDateTime.getTime() - timeZone * 60 * 60 * 1000);
            return inputDateTime;
        }
    }
    /**
     * 数组帮助类
     */
    export class ArrayHelper {
        /**
         * 插入数组
         * @param inputArray 原数组
         * @param item 要插入的对象
         * @param index 位序
         * @returns 插入后的数组
         */
        static insert<T>(inputArray: Array<T>, item: T, index: number): Array<T> {
            inputArray.splice(index, 0, item);
            return inputArray;
        }
        /**
         * 删除数组
         * @param inputArray 原数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        static remove<T>(inputArray: Array<T>, item: T): Array<T> {
            const index = inputArray.indexOf(item);
            if (index >= 0) {
                inputArray = this.removeAt(inputArray, index);
            }
            return inputArray;
        }
        /**
         * 删除所有数组
         * @param inputArray 原数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        static removeAll<T>(inputArray: Array<T>, item: T): Array<T> {
            let index = inputArray.indexOf(item);
            while (index >= 0) {
                inputArray = this.removeAt(inputArray, index);
                index = inputArray.indexOf(item);
            }
            return inputArray;
        }
        /**
         * 移除
         * @param inputArray 原数组
         * @param index 位序
         * @returns 移除后的数组
         */
        static removeAt<T>(inputArray: Array<T>, index: number): Array<T> {
            const count = inputArray.length;
            inputArray = inputArray.splice(index, 1);
            if (count === inputArray.length && count === 1) {
                inputArray = [];
            }
            return inputArray;
        }
        /**
         * 清空
         * @param inputArray 原数组
         * @returns 清空后的数组
         */
        static clear<T>(inputArray: Array<T>): Array<T> {
            inputArray = inputArray.splice(0, 0);
            return inputArray;
        }
    }
    /**
     * 地址帮助类
     */
    export class LocationHelper {
        /**
         * 获得参数
         * @returns URL参数
         */
        static getUrlParamsByArray() {
            let result: string[] = null;
            let paramString = window.location.search;
            if (!StringHelper.isNullOrrUndefinedOrEmpty(paramString)) {
                paramString = paramString.substring(1, paramString.length);
                result = paramString.split("&");
            }
            return result;
        }
        /**
         * 获得URL参数
         * @returns URL参数
         */
        static getUrlParamsByObject(): Object {
            const result = new Object();
            const paramStrings = this.getUrlParamsByArray();
            if (!Common.isNullOrUndefined(paramStrings)) {
                for (let i = 0; i < paramStrings.length; i++) {
                    const params = paramStrings[i].split("=");
                    if (params.length === 2) {
                        result[params[0]] = params[1];
                    }
                    else if (params.length === 1) {
                        result[params[0]] = null;
                    }
                }
            }
            return result;
        }
        /**
         * 获得URL参数
         * @returns URL参数
         */
        static getUrlParamsByDictionary(): Dictionary {
            const result = new Dictionary();
            const paramStrings = this.getUrlParamsByArray();
            if (!Common.isNullOrUndefined(paramStrings)) {
                for (let i = 0; i < paramStrings.length; i++) {
                    const params = paramStrings[i].split("=");
                    if (params.length === 2) {
                        result.set(params[0], params[1]);
                    }
                    else if (params.length === 1) {
                        result.set(params[0], null);
                    }
                }
            }
            return result;
        }
        /**
         * 获得URL参数
         * @param key 键
         * @returns URL参数
         */
        static getUrlParam(key: string): string {
            const params = this.getUrlParamsByObject();
            if (params.hasOwnProperty(key)) {
                return params[key];
            } else {
                throw `未找到键为${key}的Url参数`;
            }
        }
    }
    /**
     * 文档帮助类
     */
    export class DocumentHelper {
        /**
         * 获取滚动条位置
         * @returns 滚动条位置
         */
        static getScrollTop(): number {
            let scrollTop = 0;
            if (document.documentElement && document.documentElement.scrollTop) {
                scrollTop = document.documentElement.scrollTop;
            }
            else if (document.body) {
                scrollTop = document.body.scrollTop;
            }
            return scrollTop;
        }
        /**
         * 获取可见高度
         * @returns 可见高度
         */
        static getClientHeight(): number {
            let clientHeight: number;
            if (document.body.clientHeight && document.documentElement.clientHeight) {
                clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            }
            else {
                clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
            }
            return clientHeight;
        }
    }
    /**
     * 元素帮助类
     */
    export class ElementHelper {
        /**
         * 设置样式
         * @param inputElement 目标元素
         * @param element 页面元素
         * @param className 要设置的样式列表
         */
        static setClass(inputElement: Element, className: string | string[]): void {
            let classString = Common.getType(className) === "Array" ? (className as string[]).join(" ") : className as string;
            if (!StringHelper.isNullOrrUndefinedOrEmpty(classString)) {
                classString = StringHelper.simplifyMultiSpaceToSingle(classString).trim();
                inputElement.setAttribute("class", classString);
            }
            else {
                inputElement.removeAttribute("class");
            }
        }
        /**
         * 添加样式
         * @param inputElement 目标元素
         * @param className 要添加的样式
         */
        static addClass(inputElement: Element, className: string | string[]): void {
            if (Common.getType(className) === "string") {
                className = (className as string).split(" ");
            }
            for (let i = 0; i < className.length; i++) {
                inputElement.classList.add(className[i]);
            }
        }
        /**
         * 删除样式
         * @param inputElement 目标元素
         * @param className 要删除的样式
         */
        static removeClass(inputElement: Element, className: string | string[]): void {
            if (Common.getType(className) === "string") {
                className = (className as string).split(" ");
            }
            for (let i = 0; i < className.length; i++) {
                inputElement.classList.remove(className[i]);
            }
        }
        /**
         * 是否有拥有样式
         * @param inputElement 目标元素
         * @param className 要查找的样式列表
         * @returns 查询结果
         */
        static hasClass(inputElement: Element, className: string | string[]): boolean {
            let resM = true;
            if (Common.getType(className) === "string") {
                className = (className as string).split(" ");
            }
            for (let i = 0; i < className.length && resM; i++) {
                resM = inputElement.classList.contains(className[i]);
                if (resM === false) break;
            }
            return resM;
        }
        /**
         * 根据ClassName获得元素对象
         * @param inputElement 目标元素
         * @param className ClassName
         * @returns Element集合
         */
        static getElementsByClassName(inputElement: Element, className: string): HTMLCollectionOf<Element> | Array<Element> {
            className = className.trim();
            if (inputElement.getElementsByClassName) {
                return inputElement.getElementsByClassName(className);
            } else {
                const elements = inputElement.getElementsByTagName("*");
                const result = new Array<Element>();
                for (let i = 0; i < elements.length; i++) {
                    if (ElementHelper.hasClass(elements[i], className)) {
                        result.push(elements[i]);
                    }
                }
                return result;
            }
        }
        /**
         * 根据Name获得元素对象
         * @param inputElement 目标元素
         * @param name Name
         * @returns Element集合
         */
        static getElementsByName(inputElement: Element, name: string): Array<Element> {
            name = name.trim();
            const result = new Array<Element>();
            const elements = inputElement.getElementsByTagName("*");
            for (let i = 0; i < elements.length; i++) {
                if ((elements[i] as any).name && (elements[i] as any).name === name) {
                    result.push(elements[i]);
                }
            }
            return result;
        }
        /**
         * 获得子节点
         * @param inputElement 目标元素
         * @returns 子节点
         */
        static getChildren(inputElement: Element): HTMLCollection | Array<Node> {
            if (inputElement.children) {
                return inputElement.children;
            }
            else {
                const result = new Array<Node>();
                const length = inputElement.childNodes.length;
                for (let i = 0; i < length; i++) {
                    if (inputElement.childNodes[i].nodeType === 1) {
                        result.push(inputElement.childNodes[i]);
                    }
                }
                return result;
            }
        }
        /**
         * 获得元素的实际样式
         * @param inputElement 目标元素
         * @returns 实际样式
         */
        static getComputedStyle(inputElement: Element): CSSStyleDeclaration {
            let cssStyle: CSSStyleDeclaration;
            if (inputElement["currentStyle"]) {
                cssStyle = inputElement["currentStyle"];
            }
            else {
                cssStyle = getComputedStyle(inputElement);
            }
            return cssStyle;
        }
        /**
         * 获得自定义属性
         * @param inputElement 目标元素
         * @returns 自定义属性
         */
        getDataSet(inputElement: HTMLElement): DOMStringMap | Object {
            if (inputElement.dataset) {
                return inputElement.dataset;
            }
            else {
                const result = new Object();
                const length = inputElement.attributes.length;
                let item: any;
                for (let i = 0; i < length; i++) {
                    item = inputElement.attributes[i];
                    if (item.specified && /^data-/.test(item.nodeName)) {
                        result[item.nodeName.substring(5)] = item.nodeValue;
                    }
                }
                return result;
            }
        }
    }
    /**
     * 事件帮助类
     */
    export class EventHelper {
        /**
         * 获得事件触发元素
         * @returns 触发元素 
         */
        static getEventTarget(inputEvent: Event): Element | EventTarget {
            return inputEvent["target"] || inputEvent["srcElement"];
        }
    }
    /**
     * Json帮助类
     */
    export class JsonHelper {
        /**
         * json转换为对象
         * @param jsonString json字符串
         * @returns 对象
         */
        static jsonStringToObject(jsonString: string): any {
            return JSON.parse ? JSON.parse(jsonString) : eval(`(${jsonString})`);
        }
        /**
         * 对象转换为Json
         * @param obj json对象
         * @returns json字符串
         */
        static objectToJsonString(obj: any): string {
            let result = "";
            let isArray: boolean;
            for (let key in obj) {
                if (obj.hasOwnProperty(key)) {
                    isArray = false;
                    const typeString = Common.getType(obj[key]);
                    if (obj instanceof Array) {
                        isArray = true;
                    }
                    if (typeString === "string") {
                        if (isArray) {
                            result += `"${obj[key].toString()}",`;
                        } else {
                            result += `"${key}":"${obj[key].toString()}",`;
                        }
                    } else if (obj[key] instanceof RegExp) {
                        if (isArray) {
                            result += obj[key].toString() + ",";
                        } else {
                            result += `"${key}":"${obj[key].toString()}",`;
                        }
                    } else if (obj[key] instanceof Array) {
                        result += `"${key}":${this.objectToJsonString(obj[key])},`;
                    } else if (typeString === "boolean") {
                        if (isArray) {
                            result += obj[key].toString() + ",";
                        } else {
                            result += `"${key}":${obj[key].toString()},`;
                        }
                    } else if (typeString === "number") {
                        if (isArray) {
                            result += obj[key].toString() + ",";
                        } else {
                            result += `"${key}":${obj[key].toString()},`;
                        }
                    } else if (obj[key] instanceof Object) {
                        if (isArray) {
                            result += this.objectToJsonString(obj[key]) + ",";
                        } else {
                            result += `"${key}":${this.objectToJsonString(obj[key])},`;
                        }
                    } else if (!obj[key] || obj[key] instanceof Function) {
                        if (isArray) {
                            result += "null,";
                        } else {
                            result += `"${key}":null,`;
                        }
                    }
                }
            }
            if (isArray) {
                result = `[${result.slice(0, -1)}]`;
            }
            else {
                result = `{${result.slice(0, -1)}}`;
            }
            return result;
        }
    }
    /**
     * 数学帮助类
     */
    export class MathHelper {
        /**
         * 返回一个随机数
         * @param min 最小值
         * @param max 最大值
         * @returns 随机数
         */
        static getRandom(min: number, max: number): number {
            return Math.floor(Math.random() * max + min);
        }
        /**
         * 获取四边形的外接圆半径
         * @param length 长
         * @param width 宽
         * @param isRound 是圆形
         * @returns 外接圆半径
         */
        static getCircumscribedCircleRadius(length: number, width: number, isRound: boolean): number {
            const max = Math.max(length, width);
            //正方形的对角线=边长^2*2
            const diameter = Math.sqrt(Math.pow(max, 2) * 2);
            //外接圆的直径=正方形的对角线
            //圆的半径=直径/2
            let radius = diameter / 2;
            if (isRound) {
                radius = Math.round(radius);
            }
            return radius;
        }
    }
    /**
     * HttpMethod枚举
     */
    export enum HttpMethod {
        GET = ("get") as any,
        POST = ("post") as any,
    }
    /**
     * Http配置类
     */
    export class HttpConfigModel {
        /**
         * URL地址
         */
        url: string;
        /**
         * 要发送的数据
         */
        data: any;
        /**
         * 成功方法
         */
        success: Function;
        /**
         * 失败方法
         */
        error: Function;
        /**
         * 成功错误都执行的方法
         */
        complete: Function;
        /**
         * HttpMethod类型
         */
        method: HttpMethod;
        /**
         * 超时时间
         */
        timeout: number = 15000;
        /**
         * 异步发送
         */
        async: boolean = true;
        /**
         * HTTP头
         */
        heads: any;
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
        constructor(url: string, method: HttpMethod = HttpMethod.POST, data: Object = null, heads: any = null, success: Function = null, error: Function = null, complete: Function = null) {
            if (heads == null) {
                heads = {
                    "Content-type": "application/json"
                }
            }
            this.url = url;
            this.method = method;
            this.data = data;
            this.heads = heads;
            this.success = success;
            this.error = error;
            this.complete = complete;
        }
    }
    /**
     * Http帮助类
     */
    export class HttpManager {
        /**
         * 获取XMLHttpRequest对象
         * @param config 配置对象
         * @returns HttpRequest对象
         */
        private static getHttpRequest(config: HttpConfigModel): XMLHttpRequest {
            let xhr: XMLHttpRequest;
            if (window["XMLHttpRequest"]) {
                xhr = new XMLHttpRequest();
            }
            else {
                xhr = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhr.onreadystatechange = () => {
                HttpManager.readyStateChange(xhr, config);
            }
            return xhr;
        }
        /**
         * 状态更改方法
         * @param xhr XMLHttpRequest对象
         * @param config 配置对象
         */
        private static readyStateChange(xhr: XMLHttpRequest, config: HttpConfigModel): void {
            if (xhr.readyState === 4) {
                let resM: any;
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
        }
        /**
         * 序列化参数
         * @param data 要序列化的参数
         * @returns 序列化后的字符串 
         */
        private static serialize(data: Object): string {
            const result = new Array<string>();
            for (let name in data) {
                if (data.hasOwnProperty(name)) {
                    if (typeof data[name] === "function") {
                        continue;
                    }
                    if (Common.getType(data[name]) === "Object") {
                        result.push(this.serialize(data[name]));
                    } else {
                        name = encodeURIComponent(name);
                        let value: string;
                        if (data[name]) {
                            value = data[name].toString();
                            value = encodeURIComponent(value);
                        } else {
                            value = "";
                        }
                        result.push(name + "=" + value);
                    }
                }
            };
            return result.join("&");
        }
        /**
         * 发送Post请求
         * @param config 配置对象
         */
        private static sendPost(config: HttpConfigModel): void {
            const xhr = this.getHttpRequest(config);
            xhr.open(String(config.method), config.url, config.async);
            for (let head in config.heads) {
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
        }
        /**
         * 发送Get请求
         * @param config 配置对象
         */
        private static sendGet(config: HttpConfigModel): void {
            const xhr = HttpManager.getHttpRequest(config);
            let url = config.url;
            if (config.data) {
                url += `?${HttpManager.serialize(config.data)}`;
            }
            xhr.open(String(config.method), url, config.async);
            for (let head in config.heads) {
                if (config.heads.hasOwnProperty(head)) {
                    xhr.setRequestHeader(head, config.heads[head]);
                }
            }
            xhr.send(null);
        }
        /**
         * 发送请求
         * @param config 配置对象
         */
        static send(config: HttpConfigModel): void {
            if (config.method === HttpMethod.POST) {
                HttpManager.sendPost(config);
            }
            else {
                HttpManager.sendGet(config);
            }
        }
    }
    /**
     * 字典
     */
    export class Dictionary {
        private data = new Object();
        private keys = new Array<string>();
        /**
         * 设置值
         * @param key 键
         * @param value 值
         */
        set(key: string, value: any): void {
            if (!this.data.hasOwnProperty(key)) {
                this.keys.push(key);
            }
            this.data[key] = value;
        }
        /**
         * 获得值
         * @param key 键
         * @returns 值
         */
        get(key: string): any {
            if (this.data.hasOwnProperty(key)) {
                return this.data[key];
            } else {
                return undefined;
            }
        }
        /**
         * 根据位序获得值
         * @param index 位序
         * @returns 值
         */
        getByIndex(index: number): any {
            if (index >= this.keys.length) throw "位序超过索引";
            return this.get(this.keys[index]);
        }
        /**
         * 移除值
         * @param key 键
         */
        remove(key: string): void {
            if (this.data.hasOwnProperty(key)) {
                const index = this.keys.indexOf(key);
                this.keys = ArrayHelper.removeAt(this.keys, index);
                delete this.data[key];
            }
        }
        /**
         * 获取所有的键
         */
        getAllKeys(): Array<string> {
            return this.keys;
        }
        /**
         * 根据位序获得键
         * @param index 位序
         * @returns 键
         */
        getKeyByIndex(index: number): string {
            if (index >= this.keys.length) throw "位序超过索引";
            return this.keys[index];
        }
        /**
         * 获得总数
         * @returns 总数
         */
        getCount(): number {
            return this.keys.length;
        }
        /**
         * 清空
         */
        clear(): void {
            this.keys = ArrayHelper.clear(this.keys);
            for (let key in this.data) {
                if (this.data.hasOwnProperty(key)) {
                    delete this.data[key];
                }
            }
        }
        /**
         * 是否拥有键
         * @param key
         */
        hasKey(key: string): boolean {
            return this.data.hasOwnProperty(key);
        }
    }
    /**
    * 本地存储帮助类
    */
    export class LocalDataManager {
        /**
         * 是否支持本地存储
         * @returns 是否支持
         */
        static isLocalStorage(): boolean {
            if (window.localStorage) {
                return true;
            }
            else {
                return false;
            }
        }
        /**
         * 清空本地存储对象
         */
        static cleanLocalData(): void {
            if (this.isLocalStorage()) {
                window.localStorage.clear();
            }
        }
        /**
         * 移除本地存储对象
         * @param key Key值
         */
        static removeLocalData(key: string): void {
            if (this.isLocalStorage() && key) {
                window.localStorage.removeItem(key);
            }
        }
        /**
         * 设置本地存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        static setLocalData(key: string, value: any, isJson: boolean = true): void {
            if (this.isLocalStorage() && key && value) {
                this.removeLocalData(key);
                if (isJson) {
                    window.localStorage.setItem(key, JSON.stringify(value));
                }
                else {
                    window.localStorage.setItem(key, value.toString());
                }
            }
        }
        /**
         * 获取本地存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据 
         */
        static getLocalData(key: string, isJson: boolean = true): any {
            if (this.isLocalStorage() && key) {
                if (isJson) {
                    return JsonHelper.jsonStringToObject(window.localStorage.getItem(key));
                }
                else {
                    return window.localStorage.getItem(key);
                }
            }
            return null;
        }
        /**
         * 是否支持网页存储
         * @returns 是否支持 
         */
        static isSessionStorage(): boolean {
            if (window.sessionStorage) {
                return true;
            }
            else {
                return false;
            }
        }
        /**
         * 清空网页存储对象
         */
        static cleanSessionData(): void {
            if (this.isSessionStorage()) {
                window.sessionStorage.clear();
            }
        }
        /**
         * 移除网页存储对象
         * @param key Key值
         */
        static removeSessionData(key: string) {
            if (this.isSessionStorage() && key) {
                window.sessionStorage.removeItem(key);
            }
        }
        /**
         * 设置网页存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        static setSessionData(key: string, value: any, isJson: boolean = true) {
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
        }
        /**
         * 获取网页存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据 
         */
        static getSessionData(key: string, isJson: boolean = true): any {
            if (this.isSessionStorage() && key) {
                if (isJson) {
                    return JSON.parse(window.sessionStorage.getItem(key));
                }
                else {
                    return window.sessionStorage.getItem(key);
                }
            }
            return null;
        }
        /**
         * 获得有效时间
         * @param timeValue 值
         * @param timeType 单位
         * @returns 计算后的时间
         */
        private static getTime(timeValue: number = 10000, timeType: TimeType = TimeType.Minutes): number {
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
        }
        /**
         * 设置一个Cookie
         * @param key Key值
         * @param value 要保存的值
         * @param time 持续时间
         * @param timeType 单位(默认s[秒])
         */
        static setCookie(key: string, value: any, isJson: boolean = true, timeValue: number = 60, timeType: TimeType = TimeType.Minutes, path: string = "/") {
            if (isJson) {
                document.cookie = key + "=" + JSON.stringify(value) + ";max-age=" + this.getTime(timeValue, timeType) + ";path=" + path;
            }
            else {
                document.cookie = key + "=" + value + ";max-age=" + this.getTime(timeValue, timeType) + ";path=" + path;
            }
        }
        /**
         * 删除一个Cookie
         * @param key Key值
         */
        static removeCookie(key: string) {
            document.cookie = key + "=;max-age=0";
        }
        /**
         * 获得所有Cookie
         * @returns Cookie对象 
         */
        static getAllCookie(): Object {
            const cookies: string[] = document.cookie.split(";");
            const cookie: Array<string[]> = new Array();
            const localCookie = new Object();
            for (let i = 0; i < cookies.length; i++) {
                if (!StringHelper.isNullOrrUndefinedOrEmpty(cookies[i])) {
                    cookie[i] = cookies[i].trim().split("=");
                    if (cookie[i][0] && cookie[i][1]) {
                        localCookie[cookie[i][0]] = cookie[i][1];
                    }
                }
            }
            return localCookie;
        }
        /**
         * 获得Cookie
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns
         */
        static getCookie(key: string, isJson: boolean = true): any {
            const resM = this.getAllCookie();
            if (isJson && resM && resM[key]) {
                return JSON.parse(resM[key]);
            }
            else {
                return null;
            }
        }

        /**
         * 设置数据
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         * @param time 时间
         * @param timeType 时间类型
         */
        static setData(key: string, value: any, isJson: boolean = true, time: number = 60, timeType: TimeType = TimeType.Minutes): void {
            if (this.isLocalStorage()) {
                this.setLocalData(key, value, isJson);
            }
            else {
                this.setCookie(key, value, isJson, time, timeType);
            }
        }
        /**
         * 获得数据
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns 获取到的数据
         */
        static getData(key: string, isJson: boolean = true): any {
            if (this.isLocalStorage()) {
                return this.getLocalData(key, isJson);
            }
            else {
                return this.getCookie(key, isJson);
            }
        }
        /**
         * 移除数据
         * @param key Key值
         */
        static removeData(key: string): void {
            if (this.isLocalStorage()) {
                this.removeLocalData(key);
            }
            else {
                this.removeCookie(key);
            }
        }
    }
    /**
     * 实现引擎模型
     */
    export class EngineInfoModel {
        //是否为Trident引擎
        trident = false;
        //是否为Gecko引擎
        gecko = false;
        //是否为WebKit引擎
        webKit = false;
        //是否为KHTML引擎
        kHtml = false;
        //是否为Presto引擎
        presto = false;
        //具体版本号
        version = "";
    }
    /**
     * 浏览器模型
     */
    export class BrowserInfoModel {
        //是否为IE浏览器
        internetExplorer = false;
        //是否为Firefox浏览器
        firefox = false;
        //是否为Safari浏览器
        safari = false;
        //是否为Konqueror浏览器
        konqueror = false;
        //是否为Opera浏览器
        opera = false;
        //是否为Chrome浏览器
        chrome = false;
        //是否为Edge浏览器
        edge = false;
        //是否为QQ浏览器
        qq = false;
        //是否为UC浏览器
        uc = false;
        //是否为Maxthon(遨游)浏览器
        maxthon = false;
        //是否为微信浏览器
        weChat = false;
        //具体版本号
        version = "";
    }
    /**
     * 系统模型
     */
    export class SystemInfoModel {
        //是否为Windows操作系统
        windows = false;
        //是否为WindowsMobile操作系统
        windowsMobile = false;
        //Windows版本
        windowsVersion = "";
        //是否为Mac操作系统
        mac = false;
        //是否为Unix操作系统
        unix = false;
        //是否为Linux操作系统
        linux = false;
        //是否为iPhone操作系统
        iPhone = false;
        //是否为iPod操作系统
        iPod = false;
        //是否为Windows操作系统
        iPad = false;
        //是否为Windows操作系统
        ios = false;
        //IOS版本
        iosVersion = "";
        //是否为Android操作系统
        android = false;
        //Android版本
        androidVersion = "";
        //是否为NokiaN操作系统
        nokiaN = false;
        //是否为Wii操作系统
        wii = false;
        //是否为PS操作系统
        ps = false;
    }
    /**
     * 客户端信息模型
     */
    export class ClientInfoModel {
        private engineInfoModel = new EngineInfoModel();
        private browserInfoModel = new BrowserInfoModel();
        private systemInfoModel = new SystemInfoModel();
        /**
         * 实现引擎信息
         */
        get engineInfo(): EngineInfoModel {
            return (Common.clone(this.engineInfoModel) as EngineInfoModel);
        }
        /**
         * 浏览器信息
         */
        get browserInfo(): BrowserInfoModel {
            return (Common.clone(this.browserInfoModel) as BrowserInfoModel);
        }
        /**
         * 系统信息
         */
        get systemInfo(): SystemInfoModel {
            return (Common.clone(this.systemInfoModel) as SystemInfoModel);
        }
        /**
         * 客户端信息模型
         */
        constructor() {
            //检测呈现引擎和浏览器
            let userAgent = navigator.userAgent;
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
                        let safariVersion = "";
                        let webKitVersion = parseInt(this.engineInfoModel.version);
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
    }
}