import { AllStepBodys } from "../StepBodys/StepBodyInfo";
import { ErrorHandler } from "./Base/ErrorHandler";
import { InputData } from "./Base/InputData";
import { IStepData } from "./Base/IStepData";
import { OutputData } from "./Base/OutputData";
import { StepData } from "./Base/StepData";

export class ThenStepData extends StepData {
    /**
     * 节点体类型
     */
    public StepBodyType: string = `${AllStepBodys[0].Name}`;
    /**
     * 输入
     */
    public Inputs: Array<InputData> = [];
    /**
     * 输出
     */
    public Outputs: Array<OutputData> = [];
    /**
     * 补偿节点
     */
    public CompensateStep?: ThenStepData;
    /**
     * 错误处理
     */
    public Error?: ErrorHandler;
    /**
     * 下一步
     */
    public Next?: IStepData;
    constructor() {
        super(`${ThenStepData.name}`, "业务节点");
    }
}