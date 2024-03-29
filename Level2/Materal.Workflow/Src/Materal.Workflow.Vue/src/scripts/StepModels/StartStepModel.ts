import { StartStepData } from "../StepDatas/StartStepData";
import { Connection } from "@jsplumb/core";
import { ConnectorType, StepModel } from "./Base/StepModel";
import { IStepData } from "../StepDatas/Base/IStepData";

export class StartStepModel extends StepModel<StartStepData> {
    constructor(id: string, instance: any, element: HTMLElement) {
        super(`${StartStepModel.name}`, id, instance, element, new StartStepData(id));
    }
    public HandlerConnection(connection: Connection, target: StepModel<IStepData>): boolean {
        if(connection.params.cssClass != ConnectorType.NextConnector) return false;
        if(this.StepData.Next) return false;
        this.StepData.Next = target.StepData;
        target.AddOtherConnector(connection);
        return true;
    }
    public HandlerDisconnection(connection: Connection, target: StepModel<IStepData>) {
        if(connection.params.cssClass != ConnectorType.NextConnector) return;
        if(!this.StepData.Next) return;
        this.StepData.Next = undefined;
        target.RemoveOtherConnector(connection);
    }
    public Destroy(): void {
        throw new Error("开始节点不能销毁");
    }
}