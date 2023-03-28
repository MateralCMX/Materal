import { StepModel } from "./Base/StepModel";
import { Connection } from "@jsplumb/core";
import { ScheduleStepData } from "../StepDatas/ScheduleStepData";
import { IStepData } from "../StepDatas/Base/IStepData";

export class ScheduleStepModel extends StepModel<ScheduleStepData> {
    private _nextConnector?: Connection;
    private _stepConnector?: Connection;
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${ScheduleStepModel.name}`, id, instance, element, new ScheduleStepData());
    }
    public HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean {
        switch (connection.params.cssClass) {
            case "NextConnector":
                if (this.StepData.Next) return false;
                this.StepData.Next = target.StepData;
                this._nextConnector = connection;
                target.AddOtherConnector(connection);
                break;
            case "StepConnector":
                if (this.StepData.StepData) return false;
                this.StepData.StepData = target.StepData;
                this._stepConnector = connection;
                target.AddOtherConnector(connection);
                break;
            default: return false;
        }
        return true;
    }
    public HandlerDisconnection(connection: Connection, target: StepModel<IStepData>) {
        switch (connection.params.cssClass) {
            case "NextConnector":
                if (!this.StepData.Next) return;
                this.StepData.Next = undefined;
                this._nextConnector = undefined;
                target.RemoveOtherConnector(connection);
                break;
            case "StepConnector":
                if (!this.StepData.StepData) return;
                this.StepData.StepData = undefined;
                this._stepConnector = undefined;
                target.RemoveOtherConnector(connection);
                break;
            default: return;
        }
    }
    public Destroy(): void {
        if (this._nextConnector) this.Instance.deleteConnection(this._nextConnector);
        if (this._stepConnector) this.Instance.deleteConnection(this._stepConnector);
        this.DestroyOtherConnector();
    }
}