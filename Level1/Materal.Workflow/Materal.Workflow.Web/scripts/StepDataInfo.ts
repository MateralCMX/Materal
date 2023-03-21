import { StepData } from "./StepDatas/Base/StepData";
import { StartStepData } from "./StepDatas/StartStepData";
import { ThenStepData } from "./StepDatas/ThenStepData";

export class StepDataInfo {
    /**
     * 唯一标识
     */
    public ID: string;
    /**
     * 显示名称
     */
    public Name: string;
    /**
     * 表单ID
     */
    public FormID: string;
    /**
     * 源锚点
     */
    public SourceAnchor: boolean = false;
    /**
     * 目标锚点
     */
    public TargetAnchor: boolean;
    /**
     * 是否能人工创建
     */
    public CanCreate: boolean;
    /**
     * 初始化数据
     */
    public InitStepDataAction: () => StepData;
    constructor(id: string, name: string, initStepDataAction: () => StepData, targetAnchor: boolean = true, canCreate: boolean = true) {
        this.ID = id;
        this.FormID = `${id}Data`;
        this.Name = name;
        this.InitStepDataAction = initStepDataAction;
        this.TargetAnchor = targetAnchor;
        this.CanCreate = canCreate;
        const temp = initStepDataAction();
        for (const key in temp) {
            if (key == "Next") {
                this.SourceAnchor = true;
            }
        }
    }
}
export const AllStepDataInfos = {
    StartStep: new StepDataInfo("StartStep", "开始", () => new StartStepData(), false, false),
    ThenStep: new StepDataInfo("ThenStep", "业务", () => new ThenStepData()),
    // ActivityStep: new StepInfo("ActivityStep", "活动"),
    // BranchStep: new StepInfo("BranchStep", "分支"),
    // DelayStep: new StepInfo("DelayStep", "延时"),
    // EventStep: new StepInfo("EventStep", "事件"),
    // ForEachStep: new StepInfo("ForEachStep", "项循环"),
    // IfStep: new StepInfo("IfStep", "决策"),
    // ParallelStep: new StepInfo("ParallelStep", "并行"),
    // RecurStep: new StepInfo("RecurStep", "复发"),
    // ScheduleStep: new StepInfo("ScheduleStep", "计划"),
    // SequenceStep: new StepInfo("SequenceStep", "顺序"),
    // TransactionStep: new StepInfo("TransactionStep", "事务"),
    // WhileStep: new StepInfo("WhileStep", "条件循环"),
    // EndStep: new StepInfo("EndStep", "结束节点", true),
}