export abstract class StepData {
    /**
     * 节点数据类型
     */
    private StepDataType: string;
    get StepDataTypeName(): string {
        return this.StepDataType;
    }
    /**
     * 唯一标识
     */
    public ID: string;
    /**
     * 名称
     */
    public Name: string;
    /**
     * 描述
     */
    public Description: string | null;
    /**
     * 构建数据
     */
    public BuildData: any = {};
    constructor(stepDataType: string, id: string) {
        this.StepDataType = stepDataType;
        this.ID = id;
    }
    public abstract RemoveChild(childID: string): void;
}
