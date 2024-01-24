export default interface AddApplicationInfoModel {
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
    /**
     * 是增量更新
     */
    IsIncrementalUpdating: boolean;
    /**
     * 运行参数
     */
    RunParams: string;
}