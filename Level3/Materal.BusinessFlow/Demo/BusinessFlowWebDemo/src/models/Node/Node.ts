import { BaseDomain } from "../BaseDomain";
import { NodeHandleTypeEnum } from "./NodeHandleTypeEnum";

export class Node extends BaseDomain {
    /**
     * 名称
     */
    public Name: string = "";
    /**
     * 步骤唯一标识
     */
    public StepID: string = "";
    /**
     * 处理类型
     */
    public HandleType: NodeHandleTypeEnum = NodeHandleTypeEnum.Auto;
    /**
     * 执行条件
     */
    public RunConditionExpression?: string;
    /**
     * 数据
     */
    public Data?: string;
    /**
     * 处理数据
     */
    public HandleData?: string;
}