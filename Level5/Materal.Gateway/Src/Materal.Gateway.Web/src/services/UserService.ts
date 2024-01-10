import LoginRequestModel from "../models/LoginRequestModel";
import LoginResultDTO from "../models/LoginResultDTO";
import BaseService from "./BaseService";

class UserService extends BaseService {
    public async loginAsync(requestModel: LoginRequestModel): Promise<LoginResultDTO | null> {
        return await this.sendPostAsync("Login", null, requestModel);
    }
}
const service = new UserService("GatewayUser");
export default service;