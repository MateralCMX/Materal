import { AddFlowTemplateModel } from "../models/FlowTemplate/AddFlowTemplateModel";
import { EditFlowTemplateModel } from "../models/FlowTemplate/EditFlowTemplateModel";
import { QueryFlowTemplateModel } from "../models/FlowTemplate/QueryFlowTemplateModel";
import { FlowTemplate } from "../models/FlowTemplate/FlowTemplate";
import { BaseActionService } from "./BaseActionService";

class FlowTemplateService extends BaseActionService<FlowTemplate, QueryFlowTemplateModel, AddFlowTemplateModel, EditFlowTemplateModel> {
    constructor() {
        super("FlowTemplate");
    }
}
export default new FlowTemplateService();