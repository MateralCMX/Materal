import AddConfigurationItemModel from "../models/configurationItem/AddConfigurationItemModel";
import EditConfigurationItemModel from "../models/configurationItem/EditConfigurationItemModel";
import QueryConfigurationItemModel from "../models/configurationItem/QueryConfigurationItemModel";
import ConfigurationItemDTO from "../models/configurationItem/ConfigurationItemDTO";
import BaseService from "./BaseService";
import SyncConfigRequestModel from "../models/configurationItem/SyncConfigRequestModel";

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
        return await this.sendPostAsync("GetList", null, requestModel);
    }
    public async SyncConfigAsync(requestModel: SyncConfigRequestModel): Promise<null> {
        return await this.sendPutAsync("SyncConfig", null, requestModel);
    }
}
const service = new ConfigurationItemService("RCServerCenterAPI", "ConfigurationItem");
export default service;