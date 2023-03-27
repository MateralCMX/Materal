import { BrowserJsPlumbInstance } from '@jsplumb/browser-ui';
import { Endpoint, DotEndpoint, RectangleEndpoint } from '@jsplumb/core';
import { IStepData } from '../../StepDatas/Base/IStepData';

export abstract class StepModel<T extends IStepData> {
    /**
     * 节点ID
     */
    public ID: string;
    /**
     * 节点类型名称
     */
    public StepModelTypeName: string;
    /**
     * 绘图对象
     */
    public Instance: BrowserJsPlumbInstance;
    /**
     * 节点元素
     */
    public StepElement: HTMLElement;
    /**
     * 节点数据
     */
    public StepData: T;
    constructor(stepModelTypeName: string, id: string, instance: BrowserJsPlumbInstance, element: HTMLElement, stepData: T) {
        this.StepModelTypeName = stepModelTypeName;
        this.ID = id;
        this.Instance = instance;
        this.StepElement = element;
        this.StepData = stepData;
    }
    /**
     * 绘制源端点
     */
    protected CreateSourceEndpoint(): Endpoint<any> {
        return this.Instance.addEndpoint(this.StepElement, {
            source: true,
            target: false,
            anchor: "AutoDefault",
            endpoint: DotEndpoint.type,
            connectorOverlays: [
                { type: "Label", options: { label:"下一步", id:"NextLabel", location:50 } },
                { type: "Arrow", options: { location: [0.5, 0.5] } }
            ],
            // connector: FlowchartConnector.type//流程图线
        });
    }
    /**
     * 绘制目标端点
     */
    protected CreateTrgetEndpoint(): Endpoint<any> {
        return this.Instance.addEndpoint(this.StepElement, {
            source: false,
            target: true,
            anchor: "AutoDefault",
            endpoint: RectangleEndpoint.type
            // connector: FlowchartConnector.type//流程图线
        });
    }
    /**
     * 销毁
     */
    public abstract Destroy(): void;
}