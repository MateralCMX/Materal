import { AddDataModelModel } from "../models/DataModel/AddDataModelModel";
import { EditDataModelModel } from "../models/DataModel/EditDataModelModel";
import { QueryDataModelModel } from "../models/DataModel/QueryDataModelModel";
import { DataModel } from "../models/DataModel/DataModel";
import { BaseActionService } from "./BaseActionService";

class DataModelService extends BaseActionService<DataModel, QueryDataModelModel, AddDataModelModel, EditDataModelModel> {
    constructor() {
        super("DataModel");
    }
}
export default new DataModelService();