import AddRouteModel from "../models/route/AddRouteModel";
import EditRouteModel from "../models/route/EditRouteModel";
import QueryRouteModel from "../models/route/QueryRouteModel";
import RouteDTO from "../models/route/RouteDTO";
import BaseService from "./BaseService";

class RouteService extends BaseService {
    public async AddAsync(requestModel: AddRouteModel): Promise<string | null> {
        return await this.sendPutAsync("Add", null, requestModel);
    }
    public async EditAsync(requestModel: EditRouteModel): Promise<null> {
        return await this.sendPostAsync("Edit", null, requestModel);
    }
    public async DeleteAsync(id: string): Promise<null> {
        return await this.sendDeleteAsync("Delete", { id });
    }
    public async GetInfoAsync(id: string): Promise<RouteDTO | null> {
        return await this.sendGetAsync("GetInfo", { id });
    }
    public async GetListAsync(requestModel: QueryRouteModel): Promise<Array<RouteDTO> | null> {
        return await this.sendPostAsync("GetList", null, requestModel);
    }
    public async moveUpAsync(id: string): Promise<null> {
        return await this.sendPostAsync("MoveUp", { id });
    }
    public async moveNextAsync(id: string): Promise<null> {
        return await this.sendPostAsync("MoveNext", { id });
    }
}
const service = new RouteService("Route");
export default service;