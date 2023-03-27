import { BrowserJsPlumbInstance } from '@jsplumb/browser-ui';
import { Connection } from "@jsplumb/core";
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
        this.Instance.manage(element, id);
    }
    /**
     * 处理连接
     * @param type 
     * @param target 
     */
    public abstract HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean;
    /**
     * 处理解除连接
     * @param type 
     * @param target 
     */
    public abstract HandlerDisconnection(connection: Connection, target: StepModel<IStepData>): boolean;
    /**
     * 销毁
     */
    public abstract Destroy(): void;
}