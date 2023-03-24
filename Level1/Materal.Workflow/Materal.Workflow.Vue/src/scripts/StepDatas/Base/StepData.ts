import { IStepData } from "./IStepData";

/**
 * 节点数据
 */
export abstract class StepData implements IStepData {
    public StepDataType: string;
    public Name: string;
    public Description?: string | undefined;
    public BuildData: { [key: string]: string | number };
    constructor(stepDataType: string, name: string) {
        this.StepDataType = stepDataType;
        this.Name = name;
        this.BuildData = {};
    }
}