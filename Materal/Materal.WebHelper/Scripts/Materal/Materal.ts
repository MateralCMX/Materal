namespace Materal
{
    export class Common
    {
        /**
         * 是否为undefined
         * @param obj 需要判断的对象
         * @returns 判断结果
         */
        static isUndefined(obj: any): boolean
        {
            return typeof obj === "undefined";
        }
        /**
         * 是否为null或者undefined
         * @param obj 需要判断的对象
         * @returns 判断结果
         */
        static isNullOrUndefined(obj: any): boolean
        {
            return obj === null || this.isUndefined(obj);
        }
        /**
         * 是否为Empty字符串
         * @param str 需要判断的对象
         * @returns 判断结果
         */
        static isEmpty(str: string): boolean {
            return str === "";
        }
        /**
         *  是否为null或undefined或Empty字符串
         * @param str
         */
        static isNullOrrUndefinedOrEmpty(str: string): boolean {
            return this.isNullOrUndefined(str) || this.isEmpty(str);
        }
        /**
         * 鉴别类型
         * @param obj 传入对象
         * @param includeCustom 包括自定义类型
         * @returns 对象类型 
         */
        static getType(obj: any, includeCustom: boolean = true): string
        {
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
            if (!this.isNullOrrUndefinedOrEmpty(result) && lowercase) {
                result = result.toLowerCase();
            }
            return result;
        }
    }
    /**
     * 数组帮助类
     */
    export class ArrayHelper
    {
        /**
         * 移除
         * @param array 原数组
         * @param index 位序
         * @returns 移除后的数组
         */
        static removeAt<T>(array: Array<T>, index: number): Array<T>
        {
            array = array.splice(index, 1);
            return array;
        }
        /**
         * 清空
         * @param array 原数组
         * @returns 清空后的数组
         */
        static clear<T>(array: Array<T>): Array<T>
        {
            array = array.splice(0, array.length);
            return array;
        }
    }
    /**
     * 字典
     */
    export class Dictionary
    {
        private data = new Object();
        private keys = new Array<string>();
        /**
         * 设置值
         * @param key 键
         * @param value 值
         */
        set(key: string, value: any): void
        {
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
        get(key: string): any
        {
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
        getByIndex(index: number): any
        {
            if (index >= this.keys.length) throw "位序超过索引";
            return this.get(this.keys[index]);
        }
        /**
         * 移除值
         * @param key 键
         */
        remove(key: string): void
        {
            if (this.data.hasOwnProperty(key)) {
                const index = this.keys.indexOf(key);
                this.keys = ArrayHelper.removeAt(this.keys, index);
                delete this.data[key];
            }
        }
        /**
         * 获取所有的键
         */
        getAllKeys(): Array<string>
        {
            return this.keys;
        }
        /**
         * 根据位序获得键
         * @param index 位序
         * @returns 键
         */
        getKeyByIndex(index: number): string
        {
            if (index >= this.keys.length) throw "位序超过索引";
            return this.keys[index];
        }
        /**
         * 获得总数
         * @returns 总数
         */
        getCount(): number
        {
            return this.keys.length;
        }
        /**
         * 清空
         */
        clear(): void
        {
            this.keys = ArrayHelper.clear(this.keys);
            for (let key in this.data) {
                if (this.data.hasOwnProperty(key)) {
                    delete this.data[key];
                }
            }
        }
    }
}