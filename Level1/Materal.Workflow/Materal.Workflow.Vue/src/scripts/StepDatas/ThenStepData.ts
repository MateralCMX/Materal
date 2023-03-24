import { IStepData } from "./Base/IStepData";
import { StepData } from "./Base/StepData";

export class ThenStepData extends StepData {
    public Next?: IStepData;
    constructor() {
        super(`${ThenStepData.name}`, "业务节点");
    }
}