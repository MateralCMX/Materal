export abstract class StepData {
    /**
     * 节点数据类型
     */
    private StepDataType: string;
    get StepDataTypeName(): string {
        return this.StepDataType;
    }
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
    constructor(stepDataType: string) {
        this.StepDataType = stepDataType;
    }
}
