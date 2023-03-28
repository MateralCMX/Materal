import { StepModel } from "./Base/StepModel";
import { Connection } from "@jsplumb/core";
import { ThenStepData } from "../StepDatas/ThenStepData";
import { IStepData } from "../StepDatas/Base/IStepData";

export class ThenStepModel extends StepModel<ThenStepData> {
    private _nextConnector?: Connection;
    private _compensateConnector?: Connection;
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${ThenStepModel.name}`, id, instance, element, new ThenStepData());
    }
    public HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean {
        switch (connection.params.cssClass) {
            case "NextConnector":
                if (this.StepData.Next) return false;
                this.StepData.Next = target.StepData;
                this._nextConnector = connection;
                target.AddOtherConnector(connection);
                break;
            case "CompensateConnector":
                if (this.StepData.CompensateStep) return false;
                if (target.StepData.StepDataType !== `${ThenStepData.name}`) return false;
                this.StepData.CompensateStep = target.StepData as ThenStepData;
                this._compensateConnector = connection;
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
            case "CompensateConnector":
                if (!this.StepData.CompensateStep) return;
                this.StepData.CompensateStep = undefined;
                this._compensateConnector = undefined;
                target.RemoveOtherConnector(connection);
                break;
            default: return;
        }
    }
    public Destroy(): void {
        if (this._nextConnector) this.Instance.deleteConnection(this._nextConnector);
        if (this._compensateConnector) this.Instance.deleteConnection(this._compensateConnector);
        this.DestroyOtherConnector();
    }
}