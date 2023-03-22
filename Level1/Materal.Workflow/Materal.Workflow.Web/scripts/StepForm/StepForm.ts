import { CanvasManager } from "../CanvasManager";
import { StepData } from "../StepDatas/Base/StepData";

export abstract class StepFrom {
    protected settingsElement: HTMLElement;
    protected inputs: { [key: string]: HTMLInputElement } = {};
    protected selects: { [key: string]: HTMLSelectElement } = {};
    protected buttons: { [key: string]: HTMLButtonElement } = {};
    protected stepDataForm: HTMLFormElement;
    protected nowStepData: StepData | null = null;
    protected canvasManager: CanvasManager;
    constructor(settingsElement: HTMLElement, stepDataForm: HTMLFormElement, canvasManager: CanvasManager) {
        this.settingsElement = settingsElement;
        this.stepDataForm = stepDataForm;
        this.canvasManager = canvasManager;
    }
    /**
     * 绑定输入框
     * @param formElement 
     */
    protected BindInputElement(formElement: HTMLFormElement, handlerElement?: (input: HTMLInputElement) => void) {
        this.BindElement(formElement, "input", this.inputs, element => {
            element.addEventListener("change", (e: { target: HTMLInputElement; }) => {
                if (this.nowStepData === null) return;
                this.nowStepData[e.target.id] = e.target.value;
            });
        }, handlerElement);
    }
    /**
     * 绑定下拉框
     * @param formElement 
     */
    protected BindSelectElement(formElement: HTMLFormElement, handlerElement?: (input: HTMLSelectElement) => void) {
        this.BindElement(formElement, "select", this.selects, element => {
            element.addEventListener("change", (e: { target: HTMLSelectElement; }) => {
                if (this.nowStepData === null) return;
                this.nowStepData[e.target.id] = e.target.value;
                this.settingsElement.innerHTML = "";
            });
        }, handlerElement);
    }
    protected BindButtonElement(formElement: HTMLFormElement, handlerElement?: (input: HTMLButtonElement) => void) {
        this.BindElement(formElement, "button", this.buttons, element => {
            switch (element.id) {
                case "BtnDelete":
                    element.addEventListener("click", (e: { target: HTMLSelectElement; }) => {
                        if (this.nowStepData === null) return;
                        this.canvasManager.DeleteStepElement(this.nowStepData.ID);
                        this.settingsElement.innerHTML = "";
                    });
                    break;
            }
        }, handlerElement);
    }
    private BindElement(formElement: HTMLFormElement, tagName: string, elementSources: any, baseHandler?: (element: any) => void, handlerElement?: (element: any) => void) {
        const elements = formElement.getElementsByTagName(tagName);
        for (let i = 0; i < elements.length; i++) {
            const element = elements[i];
            if (element.id === null || element.id === undefined || element.id === "") continue;
            elementSources[element.id] = element;
            if (baseHandler != null) {
                baseHandler(element);
            }
            if (handlerElement != null) {
                handlerElement(element);
            }
        }
    }
    /**
     * 显示
     * @param settingsElement 
     */
    public Show(stepData: StepData) {
        this.settingsElement.innerHTML = "";
        this.InitData(stepData);
        this.settingsElement.appendChild(this.stepDataForm);
    }
    /**
     * 初始化数据
     * @param stepData 
     */
    protected abstract InitData(stepData: StepData): void;
}