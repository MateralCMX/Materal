/**
 * 分页请求模型
 */
export abstract class PageRequestModel {
    /**
     * 页码
     */
    public PageIndex: number = 1;
    /**
     * 每页数量
     */
    public PageSize: number = 10;
}
