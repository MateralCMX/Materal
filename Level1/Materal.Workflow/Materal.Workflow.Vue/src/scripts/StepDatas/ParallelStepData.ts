import { IStepData } from "./Base/IStepData";
import { StepData } from "./Base/StepData";

export class ParallelStepData extends StepData {
    /**
     * 计划节点
     */
    public StepDatas: IStepData[] = [];
    /**
     * 下一步
     */
    public Next?: IStepData;
    constructor(id: string) {
        super(`${ParallelStepData.name}`, "并行节点", id);
    }
}