import { ResultDataModel } from "../models/ResultDataModel";
import { AddStepModel } from "../models/Step/AddStepModel";
import { EditStepModel } from "../models/Step/EditStepModel";
import { QueryStepModel } from "../models/Step/QueryStepModel";
import { Step } from "../models/Step/Step";
import { BaseActionService } from "./BaseActionService";

class StepService extends BaseActionService<Step, QueryStepModel, AddStepModel, EditStepModel> {
    constructor() {
        super("Step");
    }
    /**
     * 根据流程模版唯一标识获得列表
     */
    public async GetListByFlowTemplateIDAsync(flowTemplateID: string): Promise<ResultDataModel<Step[]> | undefined> {
        const urlParams = {
            "flowTemplateID": flowTemplateID
        };
        const result = await this.SendGetAsync<ResultDataModel<Step[]>>(`/${this.controllerName}/GetListByFlowTemplateID`, urlParams);
        return result;
    }
}
export default new StepService();