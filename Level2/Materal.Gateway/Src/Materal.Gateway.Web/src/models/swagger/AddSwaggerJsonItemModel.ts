import AddSwaggerItemConfigModel from "./AddSwaggerItemConfigModel";

export default interface AddSwaggerJsonItemModel extends AddSwaggerItemConfigModel {
    /**
     * SwaggerJson文件地址
     */
    Url: string;
}