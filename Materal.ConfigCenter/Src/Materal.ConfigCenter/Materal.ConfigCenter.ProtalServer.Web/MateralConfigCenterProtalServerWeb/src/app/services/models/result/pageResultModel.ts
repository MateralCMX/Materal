import { ResultModel } from './resultModel';
import { ResultTypeEnum } from './resultTypeEnum';
import { PageModel } from './pageModel';

/**
 * 分页返回模型
 */
export class PageResultModel<T> implements ResultModel {
    public Message: string;
    public ResultType: ResultTypeEnum;
    public Data: T[];
    public PageModel: PageModel;
}
