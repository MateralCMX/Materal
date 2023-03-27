import { StartStepData } from "../StepDatas/StartStepData";
import { Connection } from "@jsplumb/core";
import { StepModel } from "./Base/StepModel";
import { IStepData } from "../StepDatas/Base/IStepData";

export class StartStepModel extends StepModel<StartStepData> {
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${StartStepModel.name}`, id, instance, element, new StartStepData());
    }
    public HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean {
        if(connection.params.cssClass != "NextConnector") return false;
        if(this.StepData.Next) return false;
        this.StepData.Next = target.StepData;
        return true;
    }
    public HandlerDisconnection(connection: Connection, target: StepModel<IStepData>): boolean {
        if(connection.params.cssClass != "NextConnector") return false;
        if(!this.StepData.Next) return false;
        this.StepData.Next = undefined;
        return true;
    }
    public Destroy(): void {
        throw new Error("开始节点不能销毁");
    }
}