declare namespace Materal {
    class Common {
        static isUndefined(obj: any): boolean;
        static isNullOrUndefined(obj: any): boolean;
        static isEmpty(str: string): boolean;
        static isNullOrrUndefinedOrEmpty(str: string): boolean;
        static getType(obj: any, includeCustom?: boolean): string;
    }
    class ArrayHelper {
        static removeAt(array: Array<any>, index: number): Array<any>;
    }
    class Dictionary {
        private data;
        private count;
        private keys;
        set(key: string, value: any): void;
        get(key: string): any;
        remove(key: string): void;
        getAllKeys(): Array<string>;
        getCount(): number;
    }
}
