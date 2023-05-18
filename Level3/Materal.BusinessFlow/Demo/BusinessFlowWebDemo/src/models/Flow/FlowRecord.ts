import { BaseDomain } from "../BaseDomain";
import { NodeHandleTypeEnum } from "../Node/NodeHandleTypeEnum";
import { FlowRecordStateEnum } from "./FlowRecordStateEnum";

export class FlowRecord extends BaseDomain {
    /**
     * 流程唯一标识
     */
    public FlowID: string = "";
    /**
     * 步骤唯一标识
     */
    public StepID: string = "";
    /**
     * 节点唯一标识
     */
    public NodeID: string = "";
    /**
     * 可操作用户
     */
    public UserID?: string;
    /**
     * 操作用户
     */
    public OperationUserID?: string;
    /**
     * 状态
     */
    public State: FlowRecordStateEnum = FlowRecordStateEnum.Wait;
    /**
     * 位序
     */
    public SortIndex: number = 0;
    /**
     * 节点处理类型
     */
    public NodeHandleType: NodeHandleTypeEnum = NodeHandleTypeEnum.Auto;
    /**
     * 数据
     */
    public Data?: string;
    /**
     * 返回消息
     */
    public ResultMessage?: string;
    /**
     * 流程模版唯一标识
     */
    public FlowTemplateID: string = "";
    /**
     * 流程模版名称
     */
    public FlowTemplateName: string = "";
    /**
     * 步骤名称
     */
    public StepName: string = "";
    /**
     * 节点名称
     */
    public NodeName: string = "";
    /**
     * 状态文本
     */
    public StateText: string = "";
}