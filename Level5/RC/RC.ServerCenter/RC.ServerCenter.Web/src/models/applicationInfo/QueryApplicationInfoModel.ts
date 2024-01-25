export default interface QueryApplicationInfoModel {
    /**
     * 名称
     */
    Name: string;
    /**
     * 路径
     */
    RootPath: string;
    /**
     * 主模块
     */
    MainModule: string;
    /**
     * 应用类型
     */
    ApplicationType?: number;
    /**
     * 页码
     */
    PageIndex: number;
    /**
     * 每页显示数量
     */
    PageSize: number;
}