import AddDefaultDataModel from "../models/defaultData/AddDefaultDataModel";
import EditDefaultDataModel from "../models/defaultData/EditDefaultDataModel";
import QueryDefaultDataModel from "../models/defaultData/QueryDefaultDataModel";
import DefaultDataDTO from "../models/defaultData/DefaultDataDTO";
import BaseService from "./BaseService";
import serverManagement from "../serverManagement";

class DefaultDataService extends BaseService {
    public async AddAsync(requestModel: AddDefaultDataModel): Promise<string | null> {
        return await this.sendPostAsync("Add", null, requestModel);
    }
    public async EditAsync(requestModel: EditDefaultDataModel): Promise<null> {
        return await this.sendPutAsync("Edit", null, requestModel);
    }
    public async DeleteAsync(id: string): Promise<null> {
        return await this.sendDeleteAsync("Delete", { id });
    }
    public async GetInfoAsync(id: string): Promise<DefaultDataDTO | null> {
        return await this.sendGetAsync("GetInfo", { id });
    }
    public async GetListAsync(requestModel: QueryDefaultDataModel): Promise<DefaultDataDTO[] | null> {
        return await this.sendPostAsync("GetList", null, requestModel);
    }
}
const service = new DefaultDataService(async () => {
    if (!serverManagement.selectedDeploy) {
        await serverManagement.initAsync();
    }
    if (!serverManagement.selectedDeploy) throw new Error("没有选中任何目标");
    return serverManagement.selectedDeploy.Service;
}, "DefaultData");
export default service;