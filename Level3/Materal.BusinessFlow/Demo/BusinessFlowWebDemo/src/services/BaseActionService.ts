import { BaseDomain } from '../models/BaseDomain';
import { PageRequestModel } from '../models/PageRequestModel';
import { PageResultModel } from '../models/PageResultModel';
import { ResultDataModel } from '../models/ResultDataModel';
import { ResultModel } from '../models/ResultModel';
import { BaseService } from './BaseService';

export abstract class BaseActionService<TDomain extends BaseDomain, TQueryModel extends PageRequestModel, TAddModel, TEditModel> extends BaseService {
    protected controllerName: string;
    constructor(controllerName: string) {
        super();
        this.controllerName = controllerName;
    }
    /**
     * 添加
     */
    public async AddAsync(model: TAddModel): Promise<ResultDataModel<string> | undefined> {
        const result = await this.SendPutAsync<ResultDataModel<string>>(`/${this.controllerName}/Add`, undefined, model);
        return result;
    }
    /**
     * 修改
     */
    public async EditAsync(model: TEditModel): Promise<ResultModel | undefined> {
        const result = await this.SendPostAsync<ResultModel>(`/${this.controllerName}/Edit`, undefined, model);
        return result;
    }
    /**
     * 删除
     */
    public async DeleteAsync(id: string): Promise<ResultModel | undefined> {
        const urlParams = {
            "id": id
        };
        const result = await this.SendDeleteAsync<ResultModel>(`/${this.controllerName}/Delete`, urlParams);
        return result;
    }
    /**
     * 获得信息
     */
    public async GetInfoAsync(id: string): Promise<ResultDataModel<TDomain> | undefined> {
        const urlParams = {
            "id": id
        };
        const result = await this.SendGetAsync<ResultDataModel<TDomain>>(`/${this.controllerName}/GetInfo`, urlParams);
        return result;
    }
    /**
     * 获得列表
     */
    public async GetListAsync(queryModel: TQueryModel): Promise<PageResultModel<TDomain> | undefined> {
        const result = await this.SendPostAsync<PageResultModel<TDomain>>(`/${this.controllerName}/GetList`, undefined, queryModel);
        return result;
    }
    /**
     * 获得所有列表
     */
    public async GetAllListAsync(queryModel: TQueryModel): Promise<ResultDataModel<TDomain[]> | undefined> {
        const result = await this.SendPostAsync<ResultDataModel<TDomain[]>>(`/${this.controllerName}/GetAllList`, undefined, queryModel);
        return result;
    }
}
