import { IStepData } from "./Base/IStepData";
import { StepData } from "./Base/StepData";

export class BranchStepData extends StepData {
    /**
     * 计划节点
     */
    public StepDatas: IStepData[] = [];
    /**
     * 下一步
     */
    public Next?: IStepData;
    constructor(id: string) {
        super(`${BranchStepData.name}`, "分支节点", id);
    }
}