import { ConsoleMessageStep } from "./Steps/ConsoleMessageStep";

export class StepBodyInfo {
    public ID: string;
    public Name: string;
    public Step: any;
    constructor(id: string, name: string, step: any) {
        this.ID = id;
        this.Name = name;
        this.Step = step;
    }
}

export const AllStepBodyInfos = [
    new StepBodyInfo("ConsoleMessageStep", "控制台输出", new ConsoleMessageStep())
];