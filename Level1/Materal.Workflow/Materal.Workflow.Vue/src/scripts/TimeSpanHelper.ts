import TimeSpan from "@web-atoms/date-time/dist/TimeSpan";

export class TimeSpanHelper {
    /**
     * TimeSpan转换为string
     * @param value 0.00:00:00
     * @returns 
     */
    public static TimeSpanToString(value?: TimeSpan): string {
        if (!value) return "0.00:00:00";
        const days = value.days.toString();
        const hours = value.hours.toString().padStart(2, "0");
        const minutes = value.minutes.toString().padStart(2, "0");
        const seconds = value.seconds.toString().padStart(2, "0");
        return `${days}.${hours}:${minutes}:${seconds}`;
    }
    /**
     * string转换为TimeSpan
     * @param value 0.00:00:00
     * @returns 
     */
    public static StringToTimeSpan(value?: string): TimeSpan {
        if (!value) return new TimeSpan(0, 0, 0);
        return TimeSpan.parse(value);
    }
}