import { CanvasManager } from "../CanvasManager";
import { AllStepDataInfos } from "../StepDataInfo";
import { StepFrom } from "./StepForm";
import { ThenStepForm } from "./ThenStepForm";

export class StepFormFactory {
    public static CreateStepFormModel(stepDataForm: HTMLFormElement, canvasManager: CanvasManager): StepFrom {
        switch (stepDataForm.id) {
            case AllStepDataInfos.ThenStep.FormID:
                return new ThenStepForm(stepDataForm, canvasManager);
            default:
                throw new Error("未找到对应类型的Form");
        }
    }
}