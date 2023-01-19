import { ResultModel } from './resultModel';
import { ResultTypeEnum } from './resultTypeEnum';

/**
 * 返回对象
 */
export class ResultDataModel<T> implements ResultModel {
    public Message: string;
    public ResultType: ResultTypeEnum;
    public Data: T;
}
