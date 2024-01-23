export default interface AuthenticationOptionsModel {
    /**
     * 类型
     */
    AuthenticationProviderKey: string;
    /**
     * 作用域
     */
    AllowedScopes: string[];
}