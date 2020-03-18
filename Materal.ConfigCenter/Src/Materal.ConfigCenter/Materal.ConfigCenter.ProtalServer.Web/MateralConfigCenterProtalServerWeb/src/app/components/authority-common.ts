import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class AuthorityCommon {
    private key = 'Token';
    /**
     * 获得Token
     */
    public getToken(): string {
        return localStorage.getItem(this.key);
    }
    /**
     * 设置token
     * @param token token
     */
    public setToken(token: string): void {
        localStorage.setItem(this.key, token);
    }
    /**
     * 移除Token
     */
    public removeToken(): void {
        localStorage.removeItem(this.key);
    }
}
