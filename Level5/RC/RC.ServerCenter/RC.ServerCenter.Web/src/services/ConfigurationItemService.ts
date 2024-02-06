import AddConfigurationItemModel from "../models/configurationItem/AddConfigurationItemModel";
import EditConfigurationItemModel from "../models/configurationItem/EditConfigurationItemModel";
import QueryConfigurationItemModel from "../models/configurationItem/QueryConfigurationItemModel";
import ConfigurationItemDTO from "../models/configurationItem/ConfigurationItemDTO";
import BaseService from "./BaseService";
import SyncConfigRequestModel from "../models/configurationItem/SyncConfigRequestModel";
import serverManagement from "../serverManagement";

class ConfigurationItemService extends BaseService {
    public async AddAsync(requestModel: AddConfigurationItemModel): Promise<string | null> {
        return await this.sendPostAsync("Add", null, requestModel);
    }
    public async EditAsync(requestModel: EditConfigurationItemModel): Promise<null> {
        return await this.sendPutAsync("Edit", null, requestModel);
    }
    public async DeleteAsync(id: string): Promise<null> {
        return await this.sendDeleteAsync("Delete", { id });
    }
    public async GetInfoAsync(id: string): Promise<ConfigurationItemDTO | null> {
        return await this.sendGetAsync("GetInfo", { id });
    }
    public async GetListAsync(requestModel: QueryConfigurationItemModel): Promise<ConfigurationItemDTO[] | null> {
        return await this.sendPostAsync("GetList", null, { ...requestModel, SortPropertyName: "Name", IsAsc: true });
    }
    public async SyncConfigAsync(requestModel: SyncConfigRequestModel): Promise<null> {
        return await this.sendPutAsync("SyncConfig", null, requestModel);
    }
}
const service = new ConfigurationItemService(async () => {
    if (!serverManagement.selectedEnvironmentServer) {
        await serverManagement.initAsync();
    }
    if (!serverManagement.selectedEnvironmentServer) throw new Error("没有选中任何目标");
    return serverManagement.selectedEnvironmentServer.Service;
}, "ConfigurationItem");
export default service;