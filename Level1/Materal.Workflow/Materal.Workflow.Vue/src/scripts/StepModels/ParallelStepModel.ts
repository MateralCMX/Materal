import { ConnectorType, StepModel } from "./Base/StepModel";
import { Connection } from "@jsplumb/core";
import { ParallelStepData } from "../StepDatas/ParallelStepData";
import { IStepData } from "../StepDatas/Base/IStepData";

export class ParallelStepModel extends StepModel<ParallelStepData> {
    private _nextConnector?: Connection;
    private _stepConnectors: Connection[] = [];
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${ParallelStepModel.name}`, id, instance, element, new ParallelStepData(id));
    }
    public HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean {
        switch (connection.params.cssClass) {
            case ConnectorType.NextConnector:
                if (this.StepData.Next) return false;
                this.StepData.Next = target.StepData;
                this._nextConnector = connection;
                target.AddOtherConnector(connection);
                break;
            case ConnectorType.StepConnector:
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
            case ConnectorType.NextConnector:
                if (!this.StepData.Next) return;
                this.StepData.Next = undefined;
                this._nextConnector = undefined;
                target.RemoveOtherConnector(connection);
                break;
            case ConnectorType.StepConnector:
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