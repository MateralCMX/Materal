export class AuthorityCommon {
    private static key = 'Token';
    /**
     * 获得Token
     */
    public static getToken(): string {
        return localStorage.getItem(this.key);
    }
    /**
     * 设置token
     * @param token token
     */
    public static setToken(token: string): void {
        localStorage.setItem(this.key, token);
    }
    /**
     * 移除Token
     */
    public static removeToken(): void {
        localStorage.removeItem(this.key);
    }
}
