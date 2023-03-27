import { StepModel } from "./Base/StepModel";
import { Endpoint, DotEndpoint } from '@jsplumb/core';
import { ThenStepData } from "../StepDatas/ThenStepData";
import { IStepData } from "../StepDatas/Base/IStepData";

export class ThenStepModel extends StepModel<ThenStepData> {
    /**
     * 上一个节点
     */
    public UpStep?: StepModel<IStepData>;
    /**
     * 下一个节点
     */
    public NextStep?: StepModel<IStepData>;
    /**
     * 目标端点
     */
    public TargetPoint: Endpoint<any>;
    /**
     * 源端点
     */
    public SourcePoint: Endpoint<any>;
    /**
     * 补偿端点
     */
    public CompensatePoint: Endpoint<any>;
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${ThenStepModel.name}`, id, instance, element, new ThenStepData());
        this.TargetPoint = this.CreateTrgetEndpoint();
        this.SourcePoint = this.CreateSourceEndpoint();
        this.CompensatePoint = this.CreateCompensatePoint();
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
    public BindUp(up?: StepModel<IStepData>) {
        if (up) {
            this.UpStep = up;
        }
        else {
            this.UpStep = undefined;
        }
    }
    public Destroy(): void {
        this.Instance.deleteEndpoint(this.SourcePoint);//从画布上移除源端点
        this.Instance.deleteEndpoint(this.TargetPoint);//从画布上移除目标端点
    }
    /**
     * 绘制源端点
     */
        protected CreateCompensatePoint(): Endpoint<any> {
            return this.Instance.addEndpoint(this.StepElement, {
                source: true,
                target: false,
                anchor: "AutoDefault",
                endpoint: DotEndpoint.type,
                cssClass : "CompensatePoint",
                connectorClass: "CompensateConnector",
                connectorOverlays: [
                    { type: "Label", options: { label:"异常处理", id:"CompensateLabel", location:50 } },
                    { type: "Arrow", options: { location: [0.5, 0.5] } },
                ],
                // connector: FlowchartConnector.type//流程图线
            });
        }
}