import { BuildDataType } from "../../BuildDataType";
import { IStepData } from "./IStepData";

/**
 * 节点数据
 */
export abstract class StepData implements IStepData {
    public StepDataType: string;
    public Name: string;
    public Description?: string | undefined;
    private BuildData: { [key: string]: string | number; };
    public get BuildDatas(): BuildDataType {
        let result = new BuildDataType();
        result.InitByDictionary(this.BuildData);
        return result;
    }
    public set BuildDatas(value: BuildDataType) {
        this.BuildData = value.ToDictionary();
    }
    constructor(stepDataType: string, name: string) {
        this.StepDataType = stepDataType;
        this.Name = name;
        this.BuildData = {};
    }
}