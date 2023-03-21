import { StepData } from "../StepDatas/Base/StepData";

export abstract class StepFrom {
    protected inputs: { [key: string]: HTMLInputElement } = {};
    protected selects: { [key: string]: HTMLSelectElement } = {};
    protected stepDataForm: HTMLFormElement;
    protected nowStepData: StepData | null = null;
    constructor(stepDataForm: HTMLFormElement) {
        this.stepDataForm = stepDataForm;
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
        this.BindElement(formElement, "select", this.selects, element=>{
            element.addEventListener("change", (e: { target: HTMLSelectElement; }) => {
                if (this.nowStepData === null) return;
                this.nowStepData[e.target.id] = e.target.value;
            });
        }, handlerElement);
    }
    private BindElement(formElement: HTMLFormElement, tagName: string, elementSources: any, baseHandler?: (element: any) => void, handlerElement?: (element: any) => void) {
        const elements = formElement.getElementsByTagName(tagName);
        for (const key in elements) {
            if (!Object.prototype.hasOwnProperty.call(elements, key)) continue;
            const element = elements[key];
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
    public Show(settingsElement: HTMLElement, stepData: StepData) {
        settingsElement.innerHTML = "";
        this.InitData(stepData);
        settingsElement.appendChild(this.stepDataForm);
    }
    /**
     * 初始化数据
     * @param stepData 
     */
    protected abstract InitData(stepData: StepData): void;
}