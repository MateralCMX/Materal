/**
 * 登录管理器
 */
class LoginManagement {
    private token: string | null;
    private tokenKey = "Token";
    constructor() {
        this.token = window.localStorage.getItem(this.tokenKey);
    }
    public isLogin() {
        return this.token != null;
    }
    public setToken(token: string) {
        this.token = token;
        window.localStorage.setItem(this.tokenKey, token);
    }
    public loginOut() {
        this.token = null;
        window.localStorage.removeItem(this.tokenKey);
    }
    public getToken() {
        return this.token;
    }
}
const loginManagement = new LoginManagement();
export default loginManagement;