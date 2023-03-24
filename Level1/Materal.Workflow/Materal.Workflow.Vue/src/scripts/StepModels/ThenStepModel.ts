import { StepModel } from "./Base/StepModel";
import { Endpoint } from '@jsplumb/core';
import { ThenStepData } from "../StepDatas/ThenStepData";

export class ThenStepModel extends StepModel<ThenStepData> {
    /**
     * 源端点
     */
    public TargetPoint: Endpoint<any>;
    /**
     * 源端点
     */
    public SourcePoint: Endpoint<any>;
    constructor(id: string, instance: any, element: HTMLElement) {
        super(id, instance, element, new ThenStepData());
        this.TargetPoint = this.CreateTrgetEndpoint();
        this.SourcePoint = this.CreateSourceEndpoint();
    }
    public Destroy(): void {
        if (this.UpStep &&
            Object.prototype.hasOwnProperty.call(this.UpStep.StepData, "Next")) {
            (this.UpStep.StepData as any).Next = undefined;
        }
        this.Instance.deleteEndpoint(this.SourcePoint);
        this.Instance.deleteEndpoint(this.TargetPoint);
    }
}