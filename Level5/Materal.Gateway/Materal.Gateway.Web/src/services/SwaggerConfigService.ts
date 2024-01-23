import AddSwaggerModel from "../models/swagger/AddSwaggerModel";
import AddSwaggerServiceItemModel from "../models/swagger/AddSwaggerServiceItemModel";
import AddSwaggerJsonItemModel from "../models/swagger/AddSwaggerJsonItemModel";
import EditSwaggerModel from "../models/swagger/EditSwaggerModel";
import QuerySwaggerModel from "../models/swagger/QuerySwaggerModel";
import SwaggerDTO from "../models/swagger/SwaggerDTO";
import BaseService from "./BaseService";
import EditSwaggerJsonItemModel from "../models/swagger/EditSwaggerJsonItemModel";
import EditSwaggerServiceItemModel from "../models/swagger/EditSwaggerServiceItemModel";
import SwaggerItemDTO from "../models/swagger/SwaggerItemDTO";
import QuerySwaggerItemModel from "../models/swagger/QuerySwaggerItemModel";

class SwaggerConfigService extends BaseService {
    public async AddAsync(requestModel: AddSwaggerModel): Promise<string | null> {
        return await this.sendPutAsync("Add", null, requestModel);
    }
    public async EditAsync(requestModel: EditSwaggerModel): Promise<null> {
        return await this.sendPostAsync("Edit", null, requestModel);
    }
    public async DeleteAsync(id: string): Promise<null> {
        return await this.sendDeleteAsync("Delete", { id });
    }
    public async GetInfoAsync(id: string): Promise<SwaggerDTO | null> {
        return await this.sendGetAsync("GetInfo", { id });
    }
    public async GetListAsync(requestModel: QuerySwaggerModel): Promise<Array<SwaggerDTO> | null> {
        return await this.sendPostAsync("GetList", null, requestModel);
    }
    public async AddServiceItemAsync(requestModel: AddSwaggerServiceItemModel): Promise<string | null> {
        return await this.sendPutAsync("AddServiceItem", null, requestModel);
    }
    public async AddJsonItemAsync(requestModel: AddSwaggerJsonItemModel): Promise<string | null> {
        return await this.sendPutAsync("AddJsonItem", null, requestModel);
    }
    public async EditServiceItemAsync(requestModel: EditSwaggerServiceItemModel): Promise<null> {
        return await this.sendPostAsync("EditServiceItem", null, requestModel);
    }
    public async EditJsonItemAsync(requestModel: EditSwaggerJsonItemModel): Promise<null> {
        return await this.sendPostAsync("EditJsonItem", null, requestModel);
    }
    public async DeleteItemAsync(swaggerConfigID: string, id: string): Promise<null> {
        return await this.sendDeleteAsync("DeleteItem", { swaggerConfigID, id });
    }
    public async GetItemInfoAsync(swaggerConfigID: string, id: string): Promise<SwaggerItemDTO | null> {
        return await this.sendGetAsync("GetItemInfo", { swaggerConfigID, id });
    }
    public async GetItemListAsync(requestModel: QuerySwaggerItemModel): Promise<Array<SwaggerItemDTO> | null> {
        return await this.sendPostAsync("GetItemList", null, requestModel);
    }
}
const service = new SwaggerConfigService("SwaggerConfig");
export default service;