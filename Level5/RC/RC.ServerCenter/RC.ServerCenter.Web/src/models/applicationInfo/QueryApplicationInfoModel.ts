export default interface QueryApplicationInfoModel {
    /**
     * 名称
     */
    Name: string;
    /**
     * 根路径
     */
    RootPath: string;
    /**
     * 主模块
     */
    MainModule: string;
    /**
     * 应用类型
     */
    ApplicationType: number;
}