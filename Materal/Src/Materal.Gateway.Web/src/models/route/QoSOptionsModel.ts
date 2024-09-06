export default interface QoSOptionsModel {
    /**
     * 熔断时间(ms)
     */
    DurationOfBreak: number;
    /**
     * 超时时间(ms)
     */
    TimeoutValue: number;
    /**
     * 熔断前允许错误次数
     */
    ExceptionsAllowedBeforeBreaking: number;
}