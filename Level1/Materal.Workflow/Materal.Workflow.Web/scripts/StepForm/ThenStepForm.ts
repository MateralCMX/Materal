import { CanvasManager } from "../CanvasManager";
import { ThenStepData } from "../StepDatas/ThenStepData";
import { AllStepBodyInfos, StepBodyInfo } from "../StepInfo";
import { StepFrom } from "./StepForm";
import { ValueSourceEnum } from "../ValueSourceEnum";
import { KeyValueModel } from "../KeyValueModel";

export class ThenStepForm extends StepFrom {
    private buildDataPropertys: HTMLDivElement;
    private thenInputs: HTMLDivElement;
    private stepTypeElement: HTMLSelectElement;
    constructor(settingsElement: HTMLElement, stepDataForm: HTMLFormElement, canvasManager: CanvasManager) {
        super(settingsElement, stepDataForm, canvasManager);
        this.BindInputElement(stepDataForm);
        this.BindSelectElement(stepDataForm, element => {
            switch (element.id) {
                case "StepType":
                    this.stepTypeElement = element;
                    this.BindSelectByStepTypeData(element);
                    break;
            }
        });
        this.BindButtonElement(stepDataForm, element => {
            switch (element.id) {
                case "BtnAddBuildDataProperty":
                    element.addEventListener("click", _ => this.AddBtnBuildDataPropertyClick(this.buildDataPropertys));
                    break;
                case "BtnAddInput":
                    element.addEventListener("click", _ => this.AddBtnAddInputClick());
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
                case "Inputs":
                    this.thenInputs = element;
                    break;
                case "BuildDataPropertys":
                    this.buildDataPropertys = element;
                    break;
            }
        }
    }
    /**
     * 绑定选择框节点类型数据
     * @param element 
     */
    private BindSelectByStepTypeData(element: HTMLSelectElement) {
        for (let i = 0; i < AllStepBodyInfos.length; i++) {
            const step = AllStepBodyInfos[i];
            const option = document.createElement("option");
            option.innerText = step.Name;
            option.value = step.ID;
            element.appendChild(option);
        }
    }
    /**
    * 添加输入组
    */
    private AddBtnAddInputClick() {
        const valueSources: Array<KeyValueModel<number>> = [];
        for (const key in ValueSourceEnum) {
            if (!Object.prototype.hasOwnProperty.call(ValueSourceEnum, key)) continue;
            if (typeof ValueSourceEnum[key] !== "number") continue;
            const enumValue: any = ValueSourceEnum[key];
            valueSources.push(new KeyValueModel<ValueSourceEnum>(key, enumValue));
        }
        let stepBody: StepBodyInfo | null = null;
        for (let i = 0; i < AllStepBodyInfos.length; i++) {
            const stepBodyInfo = AllStepBodyInfos[i];
            if (stepBodyInfo.ID !== this.stepTypeElement.value) continue;
            stepBody = stepBodyInfo;
            break;
        }
        if (stepBody === null) return;
        const stepDataSelect = this.CreateSelectElementByObject(stepBody.Step);
        const valueSourceSelect = this.CreateSelectElementByArray(valueSources, (element, data) => {
            element.value = data.Value.toString();
            element.innerText = data.Key;
        });
        const valueDiv = document.createElement("div");
        const createRunTimeData = () => {
            const temp = this.CreateSelectElementByObject(this.canvasManager.runTimeDataTypes, (element, data) => {
                element.value = data;
                element.innerText = data;
            });
            return temp;
        };
        const createBuildData = () => {
            if(!this.nowStepData) return document.createElement("div");
            const temp = this.CreateSelectElementByObject(this.nowStepData.BuildData, (element, data) => {
                element.value = data;
                element.innerText = data;
            });
            return temp;
        };
        const createConstantData = () => {
            const temp = this.CreateInputElement();
            return temp;
        };
        valueDiv.appendChild(createRunTimeData());
        const deleteButton = this.CreateDeleteButtonElement();
        const inlineFormItem = this.AppendNewInlineFormItemElement(this.thenInputs, [stepDataSelect, valueSourceSelect, valueDiv], deleteButton);
        valueSourceSelect.addEventListener("change", e => {
            valueDiv.innerHTML = "";
            switch (valueSourceSelect.value) {
                case "0":
                    valueDiv.appendChild(createRunTimeData());
                    break;
                case "1":
                    valueDiv.appendChild(createBuildData());
                    break;
                case "2":
                    valueDiv.appendChild(createConstantData());
                    break;
            }
        });
        deleteButton.addEventListener("click", e => {
            this.thenInputs.removeChild(inlineFormItem);
        });
    }
    /**
     * 初始化数据
     * @param stepData 
     */
    protected override InitData(stepData: ThenStepData): void {
        if (!stepData.StepType) {
            const element = this.selects["StepType"] as HTMLSelectElement;
            stepData.StepType = element.value;
        }
        this.stepDataForm.reset();
        this.nowStepData = stepData;
        for (const key in stepData) {
            if (!Object.prototype.hasOwnProperty.call(stepData, key)) continue;
            const value = stepData[key];
            if (value === null || value == undefined) continue;
            if (Object.prototype.hasOwnProperty.call(this.inputs, key)) {
                const element = this.inputs[key] as HTMLInputElement;
                element.value = value;
            }
            if (Object.prototype.hasOwnProperty.call(this.selects, key)) {
                const element = this.selects[key] as HTMLSelectElement;
                element.value = value;
            }
        }
        this.buildDataPropertys.innerHTML = "";
        for (const key in stepData.BuildData) {
            if (!Object.prototype.hasOwnProperty.call(stepData.BuildData, key)) continue;
            const data = stepData.BuildData[key];
            this.AddBtnBuildDataPropertyClick(this.buildDataPropertys, key, data);
        }
    }
}