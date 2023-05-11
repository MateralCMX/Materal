import { BaseDomain } from "../BaseDomain";

export class DataModel extends BaseDomain {
    /**
     * 名称
     */
    public Name: string = "";
    /**
     * 描述
     */
    public Description?: string;
}