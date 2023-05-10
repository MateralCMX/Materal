import { ResultTypeEnum } from "./ResultTypeEnum";

export class ResultModel {
    /**
     * 消息
     */
    public Message?: string;
    /**
     * 结果类型
     */
    public ResultType: ResultTypeEnum = ResultTypeEnum.Success;
}
