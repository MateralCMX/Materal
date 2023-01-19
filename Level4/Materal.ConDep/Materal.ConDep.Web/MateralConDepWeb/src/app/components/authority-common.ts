import { Injectable } from '@angular/core';
import { TokenResultModel } from '../services/models/user/TokenResultModel';

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
    public setToken(token: TokenResultModel): void {
        localStorage.setItem(this.key, token.AccessToken);
    }
    /**
     * 移除Token
     */
    public removeToken(): void {
        localStorage.removeItem(this.key);
    }
}
