import axios from 'axios';
import { ResultModel } from '../models/ResultModel';
import { message } from 'ant-design-vue';
import { ResultTypeEnum } from '../models/ResultTypeEnum';

export abstract class BaseService {
    /**
     * API基础地址
     */
    protected BaseUrl: string = "http://localhost:5000/bfapi";
    /**
     * 发送Get请求
     */
    protected async SendGetAsync<T extends ResultModel>(url: string, urlParams?: any): Promise<T | undefined> {
        try {
            let trueUrl: string = this.GetTrueUrl(url, urlParams);
            const response = await axios.get(trueUrl);
            const data: T = response.data;
            return this.HandleResult(data);
        } catch (error) {
            this.HandleError(error as Error);
        }
    }
    /**
     * 发送Post请求
     */
    protected async SendPostAsync<T extends ResultModel>(url: string, urlParams?: any, bodyParams?: any): Promise<T | undefined> {
        try {
            let trueUrl: string = this.GetTrueUrl(url, urlParams);
            const response = await axios.post(trueUrl, bodyParams);
            const data: T = response.data;
            return this.HandleResult(data);
        } catch (error) {
            this.HandleError(error as Error);
        }
    }
    /**
     * 发送Put请求
     */
    protected async SendPutAsync<T extends ResultModel>(url: string, urlParams?: any, bodyParams?: any): Promise<T | undefined> {
        try {
            let trueUrl: string = this.GetTrueUrl(url, urlParams);
            const response = await axios.put(trueUrl, bodyParams);
            const data: T = response.data;
            return this.HandleResult(data);
        } catch (error) {
            this.HandleError(error as Error);
        }
    }
    /**
     * 发送Delete请求
     */
    protected async SendDeleteAsync<T extends ResultModel>(url: string, urlParams?: any): Promise<T | undefined> {
        try {
            let trueUrl: string = this.GetTrueUrl(url, urlParams);
            const response = await axios.delete(trueUrl);
            const data: T = response.data;
            return this.HandleResult(data);
        } catch (error) {
            this.HandleError(error as Error);
        }
    }
    /**
     * 处理错误
     */
    private HandleError(error: Error) {
        message.error(error.message);
    }
    /**
     * 处理返回
     */
    private HandleResult<T extends ResultModel>(result: T): T {
        if (result.ResultType == ResultTypeEnum.Fail) {
            message.error(result.Message);
        }
        return result;
    }
    /**
     * 获得真实的Url
     */
    private GetTrueUrl(url: string, urlParams?: any) {
        let trueUrl: string = `${this.BaseUrl}${url}`;
        if (urlParams) {
            trueUrl += "?";
            for (const key in urlParams) {
                if (!Object.prototype.hasOwnProperty.call(urlParams, key)) continue;
                const element = urlParams[key];
                trueUrl += `${key}=${element}&`;
            }
        }
        return trueUrl;
    }
}
