import { DecisionConditionData } from "./Base/DecisionConditionData";
import { IStepData } from "./Base/IStepData";
import { StepData } from "./Base/StepData";

export class IfStepData extends StepData {
    /**
     * 决策条件数据
     */
    public Conditions: DecisionConditionData[] = []
    /**
     * 计划节点
     */
    public StepData?: IStepData;
    /**
     * 下一步
     */
    public Next?: IStepData;
    constructor(id: string) {
        super(`${IfStepData.name}`, "决策节点", id);
    }
}