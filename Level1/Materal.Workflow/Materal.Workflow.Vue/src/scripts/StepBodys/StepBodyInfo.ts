import { RuntimeDataPropertyInfo } from "../RuntimeDataType";

export class StepBodyInfo {
    public Name: string;
    public Args: Array<RuntimeDataPropertyInfo>;
    constructor(name: string, args: Array<RuntimeDataPropertyInfo> = []) {
        this.Name = name;
        this.Args = args;
    }
}

export const AllStepBodys: Array<StepBodyInfo> = [
    new StepBodyInfo("EmptyStepBody"),
    new StepBodyInfo("ConsoleMessageStepBody", [{ Name: "Message", Type: "String", Description: "消息" }])
];