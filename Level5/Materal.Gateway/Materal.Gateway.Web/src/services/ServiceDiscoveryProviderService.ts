import ServiceDiscoveryProviderModel from "../models/ServiceDiscoveryProviderModel";
import BaseService from "./BaseService";

class ServiceDiscoveryProviderService extends BaseService {
    public async SetConfigAsync(requestModel?: ServiceDiscoveryProviderModel): Promise<null> {
        return await this.sendPostAsync("SetConfig", null, requestModel);
    }
    public async GetConfigAsync(): Promise<ServiceDiscoveryProviderModel | null> {
        return await this.sendGetAsync("GetConfig");
    }
}
const service = new ServiceDiscoveryProviderService("ServiceDiscoveryProvider");
export default service;