import { IStepData } from "./StepDatas/Base/IStepData";
import { StepModel } from "./StepModels/Base/StepModel";

export interface IStep<T extends IStepData> {
    GetStepModel: () => StepModel<T>,
    GetStepID: () => string,
    BindNext: (next?: StepModel<T>) => void,
    BindUp: (next?: StepModel<T>) => void,
}