import AddUserModel from "./AddUserModel";

export default interface EditUserModel extends AddUserModel {
    /**
     * 唯一标识
     */
    ID: string;
}