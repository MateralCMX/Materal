import LoginModel from "../models/user/LoginModel";
import LoginResultDTO from "../models/user/LoginResultDTO";
import AddUserModel from "../models/user/AddUserModel";
import EditUserModel from "../models/user/EditUserModel";
import QueryUserModel from "../models/user/QueryUserModel";
import UserDTO from "../models/user/UserDTO";
import BaseService from "./BaseService";
import ChangePasswordModel from "../models/user/ChangePasswordModel";

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
    public async LoginAsync(requestModel: LoginModel): Promise<LoginResultDTO | null> {
        return await this.sendPostAsync("Login", null, requestModel);
    }
    public async ChangePasswordAsync(requestModel: ChangePasswordModel): Promise<string | null> {
        return await this.sendPutAsync("ChangePassword", null, requestModel);
    }
    public async ResetPasswordAsync(id: string): Promise<string | null> {
        return await this.sendPostAsync("ResetPassword", { id }, null);
    }
}
const service = new UserService(async () => "RCAuthority", "User");
export default service;