import { PageRequestModel } from './pageRequestModel';

/**
 * 分页模型
 */
export class PageModel implements PageRequestModel {
    public PageIndex: number;
    public PageSize: number;
    public PageCount: number;
    public DataCount: number;
}
