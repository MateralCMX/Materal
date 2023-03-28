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
    protected OtherConnector: Connection[] = [];
    constructor(stepModelTypeName: string, id: string, instance: BrowserJsPlumbInstance, element: HTMLElement, stepData: T) {
        this.StepModelTypeName = stepModelTypeName;
        this.ID = id;
        this.Instance = instance;
        this.StepElement = element;
        this.StepData = stepData;
        this.Instance.manage(element, id);
    }
    /**
     * 添加其他连接
     * @param connection 
     */
    public AddOtherConnector(connection: Connection): void {
        for (let i = 0; i < this.OtherConnector.length; i++) {
            const element = this.OtherConnector[i];
            if (element.id === connection.id) return;
        }
        this.OtherConnector.push(connection);
    }
    /**
     * 移除其他连接
     * @param connection 
     */
    public RemoveOtherConnector(connection: Connection): void {
        for (let i = 0; i < this.OtherConnector.length; i++) {
            const connector = this.OtherConnector[i];
            if (connector.id === connection.id) {
                this.OtherConnector = this.OtherConnector.slice(i, 1);
                return;
            }
        }
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
    public abstract HandlerDisconnection(connection: Connection, target: StepModel<IStepData>): void;
    /**
     * 销毁
     */
    public abstract Destroy(): void;
    /**
     * 销毁其他连接
     */
    protected DestroyOtherConnector(): void {
        while (this.OtherConnector.length > 0) {
            const connector = this.OtherConnector[0];
            this.OtherConnector = this.OtherConnector.slice(1, 1);
            this.Instance.deleteConnection(connector);
        }
    }
}