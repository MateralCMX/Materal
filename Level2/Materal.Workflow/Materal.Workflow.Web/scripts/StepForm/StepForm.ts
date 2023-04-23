import { CanvasManager } from "../CanvasManager";
import { StepData } from "../StepDatas/Base/StepData";
import { AllWorkflowDataTypes, WorkflowDataType } from "../WorkflowDataType";

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
    /**
     * 绑定按钮
     * @param formElement 
     * @param handlerElement 
     */
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
    /**
     * 追加新的输入组
     * @param elements 
     */
    protected AppendNewInlineFormItemElement(parentElement: HTMLElement, elements: HTMLElement[], deleteButton?: HTMLButtonElement): HTMLDivElement {
        const inlineFormItem = this.CreateInlineFormItem(elements, deleteButton);
        parentElement.appendChild(inlineFormItem);
        return inlineFormItem;
    }
    /**
     * 创建的输入组
     * @param elements 
     */
    protected CreateInlineFormItem(elements: HTMLElement[], deleteButton?: HTMLButtonElement): HTMLDivElement {
        const inlineFormItem = document.createElement("div");
        inlineFormItem.classList.add("inlineFormItem");
        inlineFormItem.classList.add(`inlineFormItem-${elements.length}`);
        for (let i = 0; i < elements.length; i++) {
            const element = elements[i];
            inlineFormItem.appendChild(element);
        }
        if (deleteButton) {
            inlineFormItem.appendChild(deleteButton);
        }
        return inlineFormItem;
    }
    /**
     * 根据Object创建Select元素
     * @param datas 
     * @param bindData 
     * @returns 
     */
    protected CreateSelectElementByObject<T>(datas: T, bindData?: (koptionElement: HTMLOptionElement, key: string) => void): HTMLSelectElement {
        const selectElement = document.createElement("select");
        selectElement.classList.add("input");
        for (const key in datas) {
            const optionElement = document.createElement("option");
            if (bindData) {
                bindData(optionElement, key);
            }
            else {
                optionElement.value = key
                optionElement.innerText = key;
            }
            selectElement.appendChild(optionElement);
        }
        return selectElement;
    }
    /**
     * 根据数组创建Select元素
     * @param datas 
     * @param bindData 
     * @returns 
     */
    protected CreateSelectElementByArray<T>(datas: Array<T>, bindData: (koptionElement: HTMLOptionElement, data: T) => void): HTMLSelectElement {
        const selectElement = document.createElement("select");
        selectElement.classList.add("input");
        for (let i = 0; i < datas.length; i++) {
            const optionElement = document.createElement("option");
            const element = datas[i];
            bindData(optionElement, element);
            selectElement.appendChild(optionElement);
        }
        return selectElement;
    }
    /**
     * 创建删除按钮
     */
    protected CreateDeleteButtonElement(): HTMLButtonElement {
        const deleteButton = document.createElement("button");
        deleteButton.type = "button";
        deleteButton.classList.add("deleteButton");
        deleteButton.innerText = "X";
        return deleteButton;
    }
    /**
     * 创建输入框
     */
    protected CreateInputElement(type: string = "text"): HTMLInputElement {
        const inputElement = document.createElement("input");
        inputElement.classList.add("input");
        inputElement.type = type;
        return inputElement;
    }
    /**
     * 添加构建数据属性
     */
    protected AddBtnBuildDataPropertyClick(buildDataPropertys: HTMLDivElement, name?: string, value?: string) {
        const propertyNameInput = this.CreateInputElement();
        if (name) propertyNameInput.value = name;
        const runtimeDataTypeSelect = this.CreateSelectElementByArray(AllWorkflowDataTypes, (element: HTMLOptionElement, data: WorkflowDataType) => {
            element.value = data.Value;
            element.innerText = data.Name;
        })
        if (value) runtimeDataTypeSelect.value = value;
        const deleteButton = this.CreateDeleteButtonElement();
        const inlineFormItem = this.AppendNewInlineFormItemElement(buildDataPropertys, [propertyNameInput, runtimeDataTypeSelect], deleteButton);
        let oldValue = "";
        propertyNameInput.addEventListener("focusin", e => {
            oldValue = propertyNameInput.value;
        });
        propertyNameInput.addEventListener("focusout", e => {
            if (this.nowStepData == null) return;
            let newValue = propertyNameInput.value;
            if (Object.prototype.hasOwnProperty.call(this.nowStepData.BuildData, newValue)) {
                propertyNameInput.value = oldValue;
                return;
            }
            const typeValue = runtimeDataTypeSelect.value;
            if (oldValue) {
                this.nowStepData.BuildData[newValue] = this.nowStepData.BuildData[oldValue];
                delete this.nowStepData.BuildData[oldValue];
                this.nowStepData.BuildData[newValue] = typeValue;
            }
            else {
                this.nowStepData.BuildData[newValue] = typeValue;
            }
        });
        runtimeDataTypeSelect.addEventListener("change", e => {
            if (this.nowStepData == null) return;
            let value = propertyNameInput.value;
            if (value && Object.prototype.hasOwnProperty.call(this.nowStepData.BuildData, value)) {
                const typeValue = runtimeDataTypeSelect.value;
                this.nowStepData.BuildData[value] = typeValue;
            }
        });
        deleteButton.addEventListener("click", e => {
            if (this.nowStepData == null) return;
            let value = propertyNameInput.value;
            if (value && Object.prototype.hasOwnProperty.call(this.nowStepData.BuildData, value)) {
                delete this.nowStepData.BuildData[value];
            }
            buildDataPropertys.removeChild(inlineFormItem);
        });
    }
}