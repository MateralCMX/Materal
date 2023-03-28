import { Connection } from "@jsplumb/core";
import { StepModel } from "./Base/StepModel";
import { IStepData } from "../StepDatas/Base/IStepData";
import { DelayStepData } from "../StepDatas/DelayStepData";

export class DelayStepModel extends StepModel<DelayStepData> {
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${DelayStepModel.name}`, id, instance, element, new DelayStepData());
    }
    public HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean {
        if (connection.params.cssClass != "NextConnector") return false;
        if (this.StepData.Next) return false;
        this.StepData.Next = target.StepData;
        target.AddOtherConnector(connection);
        return true;
    }
    public HandlerDisconnection(connection: Connection, target: StepModel<IStepData>) {
        if (connection.params.cssClass != "NextConnector") return;
        if (!this.StepData.Next) return;
        this.StepData.Next = undefined;
        target.RemoveOtherConnector(connection);
    }
    public Destroy(): void {
        this.DestroyOtherConnector();
    }
}