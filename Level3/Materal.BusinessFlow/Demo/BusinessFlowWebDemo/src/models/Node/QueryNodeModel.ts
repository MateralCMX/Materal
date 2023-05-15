import { PageRequestModel } from "../PageRequestModel";
import { NodeHandleTypeEnum } from "./NodeHandleTypeEnum";

export class QueryNodeModel extends PageRequestModel {
    /**
     * 名称
     */
    public Name?: string;
    /**
     * 步骤唯一标识
     */
    public StepID?: string;
    /**
     * 处理类型
     */
    public HandleType?: NodeHandleTypeEnum;
}