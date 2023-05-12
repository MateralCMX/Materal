import { PageRequestModel } from "../PageRequestModel";

export class QueryStepModel extends PageRequestModel {
    /**
     * 名称
     */
    public Name?: string;
    /**
     * 流程模版唯一标识
     */
    public FlowTemplateID?: string;
    /**
     * 下一步唯一标识
     */
    public NextID?: string;
    /**
     * 上一步唯一标识
     */
    public UpID?: string;
}
