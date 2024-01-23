import DeployDTO from "../models/server/DeployDTO";
import EnvironmentServerDTO from "../models/server/EnvironmentServerDTO";
import BaseService from "./BaseService";

class ServerService extends BaseService {
    public async GetDeployListAsync(): Promise<DeployDTO[] | null> {
        return await this.sendGetAsync("GetDeployList", null);
    }
    public async GetEnvironmentServerListAsync(): Promise<EnvironmentServerDTO[] | null> {
        return await this.sendGetAsync("GetEnvironmentServerList", null);
    }
}
const service = new ServerService("RCServerCenterAPI", "Server");
export default service;