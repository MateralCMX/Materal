import { NzMessageService } from 'ng-zorro-antd/message';
import { throwError } from 'rxjs';
import { HttpErrorResponse, HttpHeaders, HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ResultModel } from './models/result/resultModel';
import { ResultTypeEnum } from './models/result/resultTypeEnum';
import { AuthorityCommon } from '../components/authority-common';

export class BasiceService {
    public baseUrl = 'http://116.55.251.31:8200/api';
    constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService,
                protected authorityCommon: AuthorityCommon) {
        this.baseUrl = `${location.origin}/api`;
    }
    /**
     * 发送Get请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    protected sendGetUrl<T>(url: string, data: T,
                         success?: (value: ResultModel) => void,
                         fail?: (value: ResultModel) => void,
                         complete?: () => void): void {
        const headers = this.getHttpHeaders();
        const options = { headers };
        if (data) {
            url += '?';
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    const item = data[key];
                    url += `${key}=${item}&`;
                }
            }
            url = url.substr(0, url.length - 1);
        }
        this.http.get<ResultModel>(url, options).subscribe(result => {
            this.handlerSuccess(result, success, fail);
        }, error => {
            this.handlerError(error);
            if (complete) {
                complete();
            }
        }, complete);
    }
    /**
     * 发送Post请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    protected sendPostUrl<T>(url: string, data: T,
                          success?: (value: ResultModel) => void,
                          fail?: (value: ResultModel) => void,
                          complete?: () => void): void {
        const headers = this.getHttpHeaders();
        this.http.post<ResultModel>(url, data, { headers }).subscribe(result => {
            this.handlerSuccess(result, success, fail);
        }, error => {
            this.handlerError(error);
            if (complete) {
                complete();
            }
        }, complete);
    }
    /**
     * 发送Get请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    protected sendGet<T>(url: string, data: T,
                         success?: (value: ResultModel) => void,
                         fail?: (value: ResultModel) => void,
                         complete?: () => void): void {
        url = `${this.baseUrl}${url}`;
        this.sendGetUrl(url,data,success,fail,complete);
    }
    /**
     * 发送Post请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    protected sendPost<T>(url: string, data: T,
                          success?: (value: ResultModel) => void,
                          fail?: (value: ResultModel) => void,
                          complete?: () => void): void {
        url = `${this.baseUrl}${url}`;
        this.sendPostUrl(url,data,success,fail,complete);
    }
    /**
     * 处理成功请求
     * @param result 返回值
     * @param success 成功回调
     * @param fail 失败回调
     */
    protected handlerSuccess(result: ResultModel, success: (value: ResultModel) => void, fail: (value: ResultModel) => void) {
        if (result.ResultType === ResultTypeEnum.Success) {
            if (success) {
                success(result);
            }
        } else {
            if (fail) {
                fail(result);
            } else {
                this.handlerFail(result);
            }
        }
    }
    /**
     * 获得Http请求头
     */
    protected getHttpHeaders() {
        const data: any = { 'Content-Type': 'application/json' };
        const token = this.authorityCommon.getToken();
        if (token) {
            data.Authorization = `Bearer ${token}`;
        }
        return new HttpHeaders(data);
    }
    /**
     * 处理请求异常
     * @param result 返回值
     */
    protected handlerFail(result: ResultModel) {
        this.message.create('warning', result.Message);
    }
    /**
     * 处理错误
     * @param error 错误
     */
    protected handlerError(error: HttpErrorResponse) {
        switch (error.status) {
            case 401:
                this.message.create('warning', '认证失败，请重新登录');
                this.route.navigate(['/Login']);
                break;
            case 500:
                this.message.create('error', '服务器发生错误');
                break;
            default:
                this.message.create('error', '网络请求错误');
                break;
        }
        return throwError(error);
    }
}
