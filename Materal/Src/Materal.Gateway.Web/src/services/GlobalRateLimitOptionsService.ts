import GlobalRateLimitOptionsModel from "../models/GlobalRateLimitOptionsModel";
import BaseService from "./BaseService";

class GlobalRateLimitOptionsService extends BaseService {
    public async SetConfigAsync(requestModel?: GlobalRateLimitOptionsModel): Promise<null> {
        return await this.sendPostAsync("SetConfig", null, requestModel);
    }
    public async GetConfigAsync(): Promise<GlobalRateLimitOptionsModel | null> {
        return await this.sendGetAsync("GetConfig");
    }
}
const service = new GlobalRateLimitOptionsService("GlobalRateLimitOptions");
export default service;