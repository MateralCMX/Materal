import { InputValueSourceEnum } from "./InputValueSourceEnum";

/**
 * 输入数据
 */
export class InputData {
    /**
     * 节点属性名称
     */
    public StepProperty: string;
    /**
     * 值
     */
    public Value: any;
    /**
     * 值来源
     */
    public ValueSource: InputValueSourceEnum;
    constructor(josnValue?: string) {
        if (!josnValue) return;
        let model: InputData = JSON.parse(josnValue);
        this.StepProperty = model.StepProperty;
        this.Value = model.Value;
    }
}
