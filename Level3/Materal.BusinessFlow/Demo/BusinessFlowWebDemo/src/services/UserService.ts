import { AddUserModel } from "../models/User/AddUserModel";
import { EditUserModel } from "../models/User/EditUserModel";
import { QueryUserModel } from "../models/User/QueryUserModel";
import { User } from "../models/User/User";
import { BaseActionService } from "./BaseActionService";

class UserService extends BaseActionService<User, QueryUserModel, AddUserModel, EditUserModel> {
    constructor() {
        super("User");
    }
}
export default new UserService();