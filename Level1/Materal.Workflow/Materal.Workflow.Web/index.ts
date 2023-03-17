import { CanvasManager } from "./scripts/CanvasManager";
import { StepInfo, AllStepInfos } from "./scripts/StepInfo";

class IndexPage {
    private canvasManager: CanvasManager;
    constructor() {
        this.InitStpes();
        this.InitCanvas();
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
        this.canvasManager = new CanvasManager(canvas, this.SelectedStep);
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
    private SelectedStep(stepData: StepInfo, element: HTMLElement) {

    }
}
new IndexPage();