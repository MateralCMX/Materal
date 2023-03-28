import { StepData } from "./Base/StepData";

export class EndStepData extends StepData {
    constructor(id: string) {
        super(`${EndStepData.name}`, "结束节点", id);
    }
}