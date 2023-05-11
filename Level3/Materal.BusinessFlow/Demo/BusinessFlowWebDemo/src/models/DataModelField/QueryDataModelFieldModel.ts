import { PageRequestModel } from "../PageRequestModel";
import { DataTypeEnum } from "./DataTypeEnum";

export class QueryDataModelFieldModel extends PageRequestModel {
    /**
     * 唯一标识
     */
    public ID?: string;
    /**
     * 名称
     */
    public Name?: string;
    /**
     * 数据模型ID
     */
    public DataModelID?: string;
    /**
     * 数据类型
     */
    public DataType?: DataTypeEnum;
    /**
     * 描述
     */
    public Description?: string;
}
