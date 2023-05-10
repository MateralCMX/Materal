import { PageRequestModel } from "../PageRequestModel";

export class QueryUserModel extends PageRequestModel {
    /**
     * 唯一标识
     */
    public ID?: string;
    /**
     * 名称
     */
    public Name?: string;
}
