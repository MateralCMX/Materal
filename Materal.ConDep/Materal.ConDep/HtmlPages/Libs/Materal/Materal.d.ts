declare namespace Materal {
    enum TimeType {
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
    class Common {
        /**
         * 是否为undefined
         * @param obj 需要判断的对象
         * @returns 判断结果
         */
        static isUndefined(obj: any): boolean;
        /**
         * 是否为null或者undefined
         * @param obj 需要判断的对象
         * @returns 判断结果
         */
        static isNullOrUndefined(obj: any): boolean;
        /**
         * 鉴别类型
         * @param obj 传入对象
         * @param includeCustom 包括自定义类型
         * @returns 对象类型
         */
        static getType(obj: any, includeCustom?: boolean): string;
        /**
         * 克隆对象
         * @param obj 要克隆的对象
         */
        static clone(obj: any): any;
    }
    /**
     * 字符串帮助类
     */
    class StringHelper {
        /**
         * 是否为Empty字符串
         * @param inputStr 需要判断的对象
         * @returns 判断结果
         */
        static isEmpty(inputStr: string): boolean;
        /**
         * 是否为null或undefined或Empty字符串
         * @param inputStr 输入的字符串
         * 判定结果
         */
        static isNullOrrUndefinedOrEmpty(inputStr: string): boolean;
        /**
         * 移除左边空格
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        static trimLeft(inputStr: string): string;
        /**
         * 移除右边空格
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        static trimRight(inputStr: string): string;
        /**
         * 移除所有空格
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        static trimAll(inputStr: string): string;
        /**
         * 移除多余空格(连续的空格将变成一个)
         * @param inputStr 输入的字符串
         * @returns 处理过的字符串
         */
        static simplifyMultiSpaceToSingle(inputStr: string): string;
        /**
         * 左侧填充字符
         * @param inputStr 输入的字符串
         * @param length 位数
         * @param character 填充字符
         */
        static padLeft(inputStr: string, length: number, character: string): string;
        /**
         * 右侧填充字符
         * @param length 位数
         * @param character 填充字符
         */
        static padRight(inputStr: string, length: number, character: string): string;
        /**
         * 获取32位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        static get32Md5String(str: string, isLower?: boolean): string;
        /**
         * 获取16位MD5加密字符串
         * @param str 要加密的字符串
         * @param isLower 是小写
         * @returns 加密后的字符串
         */
        static get16Md5String(str: string, isLower?: boolean): string;
        /**
         * 转换为二进制字符串
         * @param str 要转换的字符串
         * @returns 转换后的字符串
         */
        static convertToBinary(str: string): string;
        /**
         * 隐藏代码
         * @param codeStr 要隐藏的代码
         * @returns 隐藏后的代码
         */
        static hideCode(codeStr: string): string;
        /**
         * 显示代码
         * @param codeStr 被隐藏的代码
         * @returns 显示的代码
         */
        static showCode(codeStr: string): string;
    }
    /**
     * 时间帮助类
     */
    class DateHelper {
        /**
         * 获得时间差
         * @param inputDateTime 输入时间
         * @param targetDate 对比时间
         * @param TimeType 返回类型
         * @param isFloor 向下取整
         * @returns 时间差
         */
        static getTimeDifference(inputDateTime: Date, targetDate: Date, timeType: TimeType, isFloor?: boolean): number;
        /**
         * 时间字符串格式化
         * @param inputDateTime 输入时间
         * @param formatStr 格式化字符串[y年|M月|d日|H时|m分|s秒|S毫秒|q季度]
         * @returns 格式化后的时间字符串
         */
        static formatString(inputDateTime: Date, formatStr: string): string;
        /**
         * 获取Input dateTime设置值字符串
         * @param inputDateTime 输入时间
         * @param dateTime 要设置的时间
         * @returns 可以设置给Input的时间值
         */
        static getInputDateTimeValueStr(inputDateTime: Date): string;
        /**
         * 获取对应时区时间
         * @param inputDateTime 输入时间
         * @param timeZone 时区
         * @returns 对应时区时间
         */
        static getDateByTimeZone(inputDateTime: Date, timeZone: number): Date;
        /**
         * 将其他时区时间转换为对应时区时间
         * @param inputDateTime 输入时间
         * @param timeZone 时区[null则自动为本地时区]
         * @returns 对应时区时间
         */
        static convertTimeZone(inputDateTime: Date, timeZone: number): Date;
    }
    /**
     * 数组帮助类
     */
    class ArrayHelper {
        /**
         * 插入数组
         * @param inputArray 原数组
         * @param item 要插入的对象
         * @param index 位序
         * @returns 插入后的数组
         */
        static insert<T>(inputArray: Array<T>, item: T, index: number): Array<T>;
        /**
         * 删除数组
         * @param inputArray 原数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        static remove<T>(inputArray: Array<T>, item: T): Array<T>;
        /**
         * 删除所有数组
         * @param inputArray 原数组
         * @param item 要删除的对象
         * @returns 删除后的数组
         */
        static removeAll<T>(inputArray: Array<T>, item: T): Array<T>;
        /**
         * 移除
         * @param inputArray 原数组
         * @param index 位序
         * @returns 移除后的数组
         */
        static removeAt<T>(inputArray: Array<T>, index: number): Array<T>;
        /**
         * 清空
         * @param inputArray 原数组
         * @returns 清空后的数组
         */
        static clear<T>(inputArray: Array<T>): Array<T>;
    }
    /**
     * 地址帮助类
     */
    class LocationHelper {
        /**
         * 获得参数
         * @returns URL参数
         */
        static getUrlParamsByArray(): string[];
        /**
         * 获得URL参数
         * @returns URL参数
         */
        static getUrlParamsByObject(): Object;
        /**
         * 获得URL参数
         * @returns URL参数
         */
        static getUrlParamsByDictionary(): Dictionary;
        /**
         * 获得URL参数
         * @param key 键
         * @returns URL参数
         */
        static getUrlParam(key: string): string;
    }
    /**
     * 文档帮助类
     */
    class DocumentHelper {
        /**
         * 获取滚动条位置
         * @returns 滚动条位置
         */
        static getScrollTop(): number;
        /**
         * 获取可见高度
         * @returns 可见高度
         */
        static getClientHeight(): number;
    }
    /**
     * 元素帮助类
     */
    class ElementHelper {
        /**
         * 设置样式
         * @param inputElement 目标元素
         * @param element 页面元素
         * @param className 要设置的样式列表
         */
        static setClass(inputElement: Element, className: string | string[]): void;
        /**
         * 添加样式
         * @param inputElement 目标元素
         * @param className 要添加的样式
         */
        static addClass(inputElement: Element, className: string | string[]): void;
        /**
         * 删除样式
         * @param inputElement 目标元素
         * @param className 要删除的样式
         */
        static removeClass(inputElement: Element, className: string | string[]): void;
        /**
         * 是否有拥有样式
         * @param inputElement 目标元素
         * @param className 要查找的样式列表
         * @returns 查询结果
         */
        static hasClass(inputElement: Element, className: string | string[]): boolean;
        /**
         * 根据ClassName获得元素对象
         * @param inputElement 目标元素
         * @param className ClassName
         * @returns Element集合
         */
        static getElementsByClassName(inputElement: Element, className: string): HTMLCollectionOf<Element> | Array<Element>;
        /**
         * 根据Name获得元素对象
         * @param inputElement 目标元素
         * @param name Name
         * @returns Element集合
         */
        static getElementsByName(inputElement: Element, name: string): Array<Element>;
        /**
         * 获得子节点
         * @param inputElement 目标元素
         * @returns 子节点
         */
        static getChildren(inputElement: Element): HTMLCollection | Array<Node>;
        /**
         * 获得元素的实际样式
         * @param inputElement 目标元素
         * @returns 实际样式
         */
        static getComputedStyle(inputElement: Element): CSSStyleDeclaration;
        /**
         * 获得自定义属性
         * @param inputElement 目标元素
         * @returns 自定义属性
         */
        getDataSet(inputElement: HTMLElement): DOMStringMap | Object;
    }
    /**
     * 事件帮助类
     */
    class EventHelper {
        /**
         * 获得事件触发元素
         * @returns 触发元素
         */
        static getEventTarget(inputEvent: Event): Element | EventTarget;
    }
    /**
     * Json帮助类
     */
    class JsonHelper {
        /**
         * json转换为对象
         * @param jsonString json字符串
         * @returns 对象
         */
        static jsonStringToObject(jsonString: string): any;
        /**
         * 对象转换为Json
         * @param obj json对象
         * @returns json字符串
         */
        static objectToJsonString(obj: any): string;
    }
    /**
     * 数学帮助类
     */
    class MathHelper {
        /**
         * 返回一个随机数
         * @param min 最小值
         * @param max 最大值
         * @returns 随机数
         */
        static getRandom(min: number, max: number): number;
        /**
         * 获取四边形的外接圆半径
         * @param length 长
         * @param width 宽
         * @param isRound 是圆形
         * @returns 外接圆半径
         */
        static getCircumscribedCircleRadius(length: number, width: number, isRound: boolean): number;
    }
    /**
     * HttpMethod枚举
     */
    enum HttpMethod {
        GET,
        POST
    }
    /**
     * Http配置类
     */
    class HttpConfigModel {
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
        timeout: number;
        /**
         * 异步发送
         */
        async: boolean;
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
        constructor(url: string, method?: HttpMethod, data?: Object, heads?: any, success?: Function, error?: Function, complete?: Function);
    }
    /**
     * Http帮助类
     */
    class HttpManager {
        /**
         * 获取XMLHttpRequest对象
         * @param config 配置对象
         * @returns HttpRequest对象
         */
        private static getHttpRequest;
        /**
         * 状态更改方法
         * @param xhr XMLHttpRequest对象
         * @param config 配置对象
         */
        private static readyStateChange;
        /**
         * 序列化参数
         * @param data 要序列化的参数
         * @returns 序列化后的字符串
         */
        private static serialize;
        /**
         * 发送Post请求
         * @param config 配置对象
         */
        private static sendPost;
        /**
         * 发送Get请求
         * @param config 配置对象
         */
        private static sendGet;
        /**
         * 发送请求
         * @param config 配置对象
         */
        static send(config: HttpConfigModel): void;
    }
    /**
     * 字典
     */
    class Dictionary {
        private data;
        private keys;
        /**
         * 设置值
         * @param key 键
         * @param value 值
         */
        set(key: string, value: any): void;
        /**
         * 获得值
         * @param key 键
         * @returns 值
         */
        get(key: string): any;
        /**
         * 根据位序获得值
         * @param index 位序
         * @returns 值
         */
        getByIndex(index: number): any;
        /**
         * 移除值
         * @param key 键
         */
        remove(key: string): void;
        /**
         * 获取所有的键
         */
        getAllKeys(): Array<string>;
        /**
         * 根据位序获得键
         * @param index 位序
         * @returns 键
         */
        getKeyByIndex(index: number): string;
        /**
         * 获得总数
         * @returns 总数
         */
        getCount(): number;
        /**
         * 清空
         */
        clear(): void;
        /**
         * 是否拥有键
         * @param key
         */
        hasKey(key: string): boolean;
    }
    /**
    * 本地存储帮助类
    */
    class LocalDataManager {
        /**
         * 是否支持本地存储
         * @returns 是否支持
         */
        static isLocalStorage(): boolean;
        /**
         * 清空本地存储对象
         */
        static cleanLocalData(): void;
        /**
         * 移除本地存储对象
         * @param key Key值
         */
        static removeLocalData(key: string): void;
        /**
         * 设置本地存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        static setLocalData(key: string, value: any, isJson?: boolean): void;
        /**
         * 获取本地存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据
         */
        static getLocalData(key: string, isJson?: boolean): any;
        /**
         * 是否支持网页存储
         * @returns 是否支持
         */
        static isSessionStorage(): boolean;
        /**
         * 清空网页存储对象
         */
        static cleanSessionData(): void;
        /**
         * 移除网页存储对象
         * @param key Key值
         */
        static removeSessionData(key: string): void;
        /**
         * 设置网页存储对象
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         */
        static setSessionData(key: string, value: any, isJson?: boolean): void;
        /**
         * 获取网页存储对象
         * @param key Key值
         * @param isJson 以Json格式获取
         * @returns 获取的数据
         */
        static getSessionData(key: string, isJson?: boolean): any;
        /**
         * 获得有效时间
         * @param timeValue 值
         * @param timeType 单位
         * @returns 计算后的时间
         */
        private static getTime;
        /**
         * 设置一个Cookie
         * @param key Key值
         * @param value 要保存的值
         * @param time 持续时间
         * @param timeType 单位(默认s[秒])
         */
        static setCookie(key: string, value: any, isJson?: boolean, timeValue?: number, timeType?: TimeType, path?: string): void;
        /**
         * 删除一个Cookie
         * @param key Key值
         */
        static removeCookie(key: string): void;
        /**
         * 获得所有Cookie
         * @returns Cookie对象
         */
        static getAllCookie(): Object;
        /**
         * 获得Cookie
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns
         */
        static getCookie(key: string, isJson?: boolean): any;
        /**
         * 设置数据
         * @param key Key值
         * @param value 要保存的数据
         * @param isJson 以Json格式保存
         * @param time 时间
         * @param timeType 时间类型
         */
        static setData(key: string, value: any, isJson?: boolean, time?: number, timeType?: TimeType): void;
        /**
         * 获得数据
         * @param key Key值
         * @param isJson 是否为Json格式
         * @returns 获取到的数据
         */
        static getData(key: string, isJson?: boolean): any;
        /**
         * 移除数据
         * @param key Key值
         */
        static removeData(key: string): void;
    }
    /**
     * 实现引擎模型
     */
    class EngineInfoModel {
        trident: boolean;
        gecko: boolean;
        webKit: boolean;
        kHtml: boolean;
        presto: boolean;
        version: string;
    }
    /**
     * 浏览器模型
     */
    class BrowserInfoModel {
        internetExplorer: boolean;
        firefox: boolean;
        safari: boolean;
        konqueror: boolean;
        opera: boolean;
        chrome: boolean;
        edge: boolean;
        qq: boolean;
        uc: boolean;
        maxthon: boolean;
        weChat: boolean;
        version: string;
    }
    /**
     * 系统模型
     */
    class SystemInfoModel {
        windows: boolean;
        windowsMobile: boolean;
        windowsVersion: string;
        mac: boolean;
        unix: boolean;
        linux: boolean;
        iPhone: boolean;
        iPod: boolean;
        iPad: boolean;
        ios: boolean;
        iosVersion: string;
        android: boolean;
        androidVersion: string;
        nokiaN: boolean;
        wii: boolean;
        ps: boolean;
    }
    /**
     * 客户端信息模型
     */
    class ClientInfoModel {
        private engineInfoModel;
        private browserInfoModel;
        private systemInfoModel;
        /**
         * 实现引擎信息
         */
        readonly engineInfo: EngineInfoModel;
        /**
         * 浏览器信息
         */
        readonly browserInfo: BrowserInfoModel;
        /**
         * 系统信息
         */
        readonly systemInfo: SystemInfoModel;
        /**
         * 客户端信息模型
         */
        constructor();
    }
}
