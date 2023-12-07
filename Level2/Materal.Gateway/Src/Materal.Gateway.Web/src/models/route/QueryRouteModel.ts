export default interface QueryRouteModel {
    /**
     * 上游路径模版
     */
    UpstreamPathTemplate?: string;
    /**
     * 下游路径模版
     */
    DownstreamPathTemplate?: string;
    /**
     * 服务名称(服务发现)
     */
    ServiceName?: string;
    /**
     * Swagger标识
     */
    SwaggerKey?: string;
    /**
     * 启用缓存
     */
    EnableCache?: boolean;
}