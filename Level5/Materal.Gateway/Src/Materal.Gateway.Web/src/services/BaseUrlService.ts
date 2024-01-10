import BaseService from "./BaseService";

class BaseUrlService extends BaseService {
    public async SetConfigAsync(baseUrl?: string ): Promise<null> {
        return await this.sendPostAsync("SetConfig", { baseUrl });
    }
    public async GetConfigAsync(): Promise<string | null> {
        return await this.sendGetAsync("GetConfig");
    }
}
const service = new BaseUrlService("BaseUrl");
export default service;