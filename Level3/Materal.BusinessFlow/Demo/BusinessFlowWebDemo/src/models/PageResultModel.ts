import { PageModel } from "./PageModel";
import { ResultDataModel } from "./ResultDataModel";

export class PageResultModel<T> extends ResultDataModel<T[]> {
    /**
     * 总数
     */
    public TotalCount: number = 0;
    public PageModel: PageModel = new PageModel();
}
