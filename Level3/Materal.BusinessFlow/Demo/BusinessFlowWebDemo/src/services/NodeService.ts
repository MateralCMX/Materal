import { AddNodeModel } from "../models/Node/AddNodeModel";
import { EditNodeModel } from "../models/Node/EditNodeModel";
import { QueryNodeModel } from "../models/Node/QueryNodeModel";
import { Node } from "../models/Node/Node";
import { BaseActionService } from "./BaseActionService";

class NodeService extends BaseActionService<Node, QueryNodeModel, AddNodeModel, EditNodeModel> {
    constructor() {
        super("Node");
    }
}
export default new NodeService();