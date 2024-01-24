import { Message } from '@arco-design/web-vue';
import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import config from "../config";
import loginManagement from "../loginManagement";

export default abstract class BaseService {
    protected controllerName: string;
    public serviceName: string;
    constructor(serviceName:string, controllerName: string) {
        this.controllerName = controllerName;
        this.serviceName = serviceName;
    }
    protected async sendGetAsync<T>(url: string, querydata?: any): Promise<T | null> {
        try {
            let trueUrl = this.getTrueUrl(url, querydata);
            const httpResult = await axios.get(trueUrl, this.getAxiosRequestConfig());
            return this.handlerHttpResult(httpResult);
        } catch (error) {
            this.handlerHttpError(error as AxiosError);
            throw error;
        }
    }
    protected async sendDeleteAsync<T>(url: string, querydata?: any): Promise<T | null> {
        try {
            let trueUrl = this.getTrueUrl(url, querydata);
            const httpResult = await axios.delete(trueUrl, this.getAxiosRequestConfig());
            return this.handlerHttpResult(httpResult);
        } catch (error) {
            this.handlerHttpError(error as AxiosError);
            throw error;
        }
    }
    protected async sendPostAsync<T>(url: string, querydata?: any, data?: any): Promise<T | null> {
        try {
            const trueUrl = this.getTrueUrl(url, querydata);
            const httpResult = await axios.post(trueUrl, data, this.getAxiosRequestConfig());
            return this.handlerHttpResult(httpResult);
        } catch (error) {
            this.handlerHttpError(error as AxiosError);
            throw error;
        }
    }
    protected async sendPutAsync<T>(url: string, querydata?: any, data?: any): Promise<T | null> {
        try {
            const trueUrl = this.getTrueUrl(url, querydata);
            const httpResult = await axios.put(trueUrl, data, this.getAxiosRequestConfig());
            return this.handlerHttpResult(httpResult);
        } catch (error) {
            this.handlerHttpError(error as AxiosError);
            throw error;
        }
    }
    private getTrueUrl(url: string, data?: any): string {
        if (url.startsWith('/')) {
            url = url.substring(1);
        }
        let trueUrl = `${config.baseUrl}/${this.serviceName}/api/${this.controllerName}/${url}`;
        if (data) {
            trueUrl += '?';
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    const value = data[key];
                    if (value) {
                        trueUrl += `${key}=${value}&`;
                    }
                }
            }
        }
        return trueUrl;
    }
    private handlerHttpResult<T>(httpResult: AxiosResponse<any, any>): T | null {
        if (httpResult.data.ResultType == 0) {
            return httpResult.data.Data ?? null;
        }
        else {
            Message.warning(httpResult.data.Message);
            return null;
        }
    }
    private getAxiosRequestConfig(): AxiosRequestConfig {
        const axiosConfig: AxiosRequestConfig = {
            headers: {
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*"
            }
        };
        const token = loginManagement.getToken();
        if (axiosConfig.headers && token) {
            axiosConfig.headers.Authorization = `Bearer ${token}`;
        }
        return axiosConfig;
    }
    private handlerHttpError(error: AxiosError) {
        if (!error.response) return;
        if (error.response.status == 401) {
            window.location.hash = "/Login";
        }
    }
}