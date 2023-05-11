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
    /**
     * 获得枚举数据
     */
    public GetEnumData(): string[] {
        if (!this.Data) return [];
        else return JSON.parse(this.Data) as string[];
    }
    /**
     * 设置枚举数据
     * @param datas 
     */
    public SetEnumData(datas: string[]) {
        for (let i = 0; i < datas.length; i++) {
            if (!datas[i]) {
                datas.splice(i, 1);
            }
        }
        this.Data = JSON.stringify(datas);
    }
}
