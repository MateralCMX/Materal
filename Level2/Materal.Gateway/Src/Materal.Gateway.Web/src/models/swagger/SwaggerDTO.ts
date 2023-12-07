import SwaggerItemDTO from "./SwaggerItemDTO";

export default interface SwaggerDTO {
    /**
     * 唯一标识
     */
    ID: string;
    /**
     * SwaggerKey
     */
    Key: string;
    /**
     * 服务发现
     */
    TakeServersFromDownstreamService: boolean;
    /**
     * 项
     */
    Items: SwaggerItemDTO[];
}