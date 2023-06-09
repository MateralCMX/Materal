import { KeyValueModel } from "../KeyValueModel";

export class HttpAutoNodeDataModel {
    /**
     * 请求地址
     */
    public Url: string = "";
    /**
     * 请求方式
     */
    public Method: string = "";
    /**
     * 请求头
     */
    public Headers: KeyValueModel[] = [];
    /**
     * 请求头
     */
    public QueryParams: KeyValueModel[] = [];
    /**
     * 请求体
     */
    public Body: string = "";
    /**
     * 请求头
     */
    public ResultMappers: HttpAutoNodeMapperModel[] = [];
}
export class HttpAutoNodeMapperModel {
    /**
     * 返回名称
     */
    public ResultName: string = "";
    /**
     * 数据模型字段名称
     */
    public DataModelFieldName: string = "";
}