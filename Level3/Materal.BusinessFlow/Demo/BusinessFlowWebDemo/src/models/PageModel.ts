import { PageRequestModel } from "./PageRequestModel";

export class PageModel extends PageRequestModel {
    /**
     * 页数
     */
    public PageCount: number = 0;
    /**
     * 数据总数
     */
    public DataCount: number = 0;
}
