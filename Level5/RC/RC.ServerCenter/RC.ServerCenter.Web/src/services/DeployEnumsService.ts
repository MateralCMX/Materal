import KeyValueModel from "../models/KeyValueModel";
import BaseService from "./BaseService";

class DeployEnumsService extends BaseService {
    public async GetAllApplicationStatusEnumAsync(): Promise<KeyValueModel[] | null> {
        return await this.sendGetAsync("GetAllApplicationStatusEnum", null);
    }
    public async GetAllApplicationTypeEnumAsync(): Promise<KeyValueModel[] | null> {
        return await this.sendGetAsync("GetAllApplicationTypeEnum", null);
    }
}
const service = new DeployEnumsService("", "Enum");
export default service;