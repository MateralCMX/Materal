import { StepData } from "./Base/StepData";

export class EndStepData extends StepData {
    constructor() {
        super(`${EndStepData.name}`, "结束节点");
    }
}