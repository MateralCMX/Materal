import { CanvasManager } from "../CanvasManager";
import { AllWorkflowDataTypes, WorkflowDataType } from "../WorkflowDataType";
import { ThenStepData } from "../StepDatas/ThenStepData";
import { StepFrom } from "./StepForm";

export class StartStepForm extends StepFrom {
    private runtimeDataPropertys: HTMLDivElement;
    constructor(settingsElement: HTMLElement, stepDataForm: HTMLFormElement, canvasManager: CanvasManager) {
        super(settingsElement, stepDataForm, canvasManager);
        this.BindInputElement(stepDataForm);
        this.BindButtonElement(stepDataForm, element => {
            switch (element.id) {
                case "BtnAddRuntimeDataProperty":
                    element.addEventListener("click", _ => this.AddBtnAddRuntimeDataPropertyClick());
                    break;
            }
        });
        this.BindDivElement(stepDataForm);
    }
    /**
     * 绑定Div元素
     * @param stepDataForm 
     */
    private BindDivElement(stepDataForm: HTMLFormElement) {
        const elements = stepDataForm.getElementsByTagName("div");
        for (let i = 0; i < elements.length; i++) {
            const element = elements[i];
            if (element.id === null || element.id === undefined || element.id === "") continue;
            switch (element.id) {
                case "RuntimeDataPropertys":
                    this.runtimeDataPropertys = element;
                    break;
            }
        }
    }
    /**
     * 添加运行时数据属性
     */
    private AddBtnAddRuntimeDataPropertyClick() {
        const propertyNameInput = this.CreateInputElement();
        const runtimeDataTypeSelect = this.CreateSelectElementByArray(AllWorkflowDataTypes, (element: HTMLOptionElement, data: WorkflowDataType) => {
            element.value = data.Value;
            element.innerText = data.Name;
        })
        const deleteButton = this.CreateDeleteButtonElement();
        const inlineFormItem = this.AppendNewInlineFormItemElement(this.runtimeDataPropertys, [propertyNameInput, runtimeDataTypeSelect], deleteButton);
        let oldValue = "";
        propertyNameInput.addEventListener("focusin", e => {
            oldValue = propertyNameInput.value;
        });
        propertyNameInput.addEventListener("focusout", e => {
            let newValue = propertyNameInput.value;
            if (Object.prototype.hasOwnProperty.call(this.canvasManager.runTimeDataTypes, newValue)) {
                propertyNameInput.value = oldValue;
                return;
            }
            const typeValue = runtimeDataTypeSelect.value;
            if (oldValue) {
                this.canvasManager.runTimeDataTypes[newValue] = this.canvasManager.runTimeDataTypes[oldValue];
                delete this.canvasManager.runTimeDataTypes[oldValue];
                this.canvasManager.runTimeDataTypes[newValue] = typeValue;
            }
            else {
                this.canvasManager.runTimeDataTypes[newValue] = typeValue;
            }
        });
        runtimeDataTypeSelect.addEventListener("change", e => {
            let value = propertyNameInput.value;
            if (value && Object.prototype.hasOwnProperty.call(this.canvasManager.runTimeDataTypes, value)) {
                const typeValue = runtimeDataTypeSelect.value;
                this.canvasManager.runTimeDataTypes[value] = typeValue;
            }
        });
        deleteButton.addEventListener("click", e => {
            let value = propertyNameInput.value;
            if (value && Object.prototype.hasOwnProperty.call(this.canvasManager.runTimeDataTypes, value)) {
                delete this.canvasManager.runTimeDataTypes[value];
            }
            this.runtimeDataPropertys.removeChild(inlineFormItem);
        });
    }
    /**
     * 初始化数据
     * @param stepData 
     */
    protected override InitData(stepData: ThenStepData): void {
    }
}