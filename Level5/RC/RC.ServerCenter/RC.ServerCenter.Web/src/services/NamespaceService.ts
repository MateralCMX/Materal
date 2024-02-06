import AddNamespaceModel from "../models/namespace/AddNamespaceModel";
import EditNamespaceModel from "../models/namespace/EditNamespaceModel";
import QueryNamespaceModel from "../models/namespace/QueryNamespaceModel";
import NamespaceDTO from "../models/namespace/NamespaceDTO";
import BaseService from "./BaseService";

class NamespaceService extends BaseService {
    public async AddAsync(requestModel: AddNamespaceModel): Promise<string | null> {
        return await this.sendPostAsync("Add", null, requestModel);
    }
    public async EditAsync(requestModel: EditNamespaceModel): Promise<null> {
        return await this.sendPutAsync("Edit", null, requestModel);
    }
    public async DeleteAsync(id: string): Promise<null> {
        return await this.sendDeleteAsync("Delete", { id });
    }
    public async GetInfoAsync(id: string): Promise<NamespaceDTO | null> {
        return await this.sendGetAsync("GetInfo", { id });
    }
    public async GetListAsync(requestModel: QueryNamespaceModel): Promise<NamespaceDTO[] | null> {
        return await this.sendPostAsync("GetList", null, { ...requestModel, SortPropertyName: "Name", IsAsc: true });
    }
}
const service = new NamespaceService(async () => "RCServerCenterAPI", "Namespace");
export default service;