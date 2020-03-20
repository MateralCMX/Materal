import { ResultTypeEnum } from './resultTypeEnum';

/**
 * 返回对象
 */
export interface ResultModel {
    Message: string;
    ResultType: ResultTypeEnum;
}
