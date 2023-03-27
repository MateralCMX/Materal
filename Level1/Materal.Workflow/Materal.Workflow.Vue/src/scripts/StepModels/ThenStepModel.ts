import { StepModel } from "./Base/StepModel";
import { Connection } from "@jsplumb/core";
import { ThenStepData } from "../StepDatas/ThenStepData";
import { IStepData } from "../StepDatas/Base/IStepData";

export class ThenStepModel extends StepModel<ThenStepData> {
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${ThenStepModel.name}`, id, instance, element, new ThenStepData());
    }
    public HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean {
        switch (connection.params.cssClass) {
            case "NextConnector":
                if (this.StepData.Next) return false;
                this.StepData.Next = target.StepData;
                break;
            case "CompensateConnector":
                if (this.StepData.CompensateStep) return false;
                if (target.StepData.StepDataType !== `${ThenStepData.name}`) return false;
                this.StepData.CompensateStep = target.StepData as ThenStepData;
                break;
            default: return false;
        }
        return true;
    }
    public HandlerDisconnection(connection: Connection, target: StepModel<IStepData>): boolean {
        switch (connection.params.cssClass) {
            case "NextConnector":
                if (!this.StepData.Next) return false;
                this.StepData.Next = undefined;
                break;
            case "CompensateConnector":
                if (!this.StepData.CompensateStep) return false;
                this.StepData.CompensateStep = undefined;
                break;
            default: return false;
        }
        return true;
    }
    public Destroy(): void {
    }
}