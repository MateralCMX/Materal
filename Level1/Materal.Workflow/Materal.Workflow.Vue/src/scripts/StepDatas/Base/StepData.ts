import { RuntimeDataType } from "../../RuntimeDataType";
import { IStepData } from "./IStepData";

/**
 * 节点数据
 */
export abstract class StepData implements IStepData {
    public StepDataType: string;
    public Name: string;
    public Description?: string | undefined;
    public BuildData: { [key: string]: string | number; };
    // public get BuildDatas(): WorkflowDataType {
    //     let result = new WorkflowDataType();
    //     for (const key in this.BuildData) {
    //         if (!Object.prototype.hasOwnProperty.call(this.BuildData, key)) continue;
    //         const element : string | number = this.BuildData[key];
    //         result.Items.push({ Name: key, Type: element });
    //     }
    //     return result;
    // }
    // public set BuildDatas(value: WorkflowDataType) {
    //     this.BuildData = value;
    // }
    constructor(stepDataType: string, name: string) {
        this.StepDataType = stepDataType;
        this.Name = name;
        this.BuildData = {};
    }
}