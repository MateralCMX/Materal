export default interface QueryUserModel {
    /**
     * 姓名
     */
    Name: string;
    /**
     * 账号
     */
    Account: string;
    /**
     * 页码
     */
    PageIndex: number;
    /**
     * 每页显示数量
     */
    PageSize: number;
}