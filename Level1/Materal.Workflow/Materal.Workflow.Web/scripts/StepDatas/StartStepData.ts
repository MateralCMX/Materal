import { StepData } from "./Base/StepData";

/**
 * 开始节点数据
 */
export class StartStepData extends StepData {
    /**
     * 下一步
     */
    public Next: StepData | null;
    constructor() {
        super(StartStepData.name);
    }
}
