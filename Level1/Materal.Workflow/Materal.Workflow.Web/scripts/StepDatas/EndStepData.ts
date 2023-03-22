import { StepData } from "./Base/StepData";

/**
 * 结束节点数据
 */
export class EndStepData extends StepData {
    constructor() {
        super(EndStepData.name);
    }
    public RemoveChild(childID: string): void {
    }
}
