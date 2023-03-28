import { Connection } from "@jsplumb/core";
import { StepModel } from "./Base/StepModel";
import { IStepData } from "../StepDatas/Base/IStepData";
import { DelayStepData } from "../StepDatas/DelayStepData";

export class DelayStepModel extends StepModel<DelayStepData> {
    private _nextConnector?: Connection;
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${DelayStepModel.name}`, id, instance, element, new DelayStepData());
    }
    public HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean {
        if (connection.params.cssClass != "NextConnector") return false;
        if (this.StepData.Next) return false;
        this._nextConnector = connection;
        this.StepData.Next = target.StepData;
        target.AddOtherConnector(connection);
        return true;
    }
    public HandlerDisconnection(connection: Connection, target: StepModel<IStepData>) {
        if (connection.params.cssClass != "NextConnector") return;
        if (!this.StepData.Next) return;
        this._nextConnector = undefined;
        this.StepData.Next = undefined;
        target.RemoveOtherConnector(connection);
    }
    public Destroy(): void {
        if (this._nextConnector) this.Instance.deleteConnection(this._nextConnector);
        this.DestroyOtherConnector();
    }
}