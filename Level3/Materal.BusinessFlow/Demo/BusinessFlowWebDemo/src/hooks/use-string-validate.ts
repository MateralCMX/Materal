import { RuleObject } from "ant-design-vue/es/form";

export enum StringValidateOptionsRuleItemType {
    string = 'string',
    number = 'number',
    date = 'date'
}

export interface StringValidateStringRule<T> {
    type: StringValidateOptionsRuleItemType.string;
    key: keyof T;
    message: string
}

export interface StringValidateNumberRule<T> {
    type: StringValidateOptionsRuleItemType.number;
    key: keyof T;
    requiredMessage: string,
    rangeMessage: string,
    min?: number;
    max?: number;
}

export interface StringValidateDateRule<T> {
    type: StringValidateOptionsRuleItemType.date;
    key: keyof T;
    message: string;
    minDate?: string;
    maxDate?: string;
}


export type StringValidateOptionsRuleItem<T> = StringValidateStringRule<T> | StringValidateNumberRule<T>;

export interface StringValidateOptions<T> {
    rules: StringValidateOptionsRuleItem<T>[];
}

const generateMap: Partial<Record<StringValidateOptionsRuleItemType, (data: any, item: any) => { validator: () => Promise<string | void> }>> = {
    [StringValidateOptionsRuleItemType.string]: generateStringValidator
}

export function useStringValida<T extends Record<string, any>>(data: Ref<T>, options: StringValidateOptions<T>) {
    const resultRule: Record<string, RuleObject> = {};
    options.rules.forEach((item) => {
        const func = generateMap[item.type]
        if (func) {
            Reflect.set(resultRule, item.key, func(data, item))
        }
    })
    return resultRule;
}

function generateStringValidator<T extends object>(data: Ref<T>, item: StringValidateStringRule<any>) {
    return {
        validator: () => {
            return new Promise<string | void>((resolve, reject) => {
                if (!Reflect.get(data.value, item.key)) {
                    reject(item.message);
                } else {
                    resolve();
                }
            })
        }
    }
}


// function generateNumberValidator<T extends object>(data: Ref<T>, item: StringValidateNumberRule<any>) {
//     return {
//         validator: () => {
//             return new Promise<string | void>((resolve, reject) => {
//                 const value = Reflect.get(data.value, item.key);
//                 if (value == null) {
//                     reject(item.rangeMessage);
//                 } else {
//                     if (item.min != null && value < item.min) {
//                         reject(item.rangeMessage);
//                     }       
//                 }
//             })
//         }
//     }
// }