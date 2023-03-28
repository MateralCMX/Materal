import TimeSpan from "@web-atoms/date-time/dist/TimeSpan";
import { TimeSpanHelper } from "../TimeSpanHelper";
import { IStepData } from "./Base/IStepData";
import { StepData } from "./Base/StepData";

export class ScheduleStepData extends StepData {
    /**
     * 计划时间
     */
    private Delay: string;
    public get DelayValue(): TimeSpan {
        return TimeSpanHelper.StringToTimeSpan(this.Delay);
    }
    public set DelayValue(value: TimeSpan) {
        this.Delay = TimeSpanHelper.TimeSpanToString(value);
    }
    /**
     * 计划节点
     */
    public StepData?: IStepData;
    /**
     * 下一步
     */
    public Next?: IStepData;
    constructor() {
        super(`${ScheduleStepData.name}`, "计划节点");
        this.Delay = "0:0:0";
    }
}