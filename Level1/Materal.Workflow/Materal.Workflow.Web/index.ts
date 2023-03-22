import { CanvasManager } from "./scripts/CanvasManager";
import { StepData } from "./scripts/StepDatas/Base/StepData";
import { StepDataInfo, AllStepDataInfos } from "./scripts/StepDataInfo";
import { StepFrom } from "./scripts/StepForm/StepForm";
import { StepFormFactory } from "./scripts/StepForm/StepFormFactory";

/**
 * 主页
 */
class IndexPage {
    private canvasManager: CanvasManager;
    private settingsElement: HTMLElement;
    private stepDataForms: StepFrom[] = [];
    constructor() {
        this.InitStpes();
        this.InitCanvas();
        this.InitSettings();
        this.canvasManager.CreateStepElement(AllStepDataInfos.ThenStep);
    }
    /**
     * 初始化节点组
     */
    private InitStpes() {
        const stepsElement = document.getElementById("Steps");
        if (stepsElement === null) throw new Error("获取Steps元素失败");
        for (const key in AllStepDataInfos) {
            if (!Object.prototype.hasOwnProperty.call(AllStepDataInfos, key)) continue;
            const stepData = AllStepDataInfos[key] as StepDataInfo;
            if (!stepData.CanCreate) continue;
            const node = document.createElement("div");
            node.id = stepData.ID;
            node.classList.add("Step");
            node.classList.add(stepData.ID);
            node.innerText = stepData.Name;
            node.addEventListener("click", e => this.StepsStepElementClick(e));
            stepsElement.appendChild(node);
        }
    }
    /**
     * 初始化画布
     */
    private InitCanvas() {
        let canvas = document.getElementById("Canvas");
        if (canvas == null) throw new Error("未找到元素canvas");
        this.canvasManager = new CanvasManager(canvas, this.SelectedStep.bind(this));
    }
    /**
     * 节点组中节点元素单击
     * @param e 
     * @returns 
     */
    private StepsStepElementClick(e: Event) {
        if (e.target === null) return;
        const sourceElement: HTMLDivElement = (e.target as HTMLDivElement);
        this.canvasManager.CreateStepElement(AllStepDataInfos[sourceElement.id]);
    }
    /**
     * 选中节点
     * @param stepData 
     * @param element 
     */
    private SelectedStep(stepData: StepData, element: HTMLElement, stepInfo: StepDataInfo) {
        if (!Object.prototype.hasOwnProperty.call(this.stepDataForms, stepData.StepDataTypeName)) {
            this.settingsElement.innerHTML = "";
            return;
        }
        this.stepDataForms[stepData.StepDataTypeName].Show(this.settingsElement, stepData);
    }
    /**
     * 初始化设置
     */
    private InitSettings() {
        const settingsElement = document.getElementById("Settings");
        if (settingsElement == null) throw new Error("未找到元素Settings");
        this.settingsElement = settingsElement;
        for (const key in this.settingsElement.childNodes) {
            if (!Object.prototype.hasOwnProperty.call(this.settingsElement.childNodes, key)) continue;
            const element = this.settingsElement.childNodes[key] as HTMLFormElement;
            if (element.id === undefined || element.id === null) continue;
            this.stepDataForms[element.id] = StepFormFactory.CreateStepFormModel(element, this.canvasManager);
        }
        this.settingsElement.innerHTML = "";
    }
}
new IndexPage();
