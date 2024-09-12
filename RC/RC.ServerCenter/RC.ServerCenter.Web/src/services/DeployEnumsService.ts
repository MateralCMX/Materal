import KeyValueModel from "../models/KeyValueModel";
import BaseService from "./BaseService";
import serverManagement from "../serverManagement";

class DeployEnumsService extends BaseService {
    public async GetAllApplicationStatusEnumAsync(): Promise<KeyValueModel[] | null> {
        return await this.sendGetAsync("GetAllApplicationStatusEnum", null);
    }
    public async GetAllApplicationTypeEnumAsync(): Promise<KeyValueModel[] | null> {
        return await this.sendGetAsync("GetAllApplicationTypeEnum", null);
    }
}
const service = new DeployEnumsService(async () => {
    if (!serverManagement.selectedDeploy) {
        await serverManagement.initAsync();
    }
    if (!serverManagement.selectedDeploy) throw new Error("没有选中任何目标");
    return serverManagement.selectedDeploy.Service;
}, "Enums");
export default service;