import { FlowRecord } from "../models/Flow/FlowRecord";
import { OperationFlowRequestModel } from "../models/Flow/OperationFlowRequestModel";
import { SaveFlowDataRequestModel } from "../models/Flow/SaveFlowDataRequestModel";
import { FlowTemplate } from "../models/FlowTemplate/FlowTemplate";
import { ResultDataModel } from "../models/ResultDataModel";
import { ResultModel } from "../models/ResultModel";
import { BaseService } from "./BaseService";

class FlowService extends BaseService {
    /**
     * 控制器名称
     */
    private controllerName = "Flow";
    /**
     * 启动一个新的流程
     */
    public async StartNewFlowAsync(userID: string, flowTemplateID: string): Promise<ResultDataModel<string> | undefined> {
        const urlParams = {
            "userID": userID,
            "flowTemplateID": flowTemplateID
        };
        const result = await this.SendPutAsync<ResultDataModel<string>>(`/${this.controllerName}/StartNewFlow`, urlParams);
        return result;
    }
    /**
     * 获得用户参与流程模版列表
     */
    public async GetUserFlowTemplatesAsync(userID: string): Promise<ResultDataModel<FlowTemplate[]> | undefined> {
        const urlParams = {
            "userID": userID
        };
        const result = await this.SendGetAsync<ResultDataModel<FlowTemplate[]>>(`/${this.controllerName}/GetUserFlowTemplates`, urlParams);
        return result;
    }
    /**
     * 获得待办事项
     */
    public async GetBacklogAsync(userID: string, flowTemplateID?: string): Promise<ResultDataModel<FlowRecord[]> | undefined> {
        const urlParams = {
            "userID": userID,
            "flowTemplateID": flowTemplateID
        };
        if (!flowTemplateID) delete urlParams.flowTemplateID;
        const result = await this.SendGetAsync<ResultDataModel<FlowRecord[]>>(`/${this.controllerName}/GetBacklog`, urlParams);
        return result;
    }
    /**
     * 根据流程记录唯一标识获得流程数据
     */
    public async GetFlowDatasByFlowRecordIDAsync(flowTemplateID: string, flowRecordID: string): Promise<ResultDataModel<Record<string, any | undefined>> | undefined> {
        const urlParams = {
            "flowTemplateID": flowTemplateID,
            "flowRecordID": flowRecordID
        };
        const result = await this.SendGetAsync<ResultDataModel<Record<string, any | undefined>>>(`/${this.controllerName}/GetFlowDatasByFlowRecordID`, urlParams);
        return result;
    }
    /**
     * 根据流程唯一标识获得流程数据
     */
    public async GetFlowDatasAsync(flowTemplateID: string, flowID: string): Promise<ResultDataModel<Record<string, any | undefined>> | undefined> {
        const urlParams = {
            "flowTemplateID": flowTemplateID,
            "flowID": flowID
        };
        const result = await this.SendGetAsync<ResultDataModel<Record<string, any | undefined>>>(`/${this.controllerName}/GetFlowDatas`, urlParams);
        return result;
    }
    /**
     * 完成节点
     */
    public async ComplateFlowNodeAsync(model: OperationFlowRequestModel): Promise<ResultModel | undefined> {
        const result = await this.SendPostAsync<ResultDataModel<Record<string, any | undefined>>>(`/${this.controllerName}/ComplateFlowNode`, null, model);
        return result;
    }
    /**
     * 打回节点
     */
    public async RepulseFlowNodeAsync(model: OperationFlowRequestModel): Promise<ResultModel | undefined> {
        const result = await this.SendPostAsync<ResultDataModel<Record<string, any | undefined>>>(`/${this.controllerName}/RepulseFlowNode`, null, model);
        return result;
    }
    /**
     * 保存流程数据
     */
    public async SaveFlowDataAsync(model: SaveFlowDataRequestModel): Promise<ResultModel | undefined> {
        const result = await this.SendPostAsync<ResultDataModel<Record<string, any | undefined>>>(`/${this.controllerName}/SaveFlowData`, null, model);
        return result;
    }
}
export default new FlowService();