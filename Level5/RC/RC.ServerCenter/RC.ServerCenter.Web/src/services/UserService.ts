import LoginRequestModel from "../models/user/LoginRequestModel";
import LoginResultDTO from "../models/user/LoginResultDTO";
import AddUserModel from "../models/user/AddUserModel";
import EditUserModel from "../models/user/EditUserModel";
import QueryUserModel from "../models/user/QueryUserModel";
import UserDTO from "../models/user/UserDTO";
import BaseService from "./BaseService";

class UserService extends BaseService {
    public async AddAsync(requestModel: AddUserModel): Promise<string | null> {
        return await this.sendPostAsync("Add", null, requestModel);
    }
    public async EditAsync(requestModel: EditUserModel): Promise<null> {
        return await this.sendPutAsync("Edit", null, requestModel);
    }
    public async DeleteAsync(id: string): Promise<null> {
        return await this.sendDeleteAsync("Delete", { id });
    }
    public async GetInfoAsync(id: string): Promise<UserDTO | null> {
        return await this.sendGetAsync("GetInfo", { id });
    }
    public async GetListAsync(requestModel: QueryUserModel): Promise<UserDTO[] | null> {
        return await this.sendPostAsync("GetList", null, requestModel);
    }
    public async LoginAsync(requestModel: LoginRequestModel): Promise<LoginResultDTO | null> {
        return await this.sendPostAsync("Login", null, requestModel);
    }
    public async ResetPasswordAsync(id: string): Promise<string | null> {
        return await this.sendPutAsync("ResetPassword", { id }, null);
    }
}
const service = new UserService("RCAuthorityAPI", "User");
export default service;