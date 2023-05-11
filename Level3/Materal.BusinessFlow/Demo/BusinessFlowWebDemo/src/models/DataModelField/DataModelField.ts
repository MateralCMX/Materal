import { BaseDomain } from "../BaseDomain";
import { DataTypeEnum } from "./DataTypeEnum";

export class DataModelField extends BaseDomain {
    /**
     * 名称
     */
    public Name: string = "";
    /**
     * 数据模型ID
     */
    public DataModelID: string = "";
    /**
     * 数据类型
     */
    public DataType: DataTypeEnum = DataTypeEnum.String;
    /**
     * 数据类型
     */
    public DataTypeText: String = "";
    /**
     * 数据
     */
    public Data?: string;
    /**
     * 描述
     */
    public Description?: string;
}