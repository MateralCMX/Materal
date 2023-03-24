import { StartStepData } from "../StepDatas/StartStepData";
import { StepModel } from "./Base/StepModel";
import { Endpoint } from '@jsplumb/core';

export class StartStepModel extends StepModel<StartStepData> {
    /**
     * 源端点
     */
    public SourcePoint: Endpoint<any>;
    constructor(id: string, instance: any, element: HTMLElement) {
        super(id, instance, element, new StartStepData());
        this.SourcePoint = this.CreateSourceEndpoint();
    }
    public Destroy(): void {
        throw new Error("开始节点不能销毁");
    }
}