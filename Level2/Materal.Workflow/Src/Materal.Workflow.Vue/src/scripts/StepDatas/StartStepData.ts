import { IStepData } from "./Base/IStepData";
import { StepData } from "./Base/StepData";

export class StartStepData extends StepData {
    public Next?: IStepData;
    constructor(id: string) {
        super(`${StartStepData.name}`, "开始节点", id);
    }
}