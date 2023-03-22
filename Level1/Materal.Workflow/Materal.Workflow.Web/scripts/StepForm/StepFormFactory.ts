import { CanvasManager } from "../CanvasManager";
import { AllStepDataInfos } from "../StepDataInfo";
import { StartStepForm } from "./StartStepForm";
import { StepFrom } from "./StepForm";
import { ThenStepForm } from "./ThenStepForm";

export class StepFormFactory {
    public static CreateStepFormModel(settingsElement: HTMLElement, stepDataForm: HTMLFormElement, canvasManager: CanvasManager): StepFrom {
        switch (stepDataForm.id) {
            case AllStepDataInfos.StartStep.FormID:
                return new StartStepForm(settingsElement, stepDataForm, canvasManager);
            case AllStepDataInfos.ThenStep.FormID:
                return new ThenStepForm(settingsElement, stepDataForm, canvasManager);
            default:
                throw new Error("未找到对应类型的Form");
        }
    }
}