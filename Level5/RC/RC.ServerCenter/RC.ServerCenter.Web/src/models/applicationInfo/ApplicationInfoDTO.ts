export default interface ApplicationInfoDTO {
    /**
     * 唯一标识
     */
    ID: string;
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
    ApplicationType: number;
    /**
     * 应用类型文本
     */
    ApplicationTypeTxt: string;
    /**
     * 是增量更新
     */
    IsIncrementalUpdating: boolean;
    /**
     * 运行参数
     */
    RunParams: string;
    /**
     * 应用状态
     */
    ApplicationStatus: number;
    /**
     * 应用状态文本
     */
    ApplicationStatusTxt: string;
}