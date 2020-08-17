import { Injectable } from '@angular/core';
@Injectable({
    providedIn: 'root'
})
export class ServerCommon {
    private key = 'ServerUrl';
    /**
     * 是否有服务
     */
    public hasServer(): boolean {
        const serverUrl = this.getTrueServerUrl();
        return serverUrl != null && serverUrl !== undefined && serverUrl !== '';
    }
    /**
     * 设置服务地址
     * @param url url地址
     */
    public setServerUrl(url: string) {
        sessionStorage.setItem(this.key, url);
    }
    /**
     * 获取服务地址
     */
    public getServerUrl(): string {
        const url = this.getTrueServerUrl();
        return `http://${url}/api`;
    }
    /**
     * 获取服务地址
     */
    public getTrueServerUrl(): string {
        return sessionStorage.getItem(this.key);
    }
    /**
     * 清空服务地址
     */
    public removeServerUrl() {
        sessionStorage.removeItem(this.key);
    }
}
