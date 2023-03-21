/**
 * 输出数据
 */
export class OutputData {
    /**
     * 运行数据属性
     */
    public RuntimeDataProperty: string;
    /**
     * 值属性名称
     */
    public StepProperty: string;
    constructor(josnValue?: string) {
        if (!josnValue) return;
        let model: OutputData = JSON.parse(josnValue);
        this.RuntimeDataProperty = model.RuntimeDataProperty;
        this.StepProperty = model.StepProperty;
    }
}
