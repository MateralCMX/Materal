import { ResultModel } from "./ResultModel";

export class ResultDataModel<T> extends ResultModel {
    /**
     * 数据
     */
    public Data: T = new Object() as T;
}

