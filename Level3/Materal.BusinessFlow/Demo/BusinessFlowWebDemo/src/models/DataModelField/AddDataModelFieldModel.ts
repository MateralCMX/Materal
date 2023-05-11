import { DataTypeEnum } from "./DataTypeEnum";

export class AddDataModelFieldModel {
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
     * 数据
     */
    public Data?: string;
    /**
     * 描述
     */
    public Description?: string;
}
