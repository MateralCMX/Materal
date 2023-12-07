export default interface QoSOptionsModel {
    /**
     * 熔断时间
     */
    DurationOfBreak: number;
    /**
     * 重试次数
     */
    TimeoutValue: number;
    /**
     * 熔断前允许错误次数
     */
    ExceptionsAllowedBeforeBreaking: number;
}