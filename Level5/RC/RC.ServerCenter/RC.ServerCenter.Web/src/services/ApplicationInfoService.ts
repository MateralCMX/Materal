import AddApplicationInfoModel from "../models/applicationInfo/AddApplicationInfoModel";
import EditApplicationInfoModel from "../models/applicationInfo/EditApplicationInfoModel";
import QueryApplicationInfoModel from "../models/applicationInfo/QueryApplicationInfoModel";
import ApplicationInfoDTO from "../models/applicationInfo/ApplicationInfoDTO";
import BaseService from "./BaseService";
import serverManagement from "../serverManagement";
import FileInfoDTO from "../models/applicationInfo/FileInfoDTO";

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
    public async ApplyFileAsync(id: string, fileName: string): Promise<null> {
        return await this.sendPutAsync("ApplyFile", { id, fileName }, null);
    }
    public async ApplyLasetFileAsync(id: string): Promise<null> {
        return await this.sendPutAsync("ApplyLasetFile", { id }, null);
    }
    public async ClearConsoleMessagesAsync(id: string): Promise<null> {
        return await this.sendDeleteAsync("ClearConsoleMessages", { id });
    }
    public async DeleteFileAsync(id: string, fileName: string): Promise<null> {
        return await this.sendDeleteAsync("DeleteFile", { id, fileName });
    }
    public async GetConsoleMessagesAsync(id: string): Promise<string[] | null> {
        return await this.sendGetAsync("GetConsoleMessages", { id });
    }
    public async GetUploadFilesAsync(id: string): Promise<FileInfoDTO[] | null> {
        return await this.sendGetAsync("GetUploadFiles", { id });
    }
    public async KillAsync(id: string): Promise<null> {
        return await this.sendPostAsync("Kill", { id }, null);
    }
    public async StartAsync(id: string): Promise<null> {
        return await this.sendPostAsync("Start", { id }, null);
    }
    public async StartAllAsync(): Promise<null> {
        return await this.sendPostAsync("StartAll", null, null);
    }
    public async StopAsync(id: string): Promise<null> {
        return await this.sendPostAsync("Stop", { id }, null);
    }
    public async StopAllAsync(): Promise<null> {
        return await this.sendPostAsync("StopAll", null, null);
    }
    public GetUploadFileUrl(id: string): string {
        if (!serverManagement.selectedDeploy) throw new Error("没有选中任何目标");
        const servicename = serverManagement.selectedDeploy.Service;
        return this.getApiUrlByServiceName("UploadNewFile", servicename, { id });
    }
    public GetGetDownloadUrl(url: string): string {
        if (!serverManagement.selectedDeploy) throw new Error("没有选中任何目标");
        const servicename = serverManagement.selectedDeploy.Service;
        let trueUrl = this.getUrl(servicename);
        trueUrl = `${trueUrl}${url}`;
        return trueUrl;
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