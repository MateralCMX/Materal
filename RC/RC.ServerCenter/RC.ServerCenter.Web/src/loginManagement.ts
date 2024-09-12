/**
 * 登录管理器
 */
class LoginManagement {
    private tokenKey = "Token";
    public isLogin() {
        const token = window.localStorage.getItem(this.tokenKey);
        const result = token != null;
        return result;
    }
    public setToken(token: string) {
        window.localStorage.setItem(this.tokenKey, token);
    }
    public loginOut() {
        window.localStorage.removeItem(this.tokenKey);
    }
    public getToken() {
        return window.localStorage.getItem(this.tokenKey);;
    }
}
const loginManagement = new LoginManagement();
export default loginManagement;