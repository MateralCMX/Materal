import AddApplicationInfoModel from "../models/applicationInfo/AddApplicationInfoModel";
import EditApplicationInfoModel from "../models/applicationInfo/EditApplicationInfoModel";
import QueryApplicationInfoModel from "../models/applicationInfo/QueryApplicationInfoModel";
import ApplicationInfoDTO from "../models/applicationInfo/ApplicationInfoDTO";
import BaseService from "./BaseService";
import serverManagement from "../serverManagement";

class ApplicationInfoService extends BaseService {
    public async AddAsync(requestModel: AddApplicationInfoModel): Promise<string | null> {
        return await this.sendPostAsync("Add", null, requestModel);
    }
    public async EditAsync(requestModel: EditApplicationInfoModel): Promise<null> {
        return await this.sendPutAsync("Edit", null, requestModel);
    }
    public async DeleteAsync(id: string): Promise<null> {
        return await this.sendDeleteAsync("Delete", { id });
    }
    public async GetInfoAsync(id: string): Promise<ApplicationInfoDTO | null> {
        return await this.sendGetAsync("GetInfo", { id });
    }
    public async GetListAsync(requestModel: QueryApplicationInfoModel): Promise<ApplicationInfoDTO[] | null> {
        return await this.sendPostAsync("GetList", null, requestModel);
    }
}
const service = new ApplicationInfoService(async () => {
    if (!serverManagement.selectedDeploy) {
        await serverManagement.initAsync();
    }
    if (!serverManagement.selectedDeploy) throw new Error("没有选中任何目标");
    return serverManagement.selectedDeploy.Service;
}, "ApplicationInfo");
export default service;