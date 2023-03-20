import { StepData } from "./StepData";

/**
 * 结束节点数据
 */
export class EndStepData extends StepData {
    /**
     * 下一步
     */
    public Next: StepData | null;
    constructor() {
        super(EndStepData.name);
    }
}
