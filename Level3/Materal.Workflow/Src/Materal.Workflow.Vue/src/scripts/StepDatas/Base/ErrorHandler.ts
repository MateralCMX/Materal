import { ErrorHandlerTypeEnum } from "./ErrorHandlerTypeEnum";
import TimeSpan from "@web-atoms/date-time/dist/TimeSpan";
import { TimeSpanHelper } from "../../TimeSpanHelper";

/**
 * 异常处理
 */
export class ErrorHandler {
    /**
     * 错误处理类型
     */
    public HandlerType: ErrorHandlerTypeEnum = ErrorHandlerTypeEnum.Stop;
    /**
     * 重试间隔
     */
    private RetryInterval?: string;
    public get RetryIntervalValue(): TimeSpan {
        return TimeSpanHelper.StringToTimeSpan(this.RetryInterval);
    }
    public set RetryIntervalValue(value: TimeSpan) {
        this.RetryInterval = TimeSpanHelper.TimeSpanToString(value);
    }
    constructor(josnValue?: string) {
        if (!josnValue) return;
        let model: ErrorHandler = JSON.parse(josnValue);
        this.HandlerType = model.HandlerType;
        this.RetryInterval = model.RetryInterval;
    }
}
