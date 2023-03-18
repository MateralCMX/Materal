import { CanvasManager } from "./scripts/CanvasManager";
import { StepData } from "./scripts/StepData";
import { StepInfo, AllStepInfos } from "./scripts/StepInfo";

/**
 * 主页
 */
class IndexPage {
    private canvasManager: CanvasManager;
    private settingsElement: HTMLElement;
    private stepDataForms: StepFromModel[] = [];
    constructor() {
        this.InitStpes();
        this.InitCanvas();
        this.InitSettings();
        this.canvasManager.CreateStepElement(AllStepInfos.ThenStep);
    }
    /**
     * 初始化节点组
     */
    private InitStpes() {
        const stepsElement = document.getElementById("Steps");
        if (stepsElement === null) throw new Error("获取Steps元素失败");
        for (const key in AllStepInfos) {
            if (!Object.prototype.hasOwnProperty.call(AllStepInfos, key)) continue;
            const stepData = AllStepInfos[key] as StepInfo;
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
        this.canvasManager.CreateStepElement(AllStepInfos[sourceElement.id]);
    }
    /**
     * 选中节点
     * @param stepData 
     * @param element 
     */
    private SelectedStep(stepData: StepData, element: HTMLElement, stepInfo: StepInfo) {
        console.log(stepData);
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
            this.stepDataForms[element.id] = new StepFromModel(element);
        }
        this.settingsElement.innerHTML = "";
    }
}
class StepFromModel {
    private stepDataForm: HTMLFormElement;
    private inputs: HTMLInputElement[] = [];
    private nowStepData: StepData | null = null;
    constructor(stepDataForm: HTMLFormElement) {
        this.stepDataForm = stepDataForm;
        const inputs = this.stepDataForm.getElementsByTagName("input");
        for (const key in inputs) {
            if (!Object.prototype.hasOwnProperty.call(inputs, key)) continue;
            const element = inputs[key];
            if (element.id === null || element.id === undefined || element.id === "") continue;
            this.inputs[element.id] = element;
            element.addEventListener("change", e => {
                if(this.nowStepData === null) return;
                const target = e.target as HTMLInputElement;
                this.nowStepData[target.id] = target.value;
            });
        }
    }
    /**
     * 显示
     * @param settingsElement 
     */
    public Show(settingsElement: HTMLElement, stepData: StepData) {
        settingsElement.innerHTML = "";
        this.InitData(stepData);
        settingsElement.appendChild(this.stepDataForm);
    }
    /**
     * 初始化数据
     * @param stepData 
     */
    private InitData(stepData: StepData) {
        this.stepDataForm.reset();
        this.nowStepData = stepData;
        for (const key in stepData) {
            if (!Object.prototype.hasOwnProperty.call(stepData, key)) continue;
            if (!Object.prototype.hasOwnProperty.call(this.inputs, key)) continue;
            const value = stepData[key];
            if (value === null || value == undefined) continue;
            const input = this.inputs[key] as HTMLInputElement;
            input.value = value;
        }
    }
}
new IndexPage();