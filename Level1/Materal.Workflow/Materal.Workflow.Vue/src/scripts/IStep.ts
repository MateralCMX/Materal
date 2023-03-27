import { IStepData } from "./StepDatas/Base/IStepData";
import { StepModel } from "./StepModels/Base/StepModel";

export interface IStep<T extends StepModel<T2>, T2 extends IStepData> {
    GetStepModel: () => T | undefined,
    GetStepID: () => string
}