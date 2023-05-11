import { AddDataModelFieldModel } from "../models/DataModelField/AddDataModelFieldModel";
import { EditDataModelFieldModel } from "../models/DataModelField/EditDataModelFieldModel";
import { QueryDataModelFieldModel } from "../models/DataModelField/QueryDataModelFieldModel";
import { DataModelField } from "../models/DataModelField/DataModelField";
import { BaseActionService } from "./BaseActionService";

class DataModelFieldService extends BaseActionService<DataModelField, QueryDataModelFieldModel, AddDataModelFieldModel, EditDataModelFieldModel> {
    constructor() {
        super("DataModelField");
    }
}
export default new DataModelFieldService();