import { StepModel } from "./Base/StepModel";
import { Connection } from "@jsplumb/core";
import { BranchStepData } from "../StepDatas/BranchStepData";
import { IStepData } from "../StepDatas/Base/IStepData";

export class BranchStepModel extends StepModel<BranchStepData> {
    private _nextConnector?: Connection;
    private _stepConnectors: Connection[] = [];
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${BranchStepModel.name}`, id, instance, element, new BranchStepData(id));
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
                for (let i = 0; i < this.StepData.StepDatas.length; i++) {
                    const stepData = this.StepData.StepDatas[i];
                    if (stepData.ID === target.ID) return false;
                }
                this.StepData.StepDatas.push(target.StepData);
                this._stepConnectors.push(connection);
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
                for (let i = 0; i < this._stepConnectors.length; i++) {
                    const stepConnector = this._stepConnectors[i];
                    if (stepConnector.id !== connection.id) continue;
                    this._stepConnectors.splice(i, 1);
                    this.StepData.StepDatas.splice(i, 1);
                    break;
                }
                target.RemoveOtherConnector(connection);
                break;
            default: return;
        }
    }
    public Destroy(): void {
        if (this._nextConnector) this.Instance.deleteConnection(this._nextConnector);
        while (this._stepConnectors.length > 0) {
            const stepConnector = this._stepConnectors[0];
            this.Instance.deleteConnection(stepConnector);
        }
        this.DestroyOtherConnector();
    }
}