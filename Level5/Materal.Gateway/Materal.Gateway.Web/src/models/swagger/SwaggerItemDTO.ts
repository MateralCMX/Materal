export default interface SwaggerItemDTO {
    /**
     * 唯一标识
     */
    ID: string;
    /**
     * 名称
     */
    Name: string;
    /**
     * 版本
     */
    Version: string;
    /**
     * 服务名称
     */
    ServiceName: string;
    /**
     * 服务路径
     */
    ServicePath: string;
    /**
     * SwaggerJson文件地址
     */
    Url: string;
}