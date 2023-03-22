import { StepData } from "./Base/StepData";

/**
 * 开始节点数据
 */
export class StartStepData extends StepData {
    /**
     * 下一步
     */
    public Next?: StepData;
    constructor(id: string) {
        super(StartStepData.name, id);
    }
    public RemoveChild(childID: string): void {
        if (!this.Next || this.Next.ID !== childID) return;
        this.Next = undefined;
    }
}
