import { EndStepData } from "../StepDatas/EndStepData";
import { Connection } from "@jsplumb/core";
import { StepModel } from "./Base/StepModel";
import { IStepData } from "../StepDatas/Base/IStepData";

export class EndStepModel extends StepModel<EndStepData> {
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${EndStepModel.name}`, id, instance, element, new EndStepData(id));
    }
    public HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean {
        target.AddOtherConnector(connection);
        return true;
    }
    public HandlerDisconnection(connection: Connection, target: StepModel<IStepData>) {
        target.RemoveOtherConnector(connection);
    }
    public Destroy(): void {
        this.DestroyOtherConnector();
    }
}