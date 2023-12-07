import AuthenticationOptionsModel from "./AuthenticationOptionsModel";
import FileCacheOptionsModel from "./FileCacheOptionsModel";
import HostAndPortModel from "./HostAndPortModel";
import LoadBalancerOptionsModel from "./LoadBalancerOptionsModel";
import QoSOptionsModel from "./QoSOptionsModel";
import RateLimitOptionsModel from "./RateLimitOptionsModel";

export default interface RouteDTO {
    /**
     * 唯一标识
     */
    ID: string;
    /**
     * 上游路径模版
     */
    UpstreamPathTemplate: string;
    /**
     * 上游Http方法
     */
    UpstreamHttpMethod: string[];
    /**
     * 下游路径模版
     */
    DownstreamPathTemplate: string;
    /**
     * 下游方案
     */
    DownstreamScheme: string;
    /**
     * 下游Http版本
     */
    DownstreamHttpVersion: string;
    /**
     * 缓存
     */
    FileCacheOptions?: FileCacheOptionsModel;
    /**
     * 服务名称(服务发现)
     */
    ServiceName?: string;
    /**
     * 负载均衡
     */
    LoadBalancerOptions?: LoadBalancerOptionsModel;
    /**
     * 下游主机和端口
     */
    DownstreamHostAndPorts?: Array<HostAndPortModel>;
    /**
     * 服务质量
     */
    QoSOptions?: QoSOptionsModel;
    /**
     * 身份认证
     */
    AuthenticationOptions?: AuthenticationOptionsModel;
    /**
     * 限流配置
     */
    RateLimitOptions?: RateLimitOptionsModel;
    /**
     * 忽略证书错误
     */
    DangerousAcceptAnyServerCertificateValidator: boolean;
    /**
     * Swagger标识
     */
    SwaggerKey?: string;
}