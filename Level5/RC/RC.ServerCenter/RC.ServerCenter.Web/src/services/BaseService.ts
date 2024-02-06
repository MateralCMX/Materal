import { Message } from '@arco-design/web-vue';
import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import loginManagement from "../loginManagement";

export default abstract class BaseService {
    protected controllerName: string;
    public baseUrl: string = location.origin;
    public getServiceNameAsync: () => Promise<string>;
    constructor(serviceName: () => Promise<string>, controllerName: string) {
        this.controllerName = controllerName;
        this.getServiceNameAsync = serviceName;
    }
    protected async sendGetAsync<T>(url: string, querydata?: any): Promise<T | null> {
        try {
            const trueUrl = await this.getApiUrlAsync(url, querydata);
            const httpResult = await axios.get(trueUrl, this.getAxiosRequestConfig());
            return this.handlerHttpResult(httpResult);
        } catch (error) {
            this.handlerHttpError(error as AxiosError);
            throw error;
        }
    }
    protected async sendDeleteAsync<T>(url: string, querydata?: any): Promise<T | null> {
        try {
            const trueUrl = await this.getApiUrlAsync(url, querydata);
            const httpResult = await axios.delete(trueUrl, this.getAxiosRequestConfig());
            return this.handlerHttpResult(httpResult);
        } catch (error) {
            this.handlerHttpError(error as AxiosError);
            throw error;
        }
    }
    protected async sendPostAsync<T>(url: string, querydata?: any, data?: any): Promise<T | null> {
        try {
            const trueUrl = await this.getApiUrlAsync(url, querydata);
            const httpResult = await axios.post(trueUrl, data, this.getAxiosRequestConfig());
            return this.handlerHttpResult(httpResult);
        } catch (error) {
            this.handlerHttpError(error as AxiosError);
            throw error;
        }
    }
    protected async sendPutAsync<T>(url: string, querydata?: any, data?: any): Promise<T | null> {
        try {
            const trueUrl = await this.getApiUrlAsync(url, querydata);
            const httpResult = await axios.put(trueUrl, data, this.getAxiosRequestConfig());
            return this.handlerHttpResult(httpResult);
        } catch (error) {
            this.handlerHttpError(error as AxiosError);
            throw error;
        }
    }
    protected async getApiUrlByServiceNameAsync(url: string, serviceName: string, data?: any): Promise<string> {
        if (url.startsWith('/')) {
            url = url.substring(1);
        }
        let trueUrl = await this.getUrlAsync(serviceName);
        trueUrl = `${trueUrl}/api/${this.controllerName}/${url}`;
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
    protected async getApiUrlAsync(url: string, data?: any): Promise<string> {
        const serviceName = await this.getServiceNameAsync();
        let trueUrl = await this.getApiUrlByServiceNameAsync(url, serviceName, data);
        return trueUrl;
    }
    protected async getUrlAsync(serviceName: string): Promise<string> {
        if (this.baseUrl == location.origin) {
            try {
                const url = `${this.baseUrl}/ServerCenterAPI/Server/GetGatewayUrl`;
                const httpResult = await axios.get(url, this.getAxiosRequestConfig());
                const gatewayUrl = this.handlerHttpResult<string>(httpResult);
                if (gatewayUrl) {
                    this.baseUrl = gatewayUrl;
                }
            } catch (error) {
                this.baseUrl = "http://127.0.0.1:8700";
            }
        }
        let trueUrl = `${this.baseUrl}/${serviceName}`;
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