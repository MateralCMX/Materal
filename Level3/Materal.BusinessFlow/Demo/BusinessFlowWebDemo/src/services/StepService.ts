import { AddStepModel } from "../models/Step/AddStepModel";
import { EditStepModel } from "../models/Step/EditStepModel";
import { QueryStepModel } from "../models/Step/QueryStepModel";
import { Step } from "../models/Step/Step";
import { BaseActionService } from "./BaseActionService";

class StepService extends BaseActionService<Step, QueryStepModel, AddStepModel, EditStepModel> {
    constructor() {
        super("Step");
    }
}
export default new StepService();