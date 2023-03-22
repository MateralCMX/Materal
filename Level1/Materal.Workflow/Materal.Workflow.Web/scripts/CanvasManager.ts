import { BrowserJsPlumbInstance, newInstance, ContainmentType } from "@jsplumb/browser-ui";
import { DotEndpoint, RectangleEndpoint, EVENT_CONNECTION, EVENT_CONNECTION_DETACHED } from "@jsplumb/core";
import { StepDataInfo, AllStepDataInfos } from "./StepDataInfo";
import "../css/Steps.css";
import { StepData } from "./StepDatas/Base/StepData";
import { RuntimeDataType } from "./RuntimeDataType";

/**
 * 画布管理器
 */
export class CanvasManager {
    private instance: BrowserJsPlumbInstance;
    private canvasElement: HTMLElement;
    private selectedStep: (stepData: StepData, element: HTMLElement, stepInfo: StepDataInfo) => void;
    private maxStepID: number = 0;//当前最大的节点ID
    private stepDatas: { [key: string]: StepData } = {};//所有界面上的节点数据
    private targetPoints: { [key: string]: any } = {};//所有界面上的目标锚点
    private sourcePoints: { [key: string]: any } = {};//界面上的源锚点
    public runTimeDataTypes: { [key: string]: string } = {};//运行时数据类型
    constructor(targetElement: HTMLElement, selectedStep: (stepData: StepData, element: HTMLElement, stepInfo: StepDataInfo) => void) {
        this.canvasElement = targetElement;
        this.selectedStep = selectedStep;
        this.instance = newInstance({
            container: targetElement,
            dragOptions: {
                containment: ContainmentType.parentEnclosed,
                grid: {
                    w: 10,
                    h: 10
                }
            }
        });
        this.instance.bind(EVENT_CONNECTION, (params) => this.BindNext(params.sourceId, params.targetId));
        this.instance.bind(EVENT_CONNECTION_DETACHED, (params) => this.BindNext(params.sourceId, null));
        this.CreateStepElement(AllStepDataInfos.StartStep);
    }
    /**
     * 创建节点元素
     * @param node 
     * @param stepInfo 
     */
    public CreateStepElement(stepInfo: StepDataInfo) {
        //创建元素
        let node: HTMLElement = document.createElement("div");
        node.id = `step${this.maxStepID++}`;
        node.classList.add("Step");
        node.classList.add(stepInfo.ID);
        node.innerText = stepInfo.Name;
        node.addEventListener("click", e => {
            if (this.selectedStep == null) return;
            console.log(this.stepDatas[node.id]);
            this.selectedStep(this.stepDatas[node.id], e.target as HTMLElement, stepInfo);
        });
        this.canvasElement.appendChild(node);
        //创建锚点
        if (stepInfo.TargetAnchor) {
            this.CreateTargetAnchor(node);
        }
        if (stepInfo.SourceAnchor) {
            this.CreateSourceAnchor(node);
        }
        //绑定数据
        this.stepDatas[node.id] = stepInfo.InitStepDataAction(node.id);
    }
    /**
     * 创建目标锚点
     * @param node 
     */
    private CreateTargetAnchor(node: HTMLElement) {
        const point = this.instance.addEndpoint(node, {
            target: true,
            anchor: "AutoDefault",
            endpoint: RectangleEndpoint.type
        });
        this.targetPoints[node.id] = point;
    }
    /**
     * 创建源锚点
     * @param node 
     */
    private CreateSourceAnchor(node: HTMLElement) {
        const point = this.instance.addEndpoint(node, {
            source: true,
            anchor: "AutoDefault",
            endpoint: DotEndpoint.type,
            connectorOverlays: [
                { type: "Arrow", options: { location: [0.5, 0.5] } }
            ],
            // connector: FlowchartConnector.type//流程图线
        });
        this.sourcePoints[node.id] = point;
    }
    /**
     * 绑定下一步
     * @param sourceID 
     * @param targetID 
     */
    private BindNext(sourceID: string, targetID: string | null) {
        if (targetID == null) {
            this.stepDatas[sourceID]["Next"] = undefined;
        }
        else {
            if (Object.prototype.hasOwnProperty.call(this.stepDatas, sourceID) &&
                Object.prototype.hasOwnProperty.call(this.stepDatas, targetID) &&
                Object.prototype.hasOwnProperty.call(this.stepDatas[sourceID], "Next")) {
                this.stepDatas[sourceID]["Next"] = this.stepDatas[targetID];
            }
        }
    }
    /**
     * 删除节点元素
     * @param node 
     */
    public DeleteStepElement(id: string) {
        const node = document.getElementById(id);
        if (!node) return;
        if (!Object.prototype.hasOwnProperty.call(this.stepDatas, node.id)) return;
        if (Object.prototype.hasOwnProperty.call(this.targetPoints, node.id)) {
            this.instance.deleteEndpoint(this.targetPoints[node.id]);
        }
        if (Object.prototype.hasOwnProperty.call(this.sourcePoints, node.id)) {
            this.instance.deleteEndpoint(this.sourcePoints[node.id]);
        }
        delete this.stepDatas[node.id];
        for (const key in this.stepDatas) {
            if (!Object.prototype.hasOwnProperty.call(this.stepDatas, key)) continue;
            const stepData = this.stepDatas[key];
            stepData.RemoveChild(node.id);
        }
        node.parentElement?.removeChild(node);
    }
}
