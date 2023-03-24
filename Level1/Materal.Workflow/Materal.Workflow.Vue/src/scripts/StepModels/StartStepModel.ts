import { StartStepData } from "../StepDatas/StartStepData";
import { StepModel } from "./Base/StepModel";
import { Endpoint } from '@jsplumb/core';
import { IStepData } from "../StepDatas/Base/IStepData";

export class StartStepModel extends StepModel<StartStepData> {
    /**
     * 下一个节点
     */
    public NextStep?: StepModel<IStepData>;
    /**
     * 源端点
     */
    public SourcePoint: Endpoint<any>;
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${StartStepModel.name}`, id, instance, element, new StartStepData());
        this.SourcePoint = this.CreateSourceEndpoint();
    }
    public BindNext(next?: StepModel<IStepData>) {
        if (next) {
            this.NextStep = next;
            this.StepData.Next = next.StepData;
        }
        else {
            this.NextStep = undefined;
            this.StepData.Next = undefined;
        }
    }
    public Destroy(): void {
        throw new Error("开始节点不能销毁");
    }
}