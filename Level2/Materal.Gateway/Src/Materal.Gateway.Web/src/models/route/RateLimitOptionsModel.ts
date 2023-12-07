export default interface RateLimitOptionsModel {
    /**
     * 是否启用限流
     */
    EnableRateLimiting: boolean;
    /**
     * 限流配置
     */
    Period: string;
    /**
     * 统计时间内允许请求的最大次数
     */
    Limit: number;
    /**
     * 限流后重试时间(s)
     */
    PeriodTimespan:number;
    /**
     * 白名单
     */
    ClientWhitelist: string[];
}