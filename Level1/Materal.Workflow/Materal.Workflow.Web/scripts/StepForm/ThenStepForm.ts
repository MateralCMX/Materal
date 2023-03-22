import { CanvasManager } from "../CanvasManager";
import { ThenStepData } from "../StepDatas/ThenStepData";
import { AllStepBodyInfos } from "../StepInfo";
import { StepFrom } from "./StepForm";

export class ThenStepForm extends StepFrom {
    constructor(stepDataForm: HTMLFormElement, canvasManager: CanvasManager) {
        super(stepDataForm, canvasManager);
        this.BindInputElement(stepDataForm);
        this.BindSelectElement(stepDataForm, element => {
            switch (element.id) {
                case "StepType":
                    this.BindSelectByStepTypeData(element);
                    break;
            }
        });
        this.BindButtonElement(stepDataForm);
    }
    private BindSelectByStepTypeData(element: HTMLSelectElement) {
        for (const key in AllStepBodyInfos) {
            if (!Object.prototype.hasOwnProperty.call(AllStepBodyInfos, key)) continue;
            const step = AllStepBodyInfos[key];
            const option = document.createElement("option");
            option.innerText = step.Name;
            option.value = step.ID;
            element.appendChild(option);
        }
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
    }
}