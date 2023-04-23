import TimeSpan from "@web-atoms/date-time/dist/TimeSpan";
import { TimeSpanHelper } from "../TimeSpanHelper";
import { IStepData } from "./Base/IStepData";
import { StepData } from "./Base/StepData";

export class DelayStepData extends StepData {
    /**
     * 延时时间
     */
    private Delay: string;
    public get DelayValue(): TimeSpan {
        return TimeSpanHelper.StringToTimeSpan(this.Delay);
    }
    public set DelayValue(value: TimeSpan) {
        this.Delay = TimeSpanHelper.TimeSpanToString(value);
    }
    public Next?: IStepData;
    constructor(id: string) {
        super(`${DelayStepData.name}`, "延时节点", id);
        this.Delay = "0:0:0";
    }
}