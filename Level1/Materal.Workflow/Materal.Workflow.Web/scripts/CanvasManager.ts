import { BrowserJsPlumbInstance, newInstance, ContainmentType } from "@jsplumb/browser-ui";
import { DotEndpoint, RectangleEndpoint, EVENT_CONNECTION, EVENT_CONNECTION_DETACHED } from "@jsplumb/core";
import { StepInfo, AllStepInfos } from "./StepInfo";
import "../css/Steps.css";
import { StepData } from "./StepData";

/**
 * 画布管理器
 */
export class CanvasManager {
    private instance: BrowserJsPlumbInstance;
    private canvasElement: HTMLElement;
    private selectedStep: (stepData: StepData, element: HTMLElement,stepInfo: StepInfo) => void;
    private maxStepID: number = 0;
    private stepDatas: any = {};
    constructor(targetElement: HTMLElement, selectedStep: (stepData: StepData, element: HTMLElement, stepInfo: StepInfo) => void) {
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
        this.CreateStartStep();
    }
    /**
     * 创建节点元素
     * @param node 
     * @param stepInfo 
     */
    public CreateStepElement(stepInfo: StepInfo) {
        //创建元素
        let node: HTMLElement = document.createElement("div");
        node.id = `step${this.maxStepID++}`;
        node.classList.add("Step");
        node.classList.add(stepInfo.ID);
        node.innerText = stepInfo.Name;
        node.addEventListener("click", e => {
            if (this.selectedStep == null) return;
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
        this.stepDatas[node.id] = stepInfo.InitStepDataAction();
    }
    /**
     * 创建目标锚点
     * @param node 
     */
    private CreateTargetAnchor(node: HTMLElement) {
        this.instance.addEndpoint(node, {
            target: true,
            anchor: "AutoDefault",
            endpoint: RectangleEndpoint.type
        });
    }
    /**
     * 创建源锚点
     * @param node 
     */
    private CreateSourceAnchor(node: HTMLElement) {
        this.instance.addEndpoint(node, {
            source: true,
            anchor: "AutoDefault",
            endpoint: DotEndpoint.type,
            connectorOverlays: [
                { type: "Arrow", options: { location: [0.5, 0.5] } }
            ],
            // connector: FlowchartConnector.type
        });
    }
    /**
     * 创建开始节点
     */
    private CreateStartStep() {
        this.CreateStepElement(AllStepInfos.StartStep);
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
}