import { StepData } from "./StepData";
/**
 * 普通节点数据
 */
export class ThenStepData extends StepData {
    /**
     * 节点类型
     */
    public StepType: string;
    /**
     * 下一步
     */
    public Next: StepData | null;
    constructor() {
        super(ThenStepData.name);
    }
}
