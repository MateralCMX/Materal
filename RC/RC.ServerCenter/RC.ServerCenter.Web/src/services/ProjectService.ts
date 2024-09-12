import AddProjectModel from "../models/project/AddProjectModel";
import EditProjectModel from "../models/project/EditProjectModel";
import QueryProjectModel from "../models/project/QueryProjectModel";
import ProjectDTO from "../models/project/ProjectDTO";
import BaseService from "./BaseService";

class ProjectService extends BaseService {
    public async AddAsync(requestModel: AddProjectModel): Promise<string | null> {
        return await this.sendPostAsync("Add", null, requestModel);
    }
    public async EditAsync(requestModel: EditProjectModel): Promise<null> {
        return await this.sendPutAsync("Edit", null, requestModel);
    }
    public async DeleteAsync(id: string): Promise<null> {
        return await this.sendDeleteAsync("Delete", { id });
    }
    public async GetInfoAsync(id: string): Promise<ProjectDTO | null> {
        return await this.sendGetAsync("GetInfo", { id });
    }
    public async GetListAsync(requestModel: QueryProjectModel): Promise<ProjectDTO[] | null> {
        return await this.sendPostAsync("GetList", null, { ...requestModel, SortPropertyName: "Name", IsAsc: true });
    }
}
const service = new ProjectService(async () => "RCServerCenter", "Project");
export default service;