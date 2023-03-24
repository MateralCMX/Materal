import { IStepData } from "./Base/IStepData";
import { StepData } from "./Base/StepData";

export class StartStepData extends StepData {
    public Next?: IStepData;
    constructor() {
        super(`${StartStepData.name}`, "开始节点");
    }
}