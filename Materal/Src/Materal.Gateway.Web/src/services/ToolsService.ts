import GetRouteFromConsulModel from "../models/tools/GetRouteFromConsulModel";
import GetSwaggerFromConsulModel from "../models/tools/GetSwaggerFromConsulModel";
import BaseService from "./BaseService";

class ToolsService extends BaseService {
    public async GetSwaggerFromConsulAsync(requestModel: GetSwaggerFromConsulModel): Promise<null> {
        return await this.sendPostAsync("GetSwaggerFromConsul", null, requestModel);
    }
    public async GetRouteFromConsulAsync(requestModel: GetRouteFromConsulModel): Promise<null> {
        return await this.sendPostAsync("GetRouteFromConsul", null, requestModel);
    }
}
const service = new ToolsService("Tools");
export default service;