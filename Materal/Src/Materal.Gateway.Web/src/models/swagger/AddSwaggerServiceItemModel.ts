import AddSwaggerItemConfigModel from "./AddSwaggerItemConfigModel";

export default interface AddSwaggerServiceItemModel extends AddSwaggerItemConfigModel {
    /**
     * 服务名称
     */
    ServiceName: string;
    /**
     * 服务路径
     */
    ServicePath: string;
}