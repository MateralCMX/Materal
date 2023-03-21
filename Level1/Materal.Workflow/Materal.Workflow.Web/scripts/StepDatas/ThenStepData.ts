import { InputData } from "./Base/InputData";
import { OutputData } from "./Base/OutputData";
import { StepData } from "./Base/StepData";
import { ErrorHandler } from "./Base/ErrorHandler";
/**
 * 普通节点数据
 */
export class ThenStepData extends StepData {
    /**
     * 节点类型
     */
    public StepType: string;
    /**
     * 输入
     */
    public Inputs?: Array<InputData>;
    /**
     * 输出
     */
    public Outputs?: Array<OutputData>;
    /**
     * 补偿处理节点
     */
    public CompensateStep?: ThenStepData;
    /**
     * 错误处理
     */
    public Error?: ErrorHandler;
    /**
     * 下一步
     */
    public Next?: StepData;
    constructor() {
        super(ThenStepData.name);
    }
}
